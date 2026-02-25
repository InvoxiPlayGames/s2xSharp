using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceCard
    {
        private SpiceAPI _api;
        internal SpiceCard(SpiceAPI api)
        {
            _api = api;
        }

        public void Insert(int index, string card_id)
        {
            JsonElement[] array = new JsonElement[2];
            array[0] = JsonSerializer.Deserialize<JsonElement>($"{index}");
            array[1] = JsonSerializer.Deserialize<JsonElement>($"\"{card_id}\""); // :(
            _api.Command("card", "insert", array);
        }
    }
}
