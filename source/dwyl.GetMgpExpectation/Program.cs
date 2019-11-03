using System.Threading.Tasks;
using MicroBatchFramework;

namespace dwyl.GetMgpExpectation
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await BatchHost.CreateDefaultBuilder().RunBatchEngineAsync<GetMgpExpectation>(args);
        }
    }
}
