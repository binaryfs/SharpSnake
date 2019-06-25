using SharpSnake.Core;
using SharpSnake.Display;
using SharpSnake.Display.UI;
using SharpSnake.Input;
using BearLib;

namespace SharpSnake.Game.States
{
    /// <summary>
    /// The options menu state.
    /// </summary>
    public class OptionsState: State
    {
        private readonly Menu Menu;

        public OptionsState(StateContext context): base(context)
        {
            InputMap.AddMapping(Terminal.TK_UP, ActionType.MoveUp);
            InputMap.AddMapping(Terminal.TK_DOWN, ActionType.MoveDown);
            InputMap.AddMapping(Terminal.TK_LEFT, ActionType.MoveLeft);
            InputMap.AddMapping(Terminal.TK_RIGHT, ActionType.MoveRight);
            InputMap.AddMapping(Terminal.TK_ENTER, ActionType.Activate);

            var speedPicker = new Picker<GameSpeed>("Speed", HandleChangeSpeed);
            speedPicker.AddOption("Auto", GameSpeed.Auto);
            speedPicker.AddOption("Slow", GameSpeed.Slow);
            speedPicker.AddOption("Medium", GameSpeed.Medium);
            speedPicker.AddOption("Fast", GameSpeed.Fast);
            speedPicker.SelectOption(Context.Settings.Speed);

            var themePicker = new Picker<PaletteId>("Palette", HandleChangeTheme);
            themePicker.AddOption("Default", PaletteId.Default);
            themePicker.AddOption("Monochrome", PaletteId.Monochrome);
            themePicker.SelectOption(Context.Settings.PaletteId);

            Menu = new Menu();
            Menu.AddItem(themePicker);
            Menu.AddItem(speedPicker);
            Menu.AddItem(new Button("Return", () => RequestPopState()));
        }

        public override void Draw()
        {
            Context.Screen.SetColor(PaletteColor.Snake);
            Context.Screen.Draw(Application.GameTitle, 3, 3);
            Context.Screen.Draw("OPTIONS", 3, 15);
            Menu.Draw(Context.Screen, 3, 17);
        }

        protected override void HandleAction(ActionType action)
        {
            Menu.HandleAction(action);
        }

        private void HandleChangeTheme(PaletteId paletteId)
        {
            Context.Settings.PaletteId = paletteId;
            Context.Screen.Palette = Palette.GetPalette(paletteId);
        }

        private void HandleChangeSpeed(GameSpeed speed)
        {
            Context.Settings.Speed = speed;
        }
    }
}
