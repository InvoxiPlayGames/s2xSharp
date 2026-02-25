using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace s2xSharp.Modules
{

    public class SpiceInfo
    {
        public class AVS
        {
            public string model { get; set; }
            public string dest { get; set; }
            public string spec { get; set; }
            public string rev { get; set; }
            public string ext { get; set; }
            public string services { get; set; }
        }

        public class Launcher
        {
            public string version { get; set; }
            public string compile_date { get; set; }
            public string compile_time { get; set; }
            public string system_time { get; set; }
            public string[] args { get; set; }
        }

        public class Memory
        {
            public long mem_total { get; set; }
            public long mem_total_used { get; set; }
            public long mem_used { get; set; }
            public long vmem_total { get; set; }
            public long vmem_total_used { get; set; }
            public long vmem_used { get; set; }
        }

        private SpiceAPI _api;
        internal SpiceInfo(SpiceAPI api)
        {
            _api = api;
        }

        public AVS GetAVS()
        {
            SpiceResponse resp = _api.Command("info", "avs");
            return resp.data[0].Deserialize<AVS>();
        }

        public Launcher GetLauncher()
        {
            SpiceResponse resp = _api.Command("info", "launcher");
            return resp.data[0].Deserialize<Launcher>();
        }

        public Memory GetMemory()
        {
            SpiceResponse resp = _api.Command("info", "memory");
            return resp.data[0].Deserialize<Memory>();
        }
    }
}
