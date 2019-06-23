using System;

namespace SharpSnake.Game
{
    public class Timer
    {
        public delegate void UpdateHandler();

        public int Interval
        {
            get;
            private set;
        }

        public float Progress
        {
            get => (float)Counter / Interval;
        }

        private event UpdateHandler OnUpdate;
        private int Counter = 0;

        public Timer(int interval, UpdateHandler method)
        {
            Interval = interval;
            OnUpdate += method;
        }

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
