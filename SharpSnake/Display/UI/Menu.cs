using System.Collections.Generic;
using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    public class Menu
    {
        public IMenuItem SelectedItem
        {
            get
            {
                return Items[SelectedIndex];
            }
        }

        private int SelectedIndex = 0;
        private readonly List<IMenuItem> Items = new List<IMenuItem>();

        public void AddItem(IMenuItem item)
        {
            Items.Add(item);
        }

        public void HandleAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.MoveUp:
                    SelectPreviousItem();
                    break;
                case ActionType.MoveDown:
                    SelectNextItem();
                    break;
            }

            SelectedItem.HandleAction(action);
        }

        public void Draw(ConsoleScreen screen, int left, int top)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == SelectedItem)
                {
                    screen.SetColor(PaletteColor.Highlight);
                    screen.Draw(">", left, top + i);
                }

                Items[i].Draw(screen, left + 2, top + i);
            }
        }

        public void SelectItem(int index)
        {
            SelectedIndex = index;
        }

        public void SelectNextItem()
        {
            SelectedIndex += 1;
            SelectedIndex %= Items.Count;
        }

        public void SelectPreviousItem()
        {
            SelectedIndex -= 1;

            if (SelectedIndex < 0)
            {
                SelectedIndex = Items.Count - 1;
            }
        }
    }
}
