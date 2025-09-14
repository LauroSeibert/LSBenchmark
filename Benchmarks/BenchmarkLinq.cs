using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkLinq
{
    private List<int> _numbers = null!;
    private List<Person> _people = null!;
    
    [Params(1_000, 100_000)]
    public int ItemCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);
        _numbers = Enumerable.Range(1, ItemCount).ToList();
        _people = Enumerable.Range(1, ItemCount)
            .Select(i => new Person
            {
                Id = i,
                Name = $"Person_{i}",
                Age = random.Next(18, 80),
                Salary = random.Next(30000, 150000),
                IsActive = random.NextDouble() > 0.3
            })
            .ToList();
    }
    
    [Benchmark(Baseline = true)]
    public int Max()
    {
        return _people.Max(p => p.Salary);
    }
    
    [Benchmark]
    public int Min()
    {
        return _people.Min(p => p.Age);
    }
    
    [Benchmark]
    public long Sum()
    {
        return _numbers.Where(x => x % 2 == 0).Sum();
    }
    
    [Benchmark]
    public bool All()
    {
        return _people.All(p => p.Age >= 18);
    }
    
    [Benchmark]
    public List<Person> ComplexLinqQuery()
    {
        return _people
            .Where(p => p.Age > 25)
            .Where(p => p.Salary > 50000)
            .Where(p => p.IsActive)
            .OrderBy(p => p.Age)
            .Take(100)
            .ToList();
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Salary { get; set; }
    public bool IsActive { get; set; }
}
