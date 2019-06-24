using SharpSnake.Display;

namespace SharpSnake.Core
{
    public delegate void RequestStateChangeHandler(StateChangeInfo info);

    /// <summary>
    /// Interface for game states.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// An Event that is triggered when a request is sent to the state stack.
        /// </summary>
        event RequestStateChangeHandler OnRequestStateChange;

        /// <summary>
        /// Specifies if the states below this state can still be seen.
        /// </summary>
        bool Transparent
        {
            get;
        }

        void HandleInput();

        void Draw();

        void Update();
    }
}