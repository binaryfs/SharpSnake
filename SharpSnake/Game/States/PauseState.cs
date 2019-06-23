using SharpSnake.Core;
using SharpSnake.Display;
using SharpSnake.Display.UI;
using SharpSnake.Input;
using BearLib;

namespace SharpSnake.Game.States
{
    public class PauseState: State
    {
        private readonly Menu Menu;
        private readonly Timer PauseTimer;
        private bool PauseVisible = true;

        public PauseState(StateContext context): base(context, true)
        {
            InputMap.AddMapping(Terminal.TK_UP, ActionType.MoveUp);
            InputMap.AddMapping(Terminal.TK_DOWN, ActionType.MoveDown);
            InputMap.AddMapping(Terminal.TK_ENTER, ActionType.Activate);
            InputMap.AddMapping(Terminal.TK_ESCAPE, ActionType.Escape);

            Menu = new Menu();
            Menu.AddItem(new Button("Resume", () => RequestPopState()));
            Menu.AddItem(new Button("Cancel", PressCancel));

            PauseTimer = new Timer(20, () => PauseVisible = !PauseVisible);
        }

        public override void Update()
        {
            PauseTimer.Tick();
        }

        public override void Draw()
        {
            Context.Screen.Fill(' ', 11, 14, 13, 5);

            if (PauseVisible)
            {
                Context.Screen.SetColor(PaletteColor.Text);
                Context.Screen.Draw("GAME PAUSED", 12, 15);
            }

            Menu.Draw(Context.Screen, 12, 17);
        }

        protected override void HandleAction(ActionType action)
        {
            if (action == ActionType.Escape)
            {
                RequestPopState();
            }
            else
            {
                Menu.HandleAction(action);
            }
        }

        private void PressCancel()
        {
            RequestClearStack();
            RequestPushState(StateId.Menu);
        }
    }
}
