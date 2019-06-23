using SharpSnake.Display;

namespace SharpSnake.Core
{
    public delegate void RequestStateChangeHandler(StateChangeInfo info);

    public interface IState
    {
        event RequestStateChangeHandler OnRequestStateChange;

        bool Transparent
        {
            get;
        }

        void HandleInput();

        void Draw();

        void Update();
    }
}