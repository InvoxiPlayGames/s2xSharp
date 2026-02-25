using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{
    public class SpiceDRS
    {
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
    }
}
