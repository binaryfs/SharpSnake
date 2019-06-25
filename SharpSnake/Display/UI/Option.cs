namespace SharpSnake.Display.UI
{
    /// <summary>
    /// Represents a selectable option of a <see cref="Picker"/> instance.
    /// </summary>
    /// <typeparam name="T">The type to use for the option's value</typeparam>
    public struct Option<T>
    {
        public string Label
        {
            get;
            private set;
        }

        public T Value
        {
            get;
            private set;
        }

        public Option(string label, T value)
        {
            Label = label;
            Value = value;
        }
    }
}
