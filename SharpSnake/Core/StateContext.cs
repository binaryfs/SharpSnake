﻿using SharpSnake.Display;

namespace SharpSnake.Core
{
    public struct StateContext
    {
        public readonly ConsoleScreen Screen;
        public readonly Settings Settings;

        public StateContext(ConsoleScreen screen, Settings settings)
        {
            Screen = screen;
            Settings = settings;
        }
    }
}