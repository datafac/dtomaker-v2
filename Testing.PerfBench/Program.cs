using BenchmarkDotNet.Running;

namespace Testing.PerfBench;

public partial class Program
{
    public static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
