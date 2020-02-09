``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  DefaultJob : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT


```
|   Method |     Mean |    Error |   StdDev | Ratio |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|--------- |---------:|---------:|---------:|------:|-------:|-------:|------:|----------:|
| Original | 33.38 us | 0.095 us | 0.085 us |  1.00 | 8.4839 | 0.0610 |     - |  69.78 KB |
|     New1 | 32.37 us | 0.095 us | 0.084 us |  0.97 | 8.7891 | 0.0610 |     - |  71.92 KB |
|     New2 | 32.71 us | 0.071 us | 0.063 us |  0.98 | 8.7891 | 0.0610 |     - |  71.92 KB |
|     New3 | 21.71 us | 0.071 us | 0.063 us |  0.65 | 6.1035 | 0.0305 |     - |  50.02 KB |
|     New4 | 22.53 us | 0.088 us | 0.083 us |  0.67 | 5.2795 |      - |     - |  43.37 KB |
|     New5 | 22.41 us | 0.062 us | 0.058 us |  0.67 | 5.2795 |      - |     - |  43.37 KB |
