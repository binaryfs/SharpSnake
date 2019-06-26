using System.Drawing;
using System.Collections.Generic;
using SharpSnake.Input;
using SharpSnake.Display;

namespace SharpSnake.Game
{
    /// <summary>
    /// Represents the game's protagonist.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// The snake's head screen position along the X-axis (in columns).
        /// </summary>
        public int Left
        {
            get;
            private set;
        }

        /// <summary>
        /// The snake's head screen positon along the Y-axis (in rows).
        /// </summary>
        public int Top
        {
            get;
            private set;
        }

        /// <summary>
        /// The position of the snake's head in columns and rows.
        /// </summary>
        public Point Position
        {
            get => new Point(Left, Top);
        }

        /// <summary>
        /// The direction that the snake is facing/moving.
        /// </summary>
        public Direction Direction
        {
            get;
            private set;
        }

        private readonly List<Point> Body = new List<Point>();
        private int Growth;
        private readonly Timer MoveTimer;
        private Direction DesiredDirection = Direction.Right;

        /// <summary>
        /// Initialize a new Snake instance.
        /// </summary>
        /// <param name="left">Initial position on X-axis</param>
        /// <param name="top">Initial position on Y-axis</param>
        /// <param name="gameSpeed">Speed setting</param>
        /// <param name="initialLength">Initial length of the snake's body</param>
        public Snake(int left, int top, GameSpeed gameSpeed, int initialLength = 0)
        {
            Direction = DesiredDirection;
            Left = left;
            Top = top;
            Growth = initialLength;

            var speedMap = new Dictionary<GameSpeed, int>()
            {
                [GameSpeed.Slow] = 9,
                [GameSpeed.Medium] = 6,
                [GameSpeed.Fast] = 3
            };

            MoveTimer = new Timer(speedMap[gameSpeed], Move);
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

        /// <summary>
        /// Grow the snake by the given amount.
        /// </summary>
        /// <param name="growth">Number of segments to add</param>
        public void Grow(int growth = 1)
        {
            Growth += growth;
        }

        /// <summary>
        /// Check if the snake collides with the given position.
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>true if it does, false otherwise</returns>
        public bool CollidesWith(Point position)
        {
            return HeadCollidesWith(position) || BodyColliedsWith(position);
        }

        /// <summary>
        /// Check if the snake's head collides with the given position.
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>true if it does, false otherwise</returns>
        public bool HeadCollidesWith(Point position)
        {
            return Position.Equals(position);
        }

        /// <summary>
        /// Check if the snake's body collides with the given position.
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>true if it does, false otherwise</returns>
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

        /// <summary>
        /// Check if the snake collides with its own body.
        /// </summary>
        /// <returns>true if it does, false otherwise</returns>
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
            DesiredDirection = (Direction != opposite) ? newDirection : opposite;
        }
    }
}
