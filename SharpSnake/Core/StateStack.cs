using System;
using System.Collections.Generic;
using SharpSnake.Display;

namespace SharpSnake.Core
{
    public class StateStack
    {
        public bool Empty
        {
            get => Stack.Count == 0;
        }

        public IState CurrentState
        {
            get => Stack.Count > 0 ? Stack.Peek() : null;
        }

        private readonly Stack<IState> Stack = new Stack<IState>();
        private readonly Stack<IState> DrawStack = new Stack<IState>();
        private readonly Dictionary<StateId, Func<IState>> StateFactories = new Dictionary<StateId, Func<IState>>();
        private readonly Queue<StateChangeInfo> RequestQueue = new Queue<StateChangeInfo>();
        private readonly StateContext Context;

        public StateStack(StateContext context)
        {
            Context = context;
        }

        public void RegisterState<T>(StateId id) where T: State
        {
            StateFactories[id] = () => {
                var state = Activator.CreateInstance(typeof(T), new object[] { Context }) as T;
                state.OnRequestStateChange += HandleStateChangeRequest;
                return state;
            };
        }

        public void Draw()
        {
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

        public void Update()
        {
            CurrentState?.Update();
            ProcessStateChangeRequests();
        }

        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }

        public void PushState(StateId stateId)
        {
            var factory = StateFactories[stateId];
            Stack.Push(factory());
        }

        private void HandleStateChangeRequest(StateChangeInfo info)
        {
            RequestQueue.Enqueue(info);
        }

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
