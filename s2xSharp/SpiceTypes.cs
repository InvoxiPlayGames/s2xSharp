using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp
{
    public class SpiceResponse
    {
        public long id { get; set; }
        public JsonElement[] errors { get; set; }
        public JsonElement[] data { get; set; }
    }

    public class SpiceRequest
    {
        public long id { get; set; }
        public string module { get; set; }
        public string function { get; set; }
        public JsonElement[] @params { get; set; }
    }
}
