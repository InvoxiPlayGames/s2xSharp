using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceDRS
    {
        public enum TouchType : int
        {
            DOWN = 0,
            UP = 1,
            MOVE = 2,
        }

        public class Touch
        {
            public TouchType type;
            public int id;
            public double x;
            public double y;
            public double width;
            public double height;
        }

        private SpiceAPI _api;
        internal SpiceDRS(SpiceAPI api)
        {
            _api = api;
        }

        public int[] GetTapeled()
        {
            SpiceResponse resp = _api.Command("drs", "tapeled_get");
            return resp.data[0].Deserialize<int[]>();
        }

        public void SetTouch(Touch[] touches)
        {
            JsonElement[] array = new JsonElement[touches.Length];
            for (int i = 0; i < touches.Length; i++)
            {
                array[i] = JsonSerializer.Deserialize<JsonElement>($"[{touches[i].type}, {touches[i].id}, {touches[i].x}, {touches[i].y}, {touches[i].width}, {touches[i].height}]"); // I'M SORRY
            }
            _api.Command("drs", "touch_set", array);
        }
    }
}
