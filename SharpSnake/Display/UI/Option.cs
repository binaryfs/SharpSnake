namespace SharpSnake.Display.UI
{
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
