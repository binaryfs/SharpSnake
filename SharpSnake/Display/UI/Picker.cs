using System.Collections.Generic;
using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    public class Picker<T>: IMenuItem
    {
        public delegate void ChangeHandler(T selectedOption);

        public string Label
        {
            get;
            private set;
        }

        public Option<T> SelectedOption
        {
            get
            {
                return Options[SelectedIndex];
            }
        }

        private int SelectedIndex;
        private readonly List<Option<T>> Options;
        private event ChangeHandler OnChange;

        public Picker(string label, ChangeHandler changeHandler)
        {
            Label = label;
            OnChange += changeHandler;
            Options = new List<Option<T>>();
        }

        public void AddOption(string name, T value)
        {
            Options.Add(new Option<T>(name, value));
        }

        public void AddOption(string name, T value, T selectedValue)
        {
            AddOption(name, value);

            if (value.Equals(selectedValue))
            {
                SelectedIndex = Options.Count - 1;
            }
        }

        public void HandleAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.MoveLeft:
                    SelectPreviousOption();
                    break;
                case ActionType.MoveRight:
                    SelectNextOption();
                    break;
            }
        }

        public void Draw(ConsoleScreen screen, int left, int top)
        {
            screen.SetColor(PaletteColor.Text);
            screen.Draw(Label, left, top);

            int offset = left + Label.Length + 1;
            screen.SetColor(PaletteColor.Accessory);
            screen.Draw('<', offset, top);

            offset += 1;
            screen.SetColor(PaletteColor.Text);
            screen.Draw(SelectedOption.Label, offset, top);

            offset += SelectedOption.Label.Length;
            screen.SetColor(PaletteColor.Accessory);
            screen.Draw('>', offset, top);
        }

        public void SelectOption(T value)
        {
            int index = 0;

            foreach (var option in Options)
            {
                if (option.Value.Equals(value))
                {
                    SelectedIndex = index;
                    break;
                }

                index += 1;
            }
        }

        public void SelectNextOption()
        {
            SelectedIndex += 1;
            SelectedIndex %= Options.Count;

            OnChange?.Invoke(SelectedOption.Value);
        }

        public void SelectPreviousOption()
        {
            SelectedIndex -= 1;

            if (SelectedIndex < 0)
            {
                SelectedIndex = Options.Count - 1;
            }

            OnChange?.Invoke(SelectedOption.Value);
        }
    }
}
