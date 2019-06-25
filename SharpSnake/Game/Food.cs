using System.Drawing;
using SharpSnake.Display;

namespace SharpSnake.Game
{
    /// <summary>
    /// Represents an item that increases the player's score when collected.
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Screen position in columns and rows.
        /// </summary>
        public Point Position
        {
            get => new Point(Left, Top);
        }

        /// <summary>
        /// Screen position along the X-axis in columns.
        /// </summary>
        public int Left
        {
            get;
        }

        /// <summary>
        /// Screen position along the Y-axis in rows.
        /// </summary>
        public int Top
        {
            get;
        }

        /// <summary>
        /// Indicates the item type.
        /// The type determines the item's score and look.
        /// </summary>
        public FoodType Type
        {
            get;
        }

        /// <summary>
        /// Indicates whether the item is marked for removal.
        /// Items spoil when their lifetime has run out.
        /// </summary>
        public bool Spoiled
        {
            get;
            private set;
        }

        /// <summary>
        /// The amount points that the user receives when collecting the item.
        /// </summary>
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
