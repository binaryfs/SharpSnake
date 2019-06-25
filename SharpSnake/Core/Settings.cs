using SharpSnake.Game;
using SharpSnake.Display;

namespace SharpSnake.Core
{
    /// <summary>
    /// Encapsulates adjustable game settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The initial speed of the game.
        /// </summary>
        public GameSpeed Speed
        {
            get;
            set;
        }

        /// <summary>
        /// The palette used for drawing the terminal.
        /// </summary>
        public PaletteId PaletteId
        {
            get;
            set;
        }
    }
}
