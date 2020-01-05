using BenchmarkDotNet.Running;

namespace dwyl.GetMgpExpectation.Benchmark
{
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<Benchmark_2019_11_09>();
        }
    }
}
