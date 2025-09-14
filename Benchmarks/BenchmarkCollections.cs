using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkCollections
{
    private List<int> _list = null!;
    private int[] _array = null!;
    private HashSet<int> _hashSet = null!;
    private Dictionary<int, string> _dictionary = null!;
    
    [Params(1_000, 10_000)]
    public int ItemCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        var items = Enumerable.Range(1, ItemCount).ToArray();
        _list = new List<int>(items);
        _array = items;
        _hashSet = new HashSet<int>(items);
        _dictionary = items.ToDictionary(x => x, x => $"Value_{x}");
    }
    
    [Benchmark(Baseline = true)]
    public bool ListContains()
    {
        var target = ItemCount / 2;
        return _list.Contains(target);
    }
    
    [Benchmark]
    public bool ArrayContains()
    {
        var target = ItemCount / 2;
        return _array.Contains(target);
    }
    
    [Benchmark]
    public bool HashSetContains()
    {
        var target = ItemCount / 2;
        return _hashSet.Contains(target);
    }
    
    [Benchmark]
    public bool DictionaryContainsKey()
    {
        var target = ItemCount / 2;
        return _dictionary.ContainsKey(target);
    }
}
