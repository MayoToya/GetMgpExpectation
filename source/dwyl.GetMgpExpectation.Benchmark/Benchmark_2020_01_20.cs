using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dwyl.GetMgpExpectation.Benchmark
{
    [RPlotExporter, MemoryDiagnoser]
    // ReSharper disable once InconsistentNaming
    public class Benchmark_2020_01_20
    {
        private IEnumerable<int> _data;
        private Consumer _consumer;

        [GlobalSetup]
        public void Setup()
        {
            this._data = Enumerable.Range(1, 5);
            this._consumer = new Consumer();
        }

        [Benchmark(Baseline = true)]
        public void Original() => GetPermutationEnumerable(this._data).Consume(this._consumer);

        private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable(IEnumerable<int> intEnumerable)
        {
            var array = intEnumerable.ToArray();

            switch (array.Length)
            {
                case 0:
                case 1:
                    yield return array;
                    break;

                case 2:
                    yield return array;
                    yield return array.Reverse();
                    break;

                default:
                    foreach (var number in array)
                    {
                        var first = new[] { number };
                        var children = GetPermutationEnumerable(array.Except(first));

                        foreach (var child in children)
                        {
                            yield return first.Concat(child);
                        }
                    }

                    break;
            }

        }


        private static IEnumerable<int> FirstAndArray(int first, IEnumerable<int> array)
        {
            yield return first;

            foreach (var number in array)
            {
                yield return number;
            }
        }

        [Benchmark]
        public void New1() => GetPermutationEnumerable2(this._data).Consume(this._consumer);

        private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable2(IEnumerable<int> intEnumerable)
        {
            var array = intEnumerable.ToArray();

            switch (array.Length)
            {
                case 0:
                case 1:
                    yield return array;
                    break;

                case 2:
                    yield return array;
                    yield return array.Reverse();
                    break;

                default:
                    foreach (var number in array)
                    {
                        var first = new[] { number };
                        var children = GetPermutationEnumerable2(array.Except(first));

                        foreach (var child in children)
                        {
                            yield return FirstAndArray(number, child);
                        }
                    }
                    break;
            }
        }


        [Benchmark]
        public void New2() => GetPermutationEnumerable3(this._data).Consume(this._consumer);

        private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable3(IEnumerable<int> intEnumerable)
        {
            var array = intEnumerable.ToArray();

            static IEnumerable<int> FirstAndArray2(int first, IEnumerable<int> array)
            {
                yield return first;

                foreach (var number in array)
                {
                    yield return number;
                }
            }


            switch (array.Length)
            {
                case 0:
                case 1:
                    yield return array;
                    break;

                case 2:
                    yield return array;
                    yield return array.Reverse();
                    break;

                default:
                    foreach (var number in array)
                    {
                        var first = new[] { number };
                        var children = GetPermutationEnumerable3(array.Except(first));

                        foreach (var child in children)
                        {
                            yield return FirstAndArray2(number, child);
                        }
                    }
                    break;
            }
        }

        [Benchmark]
        public void New3() => GetPermutationEnumerable4(this._data).Consume(this._consumer);

        private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable4(IEnumerable<int> intEnumerable)
        {
            var array = intEnumerable.ToArray();

            static IEnumerable<int> FirstAndArray2(int first, IEnumerable<int> array)
            {
                yield return first;

                foreach (var number in array)
                {
                    yield return number;
                }
            }


            switch (array.Length)
            {
                case 0:
                case 1:
                    yield return array;
                    break;

                case 2:
                    yield return array;
                    yield return array.Reverse();
                    break;

                default:
                    foreach (var number in array)
                    {
                        var children = GetPermutationEnumerable4(array.Where(x => x != number));

                        foreach (var child in children)
                        {
                            yield return FirstAndArray2(number, child);
                        }
                    }
                    break;
            }
        }


        [Benchmark]
        public void New4() => GetPermutationEnumerable5(this._data).Consume(this._consumer);

        private static IEnumerable<int> RemoveFirst(int first, int[] array)
        {
            foreach (var number in array)
            {
                if (number != first)
                {
                    yield return number;
                }
            }
        }

        private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable5(IEnumerable<int> intEnumerable)
        {
            var array = intEnumerable.ToArray();

            static IEnumerable<int> FirstAndArray2(int first, IEnumerable<int> array)
            {
                yield return first;

                foreach (var number in array)
                {
                    yield return number;
                }
            }


            switch (array.Length)
            {
                case 0:
                case 1:
                    yield return array;
                    break;

                case 2:
                    yield return array;
                    yield return array.Reverse();
                    break;

                default:
                    foreach (var number in array)
                    {
                        var children = GetPermutationEnumerable5(RemoveFirst(number, array));

                        foreach (var child in children)
                        {
                            yield return FirstAndArray2(number, child);
                        }
                    }
                    break;
            }
        }


        [Benchmark]
        public void New5() => GetPermutationEnumerable6(this._data).Consume(this._consumer);

        private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable6(IEnumerable<int> intEnumerable)
        {
            var array = intEnumerable.ToArray();

            static IEnumerable<int> FirstAndArray2(int first, IEnumerable<int> array)
            {
                yield return first;

                foreach (var number in array)
                {
                    yield return number;
                }
            }

            static IEnumerable<int> RemoveFirst2(int first, int[] array)
            {
                foreach (var number in array)
                {
                    if (number != first)
                    {
                        yield return number;
                    }
                }
            }

            switch (array.Length)
            {
                case 0:
                case 1:
                    yield return array;
                    break;

                case 2:
                    yield return array;
                    yield return array.Reverse();
                    break;

                default:
                    foreach (var number in array)
                    {
                        var children = GetPermutationEnumerable6(RemoveFirst2(number, array));

                        foreach (var child in children)
                        {
                            yield return FirstAndArray2(number, child);
                        }
                    }
                    break;
            }
        }

        //[Benchmark]
        //public void New1() => GetPermutationEnumerable2(this._data).Consume(this._consumer);

        //private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable2(IEnumerable<int> intEnumerable)
        //{
        //    var array = intEnumerable.ToArray();

        //    static IEnumerable<int> RemoveFirst(int first, int[] array)
        //    {
        //        foreach (var number in array)
        //        {
        //            if (number != first)
        //            {
        //                yield return number;
        //            }
        //        }
        //    }

        //    switch (array.Length)
        //    {
        //        case 0:
        //        case 1:
        //            yield return array;
        //            break;

        //        case 2:
        //            yield return array;
        //            yield return array.Reverse();
        //            break;

        //        default:
        //            foreach (var first in array)
        //            {
        //                foreach (var child in GetPermutationEnumerable2(RemoveFirst(first, array)))
        //                {
        //                    yield return FirstAndArray(first, child);
        //                }
        //            }
        //            break;
        //    }

        //}

        //[Benchmark]
        //public void New2() => GetPermutationEnumerable3(this._data).Consume(this._consumer);

        //private static IEnumerable<IEnumerable<int>> GetPermutationEnumerable3(IEnumerable<int> intEnumerable)
        //{
        //    var array = intEnumerable.ToArray();

        //    switch (array.Length)
        //    {
        //        case 0:
        //        case 1:
        //            yield return array;
        //            break;

        //        case 2:
        //            yield return array;
        //            yield return array.Reverse();
        //            break;

        //        default:
        //            foreach (var first in array)
        //            {
        //                foreach (var child in GetPermutationEnumerable3(array.Where(x => x != first)))
        //                {
        //                    yield return FirstAndArray(first, child);
        //                }
        //            }
        //            break;
        //    }
        //}


    }
}
