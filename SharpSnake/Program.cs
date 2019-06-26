using System;
using SharpSnake.Core;
using SharpSnake.Display;
using SharpSnake.Game.States;
using BearLib;

namespace SharpSnake
{
    class Program
    {
        public static readonly string GameTitle = string.Join(
            Environment.NewLine,
            @" ____  _",
            @"/ ___|| |__   __ _ _ __ _ __",
            @"\___ \| '_ \ / _` | '__| '_ \",
            @" ___) | | | | (_| | |  | |_) |",
            @"|____/|_| |_|\__,_|_|  | .__/",
            @"/ ___| _ __   __ _| | _|_|_",
            @"\___ \| '_ \ / _` | |/ / _ \",
            @" ___) | | | | (_| |   <  __/",
            @"|____/|_| |_|\__,_|_|\_\___|");

        static void Main(string[] args)
        {
            Terminal.Open();
            Terminal.Set(string.Format("window.title='{0}'", "SharpSnake"));
            Terminal.Refresh();

            var consoleScreen = new ConsoleScreen(36, 28);
            var context = new StateContext(consoleScreen, new Settings());
            var stateStack = new StateStack(context);

            stateStack.RegisterState<MenuState>(StateId.Menu);
            stateStack.RegisterState<OptionsState>(StateId.Options);
            stateStack.RegisterState<PlayState>(StateId.Play);
            stateStack.RegisterState<GameOverState>(StateId.GameOver);
            stateStack.RegisterState<PauseState>(StateId.Pause);

            // Initial state
            stateStack.PushState(StateId.Menu);

            while (!stateStack.Empty)
            {
                stateStack.HandleInput();
                stateStack.Update();

                consoleScreen.Clear();
                stateStack.Draw();
                consoleScreen.Display();

                System.Threading.Thread.Sleep(16);
            }

            Terminal.Close();
        }
    }
}
