namespace SharpSnake.Game
{
    /// <summary>
    /// Represents a timer that triggers an event handler in specified intervals.
    /// </summary>
    public class Timer
    {
        public delegate void UpdateHandler();

        /// <summary>
        /// The timer's interval in ticks.
        /// </summary>
        public int Interval
        {
            get;
            private set;
        }

        /// <summary>
        /// The timer's progress in percentage (0..1).
        /// </summary>
        public float Progress
        {
            get => (float)Counter / Interval;
        }

        private event UpdateHandler OnUpdate;
        private int Counter = 0;

        /// <summary>
        /// Initialize a new timer instance.
        /// </summary>
        /// <param name="interval">The interval in ticks</param>
        /// <param name="method">The event handler to trigger</param>
        public Timer(int interval, UpdateHandler method)
        {
            Interval = interval;
            OnUpdate += method;
        }

        /// <summary>
        /// Progress the timer.
        /// </summary>
        public void Tick()
        {
            Counter += 1;

            if (Counter == Interval)
            {
                Counter = 0;
                OnUpdate();
            }
        }
    }
}
