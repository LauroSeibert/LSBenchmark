# LSBenchmark

Benchmarks de performance em .NET usando o [BenchmarkDotNet](https://benchmarkdotnet.org/). O projeto compara formas diferentes de escrever o mesmo código (LINQ, loops, coleções, strings) e o comportamento entre o **.NET 6** e o **.NET 9**.

## Benchmarks disponíveis

| Classe | O que compara | Tamanhos (`ItemCount`) |
| --- | --- | --- |
| `BenchmarkLinq` | `Max`, `Min`, `Where` + `Sum`, `All` e uma query LINQ encadeada | 1.000 / 100.000 |
| `BenchmarkCollections` | `Contains` em `List`, array e `HashSet`, e `ContainsKey` em `Dictionary` | 1.000 / 10.000 |
| `BenchmarkStrings` | Interpolação, concatenação, `string.Format` e `StringBuilder` | n/a |
| `BenchmarkDictionary` | `TryGetValue`, `ContainsKey` + indexador, `ContainsKey` e `ContainsValue` | 1.000 / 100.000 |
| `BenchmarkCount` | `List.Count`, `Count(predicado)` e `Where(...).Count()` | 1.000 |
| `BenchmarkFinds` | `List.Find`, `FirstOrDefault`, `for` e `foreach` | 10.000 / 100.000 |
| `BenchmarkSum` | `Sum()` do LINQ, `for` e `foreach` | 10.000 |
| `BenchmarkStringSearch` | `Contains`, `Contains` ignorando maiúsculas, `IndexOf` e `StartsWith` | n/a |

Em cada classe, o primeiro método é o *baseline* usado como referência. Todas rodam no .NET 6 (baseline) e no .NET 9, exceto `BenchmarkSum`, que roda apenas no .NET 9.

## Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) e [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022, VS Code ou apenas o terminal

## Como executar

1. Clone o repositório:

   ```bash
   git clone https://github.com/LauroSeibert/LSBenchmark.git
   cd LSBenchmark
   ```

2. No `Program.cs`, escolha o benchmark que deseja rodar (por padrão é o `BenchmarkFinds`):

   ```csharp
   BenchmarkRunner.Run<BenchmarkLinq>();
   ```

3. Execute em **Release**:

   ```bash
   dotnet run -c Release -f net9.0
   ```

   O projeto tem mais de um *target framework*, por isso o `-f` é necessário. Os benchmarks comparam .NET 6 e .NET 9 de qualquer forma, cada versão roda em um processo separado.

   No Visual Studio, selecione **Release** e use **Ctrl+F5** (sem depurador) para resultados mais confiáveis.

## Resultados

Os resultados aparecem no terminal ao final da execução, e os relatórios (Markdown, CSV e HTML) são salvos na pasta `BenchmarkDotNet.Artifacts/results`.

<img width="734" height="116" alt="Exemplo de resultado de benchmark" src="https://github.com/user-attachments/assets/328f616b-44bf-4f71-9c3c-728a8a7bf8ec" />

<img width="803" height="155" alt="Exemplo de resultado de benchmark com comparação entre versões" src="https://github.com/user-attachments/assets/c6b4c589-ce01-4aa3-a7bb-ec35a1704a57" />

### Como ler a tabela

| Coluna | Significado |
| --- | --- |
| `Mean` | Média aritmética do tempo das execuções |
| `Error` / `StdDev` | Margem de erro e desvio padrão das medições |
| `Ratio` | Tempo em relação ao baseline (`1.00` = igual, abaixo de `1` = mais rápido) |
| `Allocated` | Memória alocada por operação |
| `Gen0` | Coletas de lixo da geração 0 a cada 1.000 operações |

## Adicionando um benchmark

Crie uma classe em `Benchmarks/` e chame-a no `Program.cs`:

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace LSBenchmark.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class BenchmarkExemplo
{
    [Benchmark(Baseline = true)]
    public int Original() => /* implementação de referência */ 0;

    [Benchmark]
    public int Alternativa() => /* implementação a comparar */ 0;
}
```

```csharp
BenchmarkRunner.Run<BenchmarkExemplo>();
```

## Licença

Distribuído sob a licença [MIT](LICENSE).
