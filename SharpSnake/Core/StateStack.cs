using System;
using System.Collections.Generic;

namespace SharpSnake.Core
{
    /// <summary>
    /// Manages game states in a stack-based fashion.
    /// The state on top the stack is the active one.
    /// </summary>
    public class StateStack
    {
        /// <summary>
        /// Indicates whether the stack is empty.
        /// </summary>
        public bool Empty
        {
            get => Stack.Count == 0;
        }

        /// <summary>
        /// Get the currently active state.
        /// </summary>
        public IState CurrentState
        {
            get => Stack.Count > 0 ? Stack.Peek() : null;
        }

        private readonly Stack<IState> Stack = new Stack<IState>();
        private readonly Stack<IState> DrawStack = new Stack<IState>();
        private readonly Dictionary<StateId, Func<IState>> StateFactories = new Dictionary<StateId, Func<IState>>();
        private readonly Queue<StateChangeInfo> RequestQueue = new Queue<StateChangeInfo>();
        private readonly StateContext Context;

        /// <summary>
        /// Initialize a new stack instance.
        /// </summary>
        /// <param name="context">A context object that is shared between all states</param>
        public StateStack(StateContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Registers a new game state.
        /// Only registered states can be pushed onto the stack.
        /// </summary>
        /// <param name="id">State registration ID</param>
        /// <typeparam name="T">Class that represents the state</typeparam>
        public void RegisterState<T>(StateId id) where T: State
        {
            StateFactories[id] = () => {
                var state = Activator.CreateInstance(typeof(T), new object[] { Context }) as T;
                state.OnRequestStateChange += HandleStateChangeRequest;
                return state;
            };
        }

        /// <summary>
        /// Render the states from bottom to top.
        /// </summary>
        public void Draw()
        {
            // Query the next lower state for rendering only if the current state
            // is set to transparent.
            while (CurrentState != null && CurrentState.Transparent)
            {
                DrawStack.Push(Stack.Pop());
            }

            CurrentState?.Draw();

            while (DrawStack.Count > 0)
            {
                var state = DrawStack.Pop();
                state.Draw();
                Stack.Push(state);
            }
        }

        /// <summary>
        /// Update the current state.
        /// </summary>
        public void Update()
        {
            CurrentState?.Update();
            ProcessStateChangeRequests();
        }

        /// <summary>
        /// Handle the input of the current state.
        /// </summary>
        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }

        /// <summary>
        /// Push a registered state onto the stack.
        /// </summary>
        /// <param name="stateId">State registration ID</param>
        public void PushState(StateId stateId)
        {
            var factory = StateFactories[stateId];
            Stack.Push(factory());
        }

        /// <summary>
        /// Handle an incoming change request that was sent by the current state.
        /// </summary>
        /// <param name="info">An object that describes the request</param>
        private void HandleStateChangeRequest(StateChangeInfo info)
        {
            RequestQueue.Enqueue(info);
        }

        /// <summary>
        /// Process all pending change requests that were sent by the current state.
        /// </summary>
        private void ProcessStateChangeRequests()
        {
            while (RequestQueue.Count > 0)
            {
                var info = RequestQueue.Dequeue();

                switch (info.Command)
                {
                    case StateChangeCommand.PushState:
                        PushState(info.StateId);
                        break;

                    case StateChangeCommand.PopState:
                        Stack.Pop();
                        break;

                    case StateChangeCommand.ClearStack:
                        Stack.Clear();
                        break;
                }
            }
        }
    }
}
