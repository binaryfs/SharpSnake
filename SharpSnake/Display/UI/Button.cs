using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    /// <summary>
    /// Represents a menu item that triggers an event when pressed.
    /// </summary>
    public class Button: IMenuItem
    {
        public delegate void PressHandler();

        /// <summary>
        /// The button's label.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        private event PressHandler OnPress;

        /// <summary>
        /// Initialize a new button instance.
        /// </summary>
        /// <param name="text">Button label</param>
        /// <param name="pressHandler">Event handler for button presses</param>
        public Button(string text, PressHandler pressHandler)
        {
            Text = text;
            OnPress += pressHandler;
        }

        public void HandleAction(ActionType action)
        {
            if (action == ActionType.Activate)
            {
                OnPress?.Invoke();
            }
        }

        public void Draw(ConsoleScreen screen, int left, int top)
        {
            screen.SetColor(PaletteColor.Text);
            screen.Draw(Text, left, top);
        }
    }
}
