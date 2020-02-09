using System.Threading.Tasks;
using MicroBatchFramework;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;

namespace dwyl.GetMgpExpectation
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<GetMgpExpectation>(args);
        }
    }
}
