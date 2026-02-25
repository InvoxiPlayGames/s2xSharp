using s2xSharp.Modules;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace s2xSharp
{
    public class SpiceAPI
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private int _currentid = 0;

        public SpiceCard Card { get; private set; }
        public SpiceCoin Coin { get; private set; }
        public SpiceInfo Info { get; private set; }

        public SpiceAPI(string ip, int port)
        {
            _client = new TcpClient(ip, port);
            _stream = _client.GetStream();

            Card = new SpiceCard(this);
            Coin = new SpiceCoin(this);
            Info = new SpiceInfo(this);
        }

        public SpiceAPI(int port) : this("127.0.0.1", port)
        {

        }

        public SpiceResponse Command(string module, string function, JsonElement[]? @params = null)
        {
            // TODO: we probably want a better way to send parameters to this
            //       since to serialiize stuff into JsonElement you need to do JSON deserializing
            //       i don't know what i'm doing!

            // form the request
            SpiceRequest request = new SpiceRequest()
            {
                id = _currentid,
                module = module,
                function = function
            };
            if (@params == null)
                request.@params = new JsonElement[0];
            else
                request.@params = @params;

            // send off the request
            _currentid++;
            string resptxt = RawMessage(JsonSerializer.Serialize(request));
            SpiceResponse? response = JsonSerializer.Deserialize<SpiceResponse>(resptxt);
            if (response == null)
                throw new Exception("Failed to decode response from server");

            // this should never happen since TCP shouldn't do out-of-order but we'll see
            if (response.id != request.id)
                throw new Exception($"Invalid response ID, got {response.id}, expected {request.id}");

            return response;
        }

        public string RawMessage(string sendmsg)
        {
            // TODO: RC4 encryption
            _stream.Write(Encoding.UTF8.GetBytes(sendmsg + "\0"));

            // TODO: THIS FUCKING SUCKS
            List<byte> respdata = new List<byte>();
            byte lastbyte = (byte)_stream.ReadByte();
            while (lastbyte != 0x00)
            {
                respdata.Add(lastbyte);
                lastbyte = (byte)_stream.ReadByte();
            }

            string recstring = Encoding.UTF8.GetString(respdata.ToArray());
            return recstring;
        }
    }
}
