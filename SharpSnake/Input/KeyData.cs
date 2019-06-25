namespace SharpSnake.Input
{
    /// <summary>
    /// Represents a key binding of an <see cref="InputMap"/> instance.
    /// </summary>
    public class KeyData
    {
        /// <summary>
        /// The action that the key is mapped to.
        /// </summary>
        public ActionType Action
        {
            get;
        }

        /// <summary>
        /// Indicates whether the key is pressed.
        /// </summary>
        public bool Pressed
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates whether the key was pressed in the previous frame.
        /// </summary>
        public bool WasPressed
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialize a new instance and map it to the given action.
        /// </summary>
        /// <param name="action">The action</param>
        public KeyData(ActionType action)
        {
            Action = action;
        }

        /// <summary>
        /// Release the key (if it was pressed).
        /// </summary>
        public void Release()
        {
            WasPressed = Pressed;
            Pressed = false;
        }

        /// <summary>
        /// Press the key.
        /// </summary>
        /// <returns>true if the key is initially pressed, false if it is hold</returns>
        public bool Press()
        {
            Pressed = true;
            return !WasPressed;
        }
    }
}
