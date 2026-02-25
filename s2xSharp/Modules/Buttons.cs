using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceButtons
    {
        public class Button
        {
            public string name;
            public double state;
            public bool enabled;
        }

        private SpiceAPI _api;
        internal SpiceButtons(SpiceAPI api)
        {
            _api = api;
        }

        public Button[] Read()
        {
            SpiceResponse resp = _api.Command("buttons", "read");
            Button[] buttons = new Button[resp.data.Length];
            for (int i = 0; i < resp.data.Length; i++)
            {
                JsonElement[] buttonData = resp.data[i].Deserialize<JsonElement[]>();
                buttons[i] = new Button()
                {
                    name = buttonData[0].GetString(),
                    state = buttonData[1].GetDouble(),
                    enabled = buttonData[2].GetBoolean()
                };
            }
            return buttons;
        }
    }
}
