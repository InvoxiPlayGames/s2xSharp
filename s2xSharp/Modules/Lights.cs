using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceLights
    {
        public class Light
        {
            public string name;
            public double state;
            public bool enabled;
        }

        private SpiceAPI _api;
        internal SpiceLights(SpiceAPI api)
        {
            _api = api;
        }

        public Light[] Read()
        {
            SpiceResponse resp = _api.Command("lights", "read");
            Light[] lights = new Light[resp.data.Length];
            for (int i = 0; i < resp.data.Length; i++)
            {
                JsonElement[] lightData = resp.data[i].Deserialize<JsonElement[]>();
                lights[i] = new Light()
                {
                    name = lightData[0].GetString(),
                    state = lightData[1].GetDouble(),
                    enabled = lightData[2].GetBoolean()
                };
            }
            return lights;
        }
    }
}
