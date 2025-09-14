using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkStrings
{
    private readonly string _name = "John Doe";
    private readonly int _age = 30;
    private readonly decimal _salary = 75000.50m;
    
    [Benchmark(Baseline = true)]
    public string StringInterpolation()
    {
        return $"Employee: {_name}, Age: {_age}, Salary: {_salary:C}";
    }
    
    [Benchmark]
    public string StringConcatenation()
    {
        return "Employee: " + _name + ", Age: " + _age + ", Salary: " + _salary.ToString("C");
    }
    
    [Benchmark]
    public string StringFormat()
    {
        return string.Format("Employee: {0}, Age: {1}, Salary: {2:C}", _name, _age, _salary);
    }
    
    [Benchmark]
    public string StringBuilderAppend()
    {
        var sb = new StringBuilder();
        sb.Append("Employee: ");
        sb.Append(_name);
        sb.Append(", Age: ");
        sb.Append(_age);
        sb.Append(", Salary: ");
        sb.Append(_salary.ToString("C"));
        return sb.ToString();
    }
}
