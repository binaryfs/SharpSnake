using SharpSnake.Display;
using SharpSnake.Input;

namespace SharpSnake.Core
{
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

        protected void RequestPushState(StateId stateId)
        {
            OnRequestStateChange?.Invoke(new StateChangeInfo(StateChangeCommand.PushState, stateId));
        }

        protected void RequestPopState()
        {
            OnRequestStateChange?.Invoke(new StateChangeInfo(StateChangeCommand.PopState));
        }

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
