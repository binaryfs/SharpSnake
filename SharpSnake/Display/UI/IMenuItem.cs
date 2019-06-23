using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    public interface IMenuItem
    {
        void HandleAction(ActionType action);
        void Draw(ConsoleScreen screen, int left, int top);
    }
}
