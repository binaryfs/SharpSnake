namespace SharpSnake.Input
{
    public class KeyData
    {
        public ActionType Action
        {
            get;
        }

        public bool Pressed
        {
            get;
            private set;
        }

        public bool WasPressed
        {
            get;
            private set;
        }

        public KeyData(ActionType action)
        {
            Action = action;
        }

        public void Release()
        {
            WasPressed = Pressed;
            Pressed = false;
        }

        public bool Press()
        {
            Pressed = true;
            return !WasPressed;
        }
    }
}
