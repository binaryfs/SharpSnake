using System.Drawing;
using System.Collections.Generic;
using SharpSnake.Input;
using SharpSnake.Display;

namespace SharpSnake.Game
{
    public class Snake
    {
        public int Left
        {
            get;
            private set;
        }

        public int Top
        {
            get;
            private set;
        }

        public Point Position
        {
            get => new Point(Left, Top);
        }

        public Direction Direction
        {
            get;
            private set;
        }

        private readonly List<Point> Body = new List<Point>();
        private int Growth;
        private readonly Timer MoveTimer;
        private Direction DesiredDirection = Direction.Right;

        public Snake(int left, int top, int growth = 0)
        {
            Direction = DesiredDirection;
            Left = left;
            Top = top;
            Growth = growth;
            MoveTimer = new Timer(10, Move);
        }

        public void HandleAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.MoveUp:
                    ChangeDirection(Direction.Up, Direction.Down);
                    break;

                case ActionType.MoveDown:
                    ChangeDirection(Direction.Down, Direction.Up);
                    break;

                case ActionType.MoveLeft:
                    ChangeDirection(Direction.Left, Direction.Right);
                    break;

                case ActionType.MoveRight:
                    ChangeDirection(Direction.Right, Direction.Left);
                    break;
            }
        }

        public void Update()
        {
            MoveTimer.Tick();
        }

        public void Draw(ConsoleScreen screen)
        {
            screen.SetColor(PaletteColor.Snake);
            screen.Draw('@', Left, Top);

            foreach (Point part in Body)
            {
                screen.Draw('O', part.X, part.Y);
            }
        }

        public void Grow(int growth = 1)
        {
            Growth += growth;
        }

        public bool CollidesWith(Point position)
        {
            return HeadCollidesWith(position) || BodyColliedsWith(position);
        }

        public bool HeadCollidesWith(Point position)
        {
            return Position.Equals(position);
        }

        public bool BodyColliedsWith(Point position)
        {
            foreach (Point part in Body)
            {
                if (part.Equals(position))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CollidesWithSelf()
        {
            foreach (Point part in Body)
            {
                if (part.Equals(Position))
                {
                    return true;
                }
            }

            return false;
        }

        private void Move()
        {
            Direction = DesiredDirection;

            if (Growth > 0)
            {
                Growth -= 1;
                Body.Add(Position);
            }
            else if (Body.Count > 0)
            {
                Body.RemoveAt(0);
                Body.Add(Position);
            }

            switch (Direction)
            {
                case Direction.Up:
                    Top -= 1;
                    break;
                case Direction.Down:
                    Top += 1;
                    break;
                case Direction.Left:
                    Left -= 1;
                    break;
                case Direction.Right:
                    Left += 1;
                    break;
            }
        }

        private void ChangeDirection(Direction newDirection, Direction opposite)
        {
            DesiredDirection = Direction != opposite ? newDirection : opposite;
        }
    }
}
