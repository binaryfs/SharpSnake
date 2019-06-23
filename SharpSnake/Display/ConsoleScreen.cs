using BearLib;

namespace SharpSnake.Display
{
    public class ConsoleScreen
    {
        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public Palette Palette
        {
            get;
            set;
        }

        public ConsoleScreen(int width, int height)
        {
            Terminal.Set(string.Format("window: size={0}x{1}, cellsize=12x16", width, height));
            Width = width;
            Height = height;
            Palette = Palette.GetPalette(PaletteId.Default);
        }

        public void Draw(char symbol, int left, int top)
        {
            Terminal.Put(left, top, symbol);
        }

        public void Draw(string text, int left, int top)
        {
            Terminal.Print(left, top, text);
        }

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

        public void SetColor(PaletteColor color)
        {
            Terminal.Color(Palette.GetColor(color));
        }

        public void Display()
        {
            Terminal.Refresh();
        }

        public void Clear()
        {
            Terminal.Clear();
            Terminal.BkColor(Palette.GetColor(PaletteColor.Background));
        }
    }
}
