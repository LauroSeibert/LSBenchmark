using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkStringSearch
{
    private string _text = null!;
    private readonly string _searchTerm = "performance";
    
    [GlobalSetup]
    public void Setup()
    {
        _text = "This is a sample text for performance testing. " +
                "We are looking for performance improvements in .NET versions. " +
                "Performance benchmarking helps identify bottlenecks.";
    }
    
    [Benchmark(Baseline = true)]
    public bool Contains()
    {
        return _text.Contains(_searchTerm);
    }
    
    [Benchmark]
    public bool ContainsIgnoreCase()
    {
        return _text.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase);
    }
    
    [Benchmark]
    public int IndexOf()
    {
        return _text.IndexOf(_searchTerm);
    }
    
    [Benchmark]
    public bool StartsWith()
    {
        return _text.StartsWith("This");
    }
}

