using s2xSharp;
using s2xSharp.Modules;
using System.Text.Json;

namespace s2xSharp.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpiceAPI api = new(8078);
            Console.WriteLine("Connected!");

            SpiceInfo.Launcher launcher = api.Info.GetLauncher();
            Console.WriteLine($"Launcher: {launcher.version} ({launcher.compile_date} {launcher.compile_time})");

            SpiceInfo.AVS avsInfo = api.Info.GetAVS();
            Console.WriteLine($"Game: {avsInfo.model}:{avsInfo.dest}:{avsInfo.spec}:{avsInfo.rev}:{avsInfo.ext}");

            bool coinblocked = api.Coin.GetBlocker();
            Console.WriteLine($"Coin blocker: {coinblocked}");
            Console.WriteLine($"Coin stock: {api.Coin.Get()}");
            if (!coinblocked)
            {
                Console.WriteLine("Inserting coins...");
                api.Coin.Insert(1);
                Console.WriteLine($"Coin stock: {api.Coin.Get()}");
            }

            Console.WriteLine("Inserting card...");
            api.Card.Insert(0, "E004010000000000");

            Console.WriteLine("Lights:");
            SpiceLights.Light[] lights = api.Lights.Read();
            foreach(var light in lights)
            {
                Console.WriteLine($"  {light.name} = {light.state} ({light.enabled})");
            }

            Console.WriteLine("Buttons:");
            SpiceButtons.Button[] buttons = api.Buttons.Read();
            foreach (var button in buttons)
            {
                Console.WriteLine($"  {button.name} = {button.state} ({button.enabled})");
            }
        }
    }
}
