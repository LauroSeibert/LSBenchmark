using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkCount
{
    private List<int> _list = null!;
    
    [Params(1_000)]
    public int ItemCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        var items = Enumerable.Range(1, ItemCount).ToArray();
        _list = new List<int>(items);
    }
    
    [Benchmark(Baseline = true)]
    public int ListCount()
    {
        return _list.Count;
    }
    
    [Benchmark]
    public int ListCountPredicate()
    {
        return _list.Count(q => q > ItemCount / 2);
    }

    [Benchmark]
    public int ListWherePredicateCount()
    {
        return _list.Where(q => q > ItemCount / 2).Count();
    }
}

