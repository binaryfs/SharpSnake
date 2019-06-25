using SharpSnake.Core;
using SharpSnake.Display;
using SharpSnake.Display.UI;
using SharpSnake.Input;
using BearLib;
using System;

namespace SharpSnake.Game.States
{
    /// <summary>
    /// The main menu state.
    /// </summary>
    public class MenuState: State
    {
        private readonly Menu Menu;

        public MenuState(StateContext context): base(context)
        {
            InputMap.AddMapping(Terminal.TK_UP, ActionType.MoveUp);
            InputMap.AddMapping(Terminal.TK_DOWN, ActionType.MoveDown);
            InputMap.AddMapping(Terminal.TK_ENTER, ActionType.Activate);

            Menu = new Menu();
            Menu.AddItem(new Button("Play", () => RequestPushState(StateId.Play)));
            Menu.AddItem(new Button("Options", () => RequestPushState(StateId.Options)));
            Menu.AddItem(new Button("Quit", () => RequestClearStack()));
        }

        protected override void HandleAction(ActionType action)
        {
            Menu.HandleAction(action);
        }

        public override void Draw()
        {
            Context.Screen.SetColor(PaletteColor.Snake);
            Context.Screen.Draw(Application.GameTitle, 3, 3);
            Menu.Draw(Context.Screen, 3, 17);
        }
    }
}