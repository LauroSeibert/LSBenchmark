using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkSum
{
    private int[] _array = null!;
    private List<int> _list = null!;
    
    [Params(10_000)]
    public int ItemCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _array = Enumerable.Range(1, ItemCount).ToArray();
        _list = new List<int>(_array);
    }

    [Benchmark(Baseline = true)]
    public long LinqSum()
    {
        return _list.Sum();
    }

    [Benchmark]
    public long ForLoop()
    {
        long sum = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            sum += _array[i];
        }
        return sum;
    }
    
    [Benchmark]
    public long ForeachLoop()
    {
        long sum = 0;
        foreach (var item in _array)
        {
            sum += item;
        }
        return sum;
    }
}
