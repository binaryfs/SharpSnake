using System;
using SharpSnake.Display;
using SharpSnake.Game.States;
using BearLib;

namespace SharpSnake.Core
{
    public class Application
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

        private StateStack StateStack;
        private ConsoleScreen ConsoleScreen;

        public Application(string title)
        {
            Terminal.Open();
            Terminal.Set(string.Format("window.title='{0}'", title));

            ConsoleScreen = new ConsoleScreen(36, 28);

            var context = new StateContext(ConsoleScreen, new Settings());

            StateStack = new StateStack(context);
            StateStack.RegisterState<MenuState>(StateId.Menu);
            StateStack.RegisterState<OptionsState>(StateId.Options);
            StateStack.RegisterState<PlayState>(StateId.Play);
            StateStack.RegisterState<GameOverState>(StateId.GameOver);
            StateStack.RegisterState<PauseState>(StateId.Pause);
        }

        public void Run()
        {
            Terminal.Refresh();

            StateStack.PushState(StateId.Menu);

            while (!StateStack.Empty)
            {
                StateStack.HandleInput();
                StateStack.Update();
                ConsoleScreen.Clear();
                StateStack.Draw();
                ConsoleScreen.Display();

                System.Threading.Thread.Sleep(16);
            }

            Terminal.Close();
        }
    }
}
