using System.Drawing;
using SharpSnake.Display;

namespace SharpSnake.Game
{
    public class Food
    {
        public Point Position
        {
            get => new Point(Left, Top);
        }

        public bool Spoiled
        {
            get;
            private set;
        }

        public int Score
        {
            get
            {
                switch (Type)
                {
                    case FoodType.Feast:
                        return 50;
                    case FoodType.Meal:
                        return 25;
                    default:
                        return 10;
                }
            }
        }

        public readonly FoodType Type;
        public readonly int Left;
        public readonly int Top;
        private readonly Timer Lifetime;
        private readonly Timer Blinking;
        private bool Visible = true;

        public Food(FoodType type, int left, int top)
        {
            Type = type;
            Left = left;
            Top = top;
            Lifetime = new Timer(300, () => Spoiled = true);
            Blinking = new Timer(10, () => Visible = !Visible);
        }

        public void Update()
        {
            Lifetime.Tick();

            if (Lifetime.Progress >= 0.8)
            {
                Blinking.Tick();
            }
        }

        public void Draw(ConsoleScreen screen)
        {
            if (Visible)
            {
                char symbol;

                switch (Type)
                {
                    case FoodType.Feast:
                        symbol = 'Q';
                        break;
                    case FoodType.Meal:
                        symbol = 'ö';
                        break;
                    default:
                        symbol = 'o';
                        break;
                }

                screen.SetColor(PaletteColor.Food);
                screen.Draw(symbol, Left, Top);
            }
        }
    }
}
