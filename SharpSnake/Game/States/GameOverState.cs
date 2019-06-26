using SharpSnake.Core;
using SharpSnake.Display;
using SharpSnake.Display.UI;
using SharpSnake.Input;
using BearLib;

namespace SharpSnake.Game.States
{
    /// <summary>
    /// The game over screen.
    /// </summary>
    public class GameOverState: State
    {
        private readonly Menu Menu;

        public GameOverState(StateContext context): base(context)
        {
            InputMap.AddMapping(Terminal.TK_UP, ActionType.MoveUp);
            InputMap.AddMapping(Terminal.TK_DOWN, ActionType.MoveDown);
            InputMap.AddMapping(Terminal.TK_LEFT, ActionType.MoveLeft);
            InputMap.AddMapping(Terminal.TK_RIGHT, ActionType.MoveRight);
            InputMap.AddMapping(Terminal.TK_ENTER, ActionType.Activate);

            Menu = new Menu();
            Menu.AddItem(new Button("Retry", Retry));
            Menu.AddItem(new Button("Cancel", () => RequestPopState()));
        }

        public override void Draw()
        {
            Context.Screen.SetColor(PaletteColor.Snake);
            Context.Screen.Draw("GAME OVER", 3, 15);
            Menu.Draw(Context.Screen, 3, 17);
        }

        protected override void HandleAction(ActionType action)
        {
            Menu.HandleAction(action);
        }

        private void Retry()
        {
            RequestPopState();
            RequestPushState(StateId.Play);
        }
    }
}
