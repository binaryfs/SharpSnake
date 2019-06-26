using System.Collections.Generic;
using BearLib;

namespace SharpSnake.Input
{
    /// <summary>
    /// Represents an input handler that maps low-level inputs to high-level game actions.
    /// </summary>
    public class InputMap
    {
        public delegate void ActionHandler(ActionType action);

        private readonly Dictionary<int, KeyData> Map = new Dictionary<int, KeyData>();

        /// <summary>
        /// Map a given input to a specified game action.
        /// </summary>
        /// <param name="key">The input (a key scancode)</param>
        /// <param name="action">The game action</param>
        public void AddMapping(int key, ActionType action)
        {
            Map.Add(key, new KeyData(action));
        }

        /// <summary>
        /// Send all triggered actions to the given action handler.
        /// </summary>
        /// <param name="handler">The action handler</param>
        public void SendActions(ActionHandler handler)
        {
            foreach (var item in Map)
            {
                item.Value.Release();
            }

            while (Terminal.HasInput())
            {
                int input = Terminal.Read();

                if (Map.ContainsKey(input))
                {
                    var key = Map[input];

                    if (key.Press())
                    {
                        handler(key.Action);
                    }
                }
                else if (input == Terminal.TK_CLOSE)
                {
                    // Special input handling when the user closes the window.
                    handler(ActionType.CloseWindow);
                }
            }
        }
    }
}
