using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceCoin
    {
        private SpiceAPI _api;
        internal SpiceCoin(SpiceAPI api)
        {
            _api = api;
        }

        public int Get()
        {
            SpiceResponse resp = _api.Command("coin", "get");
            return resp.data[0].GetInt32();
        }

        public void Set(int value)
        {
            JsonElement[] args = new JsonElement[1]
            {
                JsonSerializer.Deserialize<JsonElement>($"{value}")
            };
            _api.Command("coin", "set", args);
        }

        public void Insert(int value)
        {
            JsonElement[] args = new JsonElement[1]
            {
                JsonSerializer.Deserialize<JsonElement>($"{value}")
            };
            _api.Command("coin", "insert", args);
        }

        public bool GetBlocker()
        {
            SpiceResponse resp = _api.Command("coin", "blocker_get");
            return resp.data[0].GetBoolean();
        }
    }
}
