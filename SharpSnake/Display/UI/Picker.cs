using System.Collections.Generic;
using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    /// <summary>
    /// Represents a list of pickable options.
    /// </summary>
    /// <typeparam name="T">The type to use for option values</typeparam>
    public class Picker<T>: IMenuItem
    {
        public delegate void ChangeHandler(T selectedOption);

        /// <summary>
        /// The text label that is displayed next to the options.
        /// </summary>
        public string Label
        {
            get;
            private set;
        }

        /// <summary>
        /// The currently selected option.
        /// </summary>
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

        /// <summary>
        /// Initialize a new picker instance.
        /// </summary>
        /// <param name="label">The label to set</param>
        /// <param name="changeHandler">The event handler to call when a new option is selected</param>
        public Picker(string label, ChangeHandler changeHandler)
        {
            Label = label;
            OnChange += changeHandler;
            Options = new List<Option<T>>();
        }

        /// <summary>
        /// Add an option to the picker.
        /// </summary>
        /// <param name="name">The option's name</param>
        /// <param name="value">The option's value</param>
        public void AddOption(string name, T value)
        {
            Options.Add(new Option<T>(name, value));
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

        /// <summary>
        /// Select an option by its value.
        /// </summary>
        /// <param name="value">The value of the option</param>
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

        /// <summary>
        /// Select the next option from the picker.
        /// Selects the first option when the end of list is reached.
        /// </summary>
        public void SelectNextOption()
        {
            SelectedIndex += 1;
            SelectedIndex %= Options.Count;

            OnChange?.Invoke(SelectedOption.Value);
        }

        /// <summary>
        /// Select the previous option from the picker.
        /// Selects the last option when the end of list is reached.
        /// </summary>
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
