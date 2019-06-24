using SharpSnake.Display;

namespace SharpSnake.Core
{
    /// <summary>
    /// Represents data that is shared between <see cref="State"/> instances.
    /// </summary>
    public struct StateContext
    {
        public readonly ConsoleScreen Screen;
        public readonly Settings Settings;

        public StateContext(ConsoleScreen screen, Settings settings)
        {
            Screen = screen;
            Settings = settings;
        }
    }
}
