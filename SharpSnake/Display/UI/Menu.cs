using System.Collections.Generic;
using SharpSnake.Input;

namespace SharpSnake.Display.UI
{
    /// <summary>
    /// Represents a list of selectable menu items.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Get the currently selected item.
        /// </summary>
        public IMenuItem SelectedItem
        {
            get
            {
                return Items[SelectedIndex];
            }
        }

        private int SelectedIndex = 0;
        private readonly List<IMenuItem> Items = new List<IMenuItem>();

        /// <summary>
        /// Add an item to the menu.
        /// </summary>
        /// <param name="item">The item to add</param>
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

        /// <summary>
        /// Draw the menu at the given screen position.
        /// </summary>
        /// <param name="screen">The screen to draw on</param>
        /// <param name="left">Position along X-axis in columns</param>
        /// <param name="top">Position along Y-axis in rows</param>
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

        /// <summary>
        /// Select the item at the given index.
        /// </summary>
        /// <param name="index">Item index</param>
        public void SelectItem(int index)
        {
            SelectedIndex = index;
        }

        /// <summary>
        /// Select the next menu item.
        /// Selects the first option when the end of list is reached.
        /// </summary>
        public void SelectNextItem()
        {
            SelectedIndex += 1;
            SelectedIndex %= Items.Count;
        }

        /// <summary>
        /// Select the previous menu item.
        /// Selects the last option when the end of list is reached.
        /// </summary>
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
