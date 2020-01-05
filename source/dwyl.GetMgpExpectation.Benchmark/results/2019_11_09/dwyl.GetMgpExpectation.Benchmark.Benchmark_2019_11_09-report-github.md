``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  DefaultJob : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT


```
|                                   Method |     Mean |   Error |  StdDev |   Median | Ratio | RatioSD |    Gen 0 |   Gen 1 | Gen 2 | Allocated |
|----------------------------------------- |---------:|--------:|--------:|---------:|------:|--------:|---------:|--------:|------:|----------:|
|                                 Original | 460.6 us | 3.15 us | 2.63 us | 460.7 us |  1.00 |    0.00 | 116.2109 | 23.4375 |     - | 934.96 KB |
|                         RemoveAsParallel | 171.6 us | 3.43 us | 7.74 us | 168.5 us |  0.39 |    0.02 |  99.8535 | 16.6016 |     - | 815.66 KB |
|                           UseUnsafeClass | 455.4 us | 1.57 us | 1.47 us | 455.2 us |  0.99 |    0.01 | 106.4453 | 20.5078 |     - |    851 KB |
|           RemoveAsParallelUseUnsafeClass | 125.0 us | 2.39 us | 2.93 us | 124.9 us |  0.27 |    0.01 |  89.4775 | 14.8926 |     - | 731.62 KB |
| RemoveAsParallelUseUnsafeClassUseSetByte | 186.9 us | 1.69 us | 1.58 us | 187.0 us |  0.41 |    0.00 |  89.3555 | 14.8926 |     - | 731.62 KB |
|           RemoveAsParallelUseUnsafeBlock | 185.0 us | 3.66 us | 5.70 us | 185.9 us |  0.40 |    0.01 |  99.8535 | 16.3574 |     - | 815.99 KB |
| RemoveAsParallelUseUnsafeBlockUseSetByte | 232.4 us | 2.45 us | 2.29 us | 231.6 us |  0.50 |    0.01 |  99.8535 | 16.3574 |     - | 815.99 KB |
