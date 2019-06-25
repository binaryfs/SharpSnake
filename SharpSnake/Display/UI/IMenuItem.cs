using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    public interface IMenuItem
    {
        void HandleAction(ActionType action);

        /// <summary>
        /// Draw the menu item at the given screen position.
        /// </summary>
        /// <param name="screen">The screen to draw on</param>
        /// <param name="left">Position along X-axis in columns</param>
        /// <param name="top">Position along Y-axis in rows</param>
        void Draw(ConsoleScreen screen, int left, int top);
    }
}
