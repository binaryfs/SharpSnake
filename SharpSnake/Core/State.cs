using SharpSnake.Input;

namespace SharpSnake.Core
{
    /// <summary>
    /// Represents an abstract base class for game states.
    /// </summary>
    public abstract class State: IState
    {
        public event RequestStateChangeHandler OnRequestStateChange;

        public bool Transparent
        {
            get;
        }

        public readonly StateContext Context;
        protected readonly InputMap InputMap;

        public State(StateContext context, bool transparent = false)
        {
            InputMap = new InputMap();
            Context = context;
            Transparent = transparent;
        }
        
        public abstract void Draw();

        public virtual void Update()
        {
            // Base class does nothing.
        }

        public void HandleInput()
        {
            InputMap.SendActions(HandleActionInternal);
        }

        protected abstract void HandleAction(ActionType action);

        /// <summary>
        /// Request the state stack to push the specified state.
        /// </summary>
        /// <param name="stateId">The state to push</param>
        protected void RequestPushState(StateId stateId)
        {
            OnRequestStateChange?.Invoke(new StateChangeInfo(StateChangeCommand.PushState, stateId));
        }

        /// <summary>
        /// Request the state stack to pop the topmost state.
        /// </summary>
        protected void RequestPopState()
        {
            OnRequestStateChange?.Invoke(new StateChangeInfo(StateChangeCommand.PopState));
        }

        /// <summary>
        /// Request the state stack to pop all states.
        /// </summary>
        protected void RequestClearStack()
        {
            OnRequestStateChange?.Invoke(new StateChangeInfo(StateChangeCommand.ClearStack));
        }

        private void HandleActionInternal(ActionType action)
        {
            if (action == ActionType.CloseWindow)
            {
                RequestClearStack();
            }
            else
            {
                HandleAction(action);
            }
        }
    }
}
