# Benchmarks de Performance .NET

Este projeto foi criado para testar a performance de diferentes versões do .NET e diferentes formas de implementar código.

## Classes de Benchmark

- **BenchmarkLinq** - Comparação de operações LINQ
- **BenchmarkCollections** - Performance de busca em coleções
- **BenchmarkStrings** - Métodos de manipulação de strings
- **BenchmarkDictionary** - Padrões de acesso a dicionários
- **BenchmarkCount** - Count vs Length vs Count()
- **BenchmarkFinds** - Diferentes abordagens de busca
- **BenchmarkSum** - Operações de soma
- **BenchmarkStringSearch** - Métodos de busca em strings

## Como Usar

1. Clone o repositório
2. Abra o projeto no Visual Studio
3. Configure para **Release**
4. No arquivo `Program.cs`, selecione o benchmark que deseja executar:

```csharp
// Mude esta linha para executar diferentes benchmarks
BenchmarkRunner.Run<BenchmarkLinq>();
```

5. Execute com F5 ou Ctrl+F5

## Pré-requisitos

- .NET 6.0 SDK
- .NET 9.0 SDK
- Visual Studio 2022 ou VS Code

## Resultados

Os resultados são exibidos automaticamente no terminal após executar os benchmarks.

## Exemplos

<img width="734" height="116" alt="image" src="https://github.com/user-attachments/assets/328f616b-44bf-4f71-9c3c-728a8a7bf8ec" />

<img width="803" height="155" alt="image" src="https://github.com/user-attachments/assets/c6b4c589-ce01-4aa3-a7bb-ec35a1704a57" />

`Mean` indica a média aritmética das execuções

## Licença

Este projeto está disponível sob a Licença MIT.
