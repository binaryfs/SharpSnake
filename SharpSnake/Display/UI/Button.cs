using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    public class Button: IMenuItem
    {
        public delegate void PressHandler();

        public string Text
        {
            get;
            private set;
        }

        private event PressHandler OnPress;

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
