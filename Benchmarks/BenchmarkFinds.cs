using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkFinds
{
    private List<int> _numbers = null!;
    private int[] _array = null!;
    
    [Params(10_000, 100_000)]
    public int ItemCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _numbers = Enumerable.Range(1, ItemCount).ToList();
        _array = _numbers.ToArray();
    }
    
    [Benchmark(Baseline = true)]
    public int Find()
    {
        var target = ItemCount / 2;
        return _numbers.Find(x => x == target);
    }
    
    [Benchmark]
    public int FirstOrDefault()
    {
        var target = ItemCount / 2;
        return _numbers.FirstOrDefault(x => x == target);
    }
    
    [Benchmark]
    public int FindWithForLoop()
    {
        var target = ItemCount / 2;
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] == target)
                return _array[i];
        }
        return 0;
    }
    
    [Benchmark]
    public int FindWithForeachLoop()
    {
        var target = ItemCount / 2;
        foreach (var number in _numbers)
        {
            if (number == target)
                return number;
        }
        return 0;
    }
}
