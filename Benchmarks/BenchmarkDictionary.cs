using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkDictionary
{
    private Dictionary<int, string> _dictionary = null!;
    
    [Params(1_000, 100_000)]
    public int ItemCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _dictionary = Enumerable.Range(1, ItemCount)
            .ToDictionary(i => i, i => $"Value_{i}");
    }
    
    [Benchmark(Baseline = true)]
    public string? TryGetValue()
    {
        var key = ItemCount / 2;
        _dictionary.TryGetValue(key, out var value);
        return value;
    }
    
    [Benchmark]
    public string? ContainsKeyThenGet()
    {
        var key = ItemCount / 2;
        if (_dictionary.ContainsKey(key))
            return _dictionary[key];
        return null;
    }
    
    [Benchmark]
    public bool ContainsKey()
    {
        var key = ItemCount / 2;
        return _dictionary.ContainsKey(key);
    }
    
    [Benchmark]
    public bool ContainsValue()
    {
        return _dictionary.ContainsValue($"Value_{ItemCount / 2}");
    }
}

