using BearLib;

namespace SharpSnake.Display
{
    /// <summary>
    /// Represents a terminal-like screen that is used to display the game.
    /// </summary>
    public class ConsoleScreen
    {
        /// <summary>
        /// Width of the screen in cell columns.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Height of the screen in cell rows.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// The palette used for drawing.
        /// </summary>
        public Palette Palette
        {
            get;
            set;
        }

        /// <summary>
        /// Initialize a new screen with the given dimensions.
        /// </summary>
        /// <param name="width">Width in cell columns</param>
        /// <param name="height">Height in cell columns</param>
        public ConsoleScreen(int width, int height)
        {
            Terminal.Set(string.Format("window: size={0}x{1}, cellsize=12x16", width, height));
            Width = width;
            Height = height;
            Palette = Palette.GetPalette(PaletteId.Default);
        }

        /// <summary>
        /// Draw a single character at the specified position.
        /// </summary>
        /// <param name="symbol">The character to draw</param>
        /// <param name="left">The position along X-axis in cell columns</param>
        /// <param name="top">The position along Y-axis in cell rows</param>
        public void Draw(char symbol, int left, int top)
        {
            Terminal.Put(left, top, symbol);
        }

        /// <summary>
        /// Draw text at the specified position.
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="left">The position along X-axis in cell columns</param>
        /// <param name="top">The position along Y-axis in cell rows</param>
        public void Draw(string text, int left, int top)
        {
            Terminal.Print(left, top, text);
        }

        /// <summary>
        /// Fill a rectangular area with a given character.
        /// </summary>
        /// <param name="symbol">The character to draw</param>
        /// <param name="left">Position of the rectangle's left border along the X-axis (in columns)</param>
        /// <param name="top">Position of the rectangle's top border along the Y-axis (in rows)</param>
        /// <param name="width">The rectangle's width in columns</param>
        /// <param name="height">The rectangle's height in rows</param>
        public void Fill(char symbol, int left, int top, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Terminal.Put(left + x, top + y, symbol);
                }
            }
        }

        /// <summary>
        /// Set the drawing color.
        /// </summary>
        /// <param name="color">Color from a palette</param>
        public void SetColor(PaletteColor color)
        {
            Terminal.Color(Palette.GetColor(color));
        }

        /// <summary>
        /// Show the screen content.
        /// </summary>
        public void Display()
        {
            Terminal.Refresh();
        }

        /// <summary>
        /// Clear the screen.
        /// </summary>
        public void Clear()
        {
            Terminal.Clear();
            Terminal.BkColor(Palette.GetColor(PaletteColor.Background));
        }
    }
}
