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

        /// <summary>
        /// Handle user input.
        /// </summary>
        void HandleInput();

        /// <summary>
        /// Draw the state on screen.
        /// </summary>
        void Draw();

        /// <summary>
        /// Execute game logic.
        /// </summary>
        void Update();
    }
}