using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceKeypads
    {
        private SpiceAPI _api;
        internal SpiceKeypads(SpiceAPI api)
        {
            _api = api;
        }

        public void Write(int keypad, string input)
        {
            JsonElement[] array = new JsonElement[2];
            array[0] = JsonSerializer.Deserialize<JsonElement>($"{keypad}");
            array[1] = JsonSerializer.Deserialize<JsonElement>($"\"{input}\""); // :(
            _api.Command("keypads", "write", array);
        }

        public void Set(int keypad, char[] input)
        {
            JsonElement[] array = new JsonElement[1 + input.Length];
            array[0] = JsonSerializer.Deserialize<JsonElement>($"{keypad}");
            for (int i = 0; i < input.Length; i++)
                array[1 + i] = JsonSerializer.Deserialize<JsonElement>($"\"{input[i]}\"");
            _api.Command("keypads", "set", array);
        }

        public char[] Get(int keypad)
        {
            JsonElement[] array = new JsonElement[1]
            {
                JsonSerializer.Deserialize<JsonElement>($"{keypad}")
            };
            SpiceResponse resp = _api.Command("keypads", "get", array);
            char[] keys = new char[resp.data.Length];
            for (int i = 0; i < resp.data.Length; i++)
            {
                keys[i] = resp.data[i].GetString()[0];
            }
            return keys;
        }
    }
}
