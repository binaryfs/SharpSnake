using System;
using System.Collections.Generic;
using System.Windows.Input;
using BearLib;

namespace SharpSnake.Input
{
    public class InputMap
    {
        public delegate void ActionHandler(ActionType action);

        private Dictionary<int, KeyData> Map
        {
            get;
        }

        public InputMap()
        {
            Map = new Dictionary<int, KeyData>();
        }

        public void AddMapping(int key, ActionType action)
        {
            Map.Add(key, new KeyData(action));
        }

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
                    handler(ActionType.CloseWindow);
                }
            }
        }
    }
}
