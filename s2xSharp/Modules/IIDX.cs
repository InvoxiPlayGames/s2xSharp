using System;
using System.Collections.Generic;
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
    }
}
