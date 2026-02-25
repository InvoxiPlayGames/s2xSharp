using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceIIDX
    {
        private SpiceAPI _api;
        internal SpiceIIDX(SpiceAPI api)
        {
            _api = api;
        }

        public string GetTicker()
        {
            SpiceResponse resp = _api.Command("iidx", "ticker_get");
            return resp.data[0].GetString();
        }

        public void SetTicker(string text)
        {
            JsonElement[] array = new JsonElement[1];
            array[0] = JsonSerializer.Deserialize<JsonElement>($"\"{text}\""); // :(
            _api.Command("iidx", "ticker_set", array);
        }

        public void ResetTicker()
        {
            _api.Command("iidx", "ticker_reset");
        }
    }
}
