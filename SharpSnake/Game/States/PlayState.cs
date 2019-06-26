using SharpSnake.Core;
using SharpSnake.Display;
using SharpSnake.Input;
using BearLib;
using System;
using System.Drawing;

namespace SharpSnake.Game.States
{
    /// <summary>
    /// The state that implements the game logic.
    /// </summary>
    public class PlayState: State
    {
        private readonly Snake Snake;
        private Food Food;
        private int Score;
        private Rectangle GameField;

        public PlayState(StateContext context): base(context)
        {
            InputMap.AddMapping(Terminal.TK_UP, ActionType.MoveUp);
            InputMap.AddMapping(Terminal.TK_DOWN, ActionType.MoveDown);
            InputMap.AddMapping(Terminal.TK_LEFT, ActionType.MoveLeft);
            InputMap.AddMapping(Terminal.TK_RIGHT, ActionType.MoveRight);
            InputMap.AddMapping(Terminal.TK_ESCAPE, ActionType.Escape);

            Snake = new Snake(10, 10, context.Settings.Speed, 10);
            GameField = new Rectangle(2, 3, context.Screen.Width - 4, context.Screen.Height - 5);

            PlaceRandomFood();
        }

        public override void Update()
        {
            Food.Update();
            Snake.Update();
            CheckForCollisions();

            if (Food.Spoiled)
            {
                PlaceRandomFood();
            }
        }

        public override void Draw()
        {
            Context.Screen.SetColor(PaletteColor.Text);
            Context.Screen.Draw("Score: " + Score, 1, 0);

            Context.Screen.SetColor(PaletteColor.Floor);
            Context.Screen.Fill('.', GameField.Left, GameField.Top, GameField.Width, GameField.Height);

            Context.Screen.SetColor(PaletteColor.Wall);
            Context.Screen.Fill('#', GameField.Left, GameField.Top - 1, GameField.Width, 1);
            Context.Screen.Fill('#', GameField.Left, GameField.Bottom, GameField.Width, 1);
            Context.Screen.Fill('#', GameField.Left - 1, GameField.Top - 1, 1, GameField.Height + 2);
            Context.Screen.Fill('#', GameField.Right, GameField.Top - 1, 1, GameField.Height + 2);

            Food.Draw(Context.Screen);
            Snake.Draw(Context.Screen);
        }

        protected override void HandleAction(ActionType action)
        {
            if (action == ActionType.Escape)
            {
                RequestPushState(StateId.Pause);
            }
            else
            {
                Snake.HandleAction(action);
            }
        }

        private void CheckForCollisions()
        {
            if (Snake.CollidesWithSelf() || !GameField.Contains(Snake.Position))
            {
                RequestPopState();
                RequestPushState(StateId.GameOver);
            }
            else if (Snake.CollidesWith(Food.Position))
            {
                Score += Food.Score;
                Snake.Grow();
                PlaceRandomFood();
            }
        }

        private void PlaceRandomFood()
        {
            Random rnd = new Random();
            Food = null;

            while (Food == null)
            {
                int left = rnd.Next(0, GameField.Width) + GameField.Left;
                int top = rnd.Next(0, GameField.Height) + GameField.Top;

                if (!Snake.CollidesWith(new Point(left, top)))
                {
                    int type = rnd.Next(0, 100);
                    if (type < 10)
                    {
                        Food = new Food(FoodType.Feast, left, top, Context.Settings.Speed);
                    }
                    else if (type < 30)
                    {
                        Food = new Food(FoodType.Meal, left, top, Context.Settings.Speed);
                    }
                    else
                    {
                        Food = new Food(FoodType.Snack, left, top, Context.Settings.Speed);
                    }
                }
            }
        }
    }
}
