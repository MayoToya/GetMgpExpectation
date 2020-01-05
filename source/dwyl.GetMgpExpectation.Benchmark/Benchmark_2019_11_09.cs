using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace dwyl.GetMgpExpectation.Benchmark
{
    [RPlotExporter, MemoryDiagnoser]
    // ReSharper disable once InconsistentNaming
    public class Benchmark_2019_11_09
    {
        private string[] _data;

        [GlobalSetup]
        public void Setup()
        {
            var sample = new[] {
                "007048002"
                ,
                "000320190"
                ,
                "006041003"
                ,
                "179000008"
                ,
                "000304890"
                ,
                "002650900"
                };

            this._data = Enumerable.Repeat(sample, 1).SelectMany(x => x).ToArray();
        }

        [Benchmark(Baseline = true)]
        public (int, int, int, int, int, int, int, int)[][] Original() => this._data.Select(PrivateOriginal).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int, int, int, int, int, int, int, int)[] PrivateOriginal(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayFromInput, hiddenNumbers) = ParseInput(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .AsParallel()
                .Select(x => GetValidArray(intArrayFromInput, x).Calculate())
                .ToArray();
        }

        [Benchmark]
        public (int, int, int, int, int, int, int, int)[][] RemoveAsParallel() => _data.Select(PrivateOriginalWithoutParallel).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int, int, int, int, int, int, int, int)[] PrivateOriginalWithoutParallel(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayFromInput, hiddenNumbers) = ParseInput(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .Select(x => GetValidArray(intArrayFromInput, x).Calculate())
                .ToArray();
        }

        #region methods for original        
        private static (int[], int[] hiddenNumbers) ParseInput(ReadOnlySpan<char> span)
        {
            var oneToNine = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var array = new int[9];
            var counter = 0;

            foreach (var c in span)
            {
                if (c < 48 || 57 < c)
                {
                    throw new InvalidDataException();
                }

                array[counter] = c - 48;
                counter++;
            }

            return (array, oneToNine.Except(array).ToArray());
        }

        private static int[] GetValidArray(in Span<int> value, in Span<int> correction)
        {
            if (value.Length != 9 || correction.Length != 5)
            {
                throw new InvalidDataException();
            }

            var counter = 0;
            var array = new int[9];

            for (var i = 0; i < 9; i++)
            {

                if (value[i] != 0)
                {
                    array[i] = value[i];
                }
                else
                {
                    array[i] = correction[counter];
                    counter++;
                }
            }

            return array;
        }
        #endregion


        [Benchmark]
        public (int, int, int, int, int, int, int, int)[][] UseUnsafeClass() => this._data.Select(PrivateNewMethod).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int, int, int, int, int, int, int, int)[] PrivateNewMethod(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayInput, hiddenNumbers, hiddenNumbersPositions) = ParseInputForUnsafe(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .AsParallel()
                .Select(x => GetValidArrayUsingUnsafeClass(intArrayInput, x, hiddenNumbersPositions).Calculate2())
                .ToArray();
        }

        [Benchmark]
        public (int, int, int, int, int, int, int, int)[][] RemoveAsParallelUseUnsafeClass() => this._data.Select(PrivateNewParallelMethodWithoutParallel).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping)[] PrivateNewParallelMethodWithoutParallel(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayInput, hiddenNumbers, hiddenNumbersPositions) = ParseInputForUnsafe(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .Select(x => GetValidArrayUsingUnsafeClass(intArrayInput, x, hiddenNumbersPositions).Calculate2())
                .ToArray();
        }

        [Benchmark]
        public (int, int, int, int, int, int, int, int)[][] RemoveAsParallelUseUnsafeClassUseSetByte() => this._data.Select(PrivateNewParallelMethodWithoutParallel3).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping)[] PrivateNewParallelMethodWithoutParallel3(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayInput, hiddenNumbers, hiddenNumbersPositions) = ParseInputForUnsafe(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .Select(x => GetValidArrayUsingUnsafeClass2(intArrayInput, x, hiddenNumbersPositions).Calculate2())
                .ToArray();
        }

        [Benchmark]
        public (int, int, int, int, int, int, int, int)[][] RemoveAsParallelUseUnsafeBlock() => this._data.Select(PrivateNewParallelMethodWithoutParallel4).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping)[] PrivateNewParallelMethodWithoutParallel4(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayInput, hiddenNumbers, hiddenNumbersPositions) = ParseInputForUnsafe(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .Select(x => GetValidArrayUsingUnsafeBlock2(intArrayInput, x, hiddenNumbersPositions).Calculate())
                .ToArray();
        }


        [Benchmark]
        public (int, int, int, int, int, int, int, int)[][] RemoveAsParallelUseUnsafeBlockUseSetByte() => this._data.Select(PrivateNewParallelMethodWithoutParallel2).ToArray();

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping)[] PrivateNewParallelMethodWithoutParallel2(string value)
        {
            var numbers = value.AsSpan();

            if (numbers.Length != 9)
            {
                throw new InvalidDataException();
            }

            var (intArrayInput, hiddenNumbers, hiddenNumbersPositions) = ParseInputForUnsafe(numbers);

            return GetPermutationEnumerable(hiddenNumbers)
                .Select(x => GetValidArrayUsingUnsafeBlock(intArrayInput, x, hiddenNumbersPositions).Calculate())
                .ToArray();
        }

        #region methods for new methods
        private static (int[], int[] hiddenNumbers, int[] hiddenNumbersPositions) ParseInputForUnsafe(ReadOnlySpan<char> span)
        {
            var oneToNine = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var array = new int[9];
            var hiddenNumbersPositions = new int[5];
            var counter = 0;
            var hiddenNumberCounter = 0;

            foreach (var c in span)
            {

                if (c < 48 || 57 < c)
                {
                    throw new InvalidDataException();
                }

                if (c == 48)
                {
                    hiddenNumbersPositions[hiddenNumberCounter] = counter;
                    hiddenNumberCounter++;
                }
                else
                {
                    array[counter] = c - 48;
                }

                counter++;
            }

            return (array, oneToNine.Except(array).ToArray(), hiddenNumbersPositions);
        }

        private static byte[] GetValidArrayUsingUnsafeClass(int[] value, in Span<int> correction, in Span<int> hiddenNumbersPositions)
        {
            if (value.Length != 9 || correction.Length != 5)
            {
                throw new InvalidDataException();
            }

            var array = new byte[36];
            ref var b = ref Unsafe.As<int[], byte[]>(ref value);
            Unsafe.CopyBlock(ref array[0], ref b[0], 36);

            for (var i = 0; i < correction.Length; i++)
            {
                array[hiddenNumbersPositions[i] * 4] = (byte)correction[i];
            }

            return array;
        }

        private static byte[] GetValidArrayUsingUnsafeClass2(int[] value, in int[] correction, in Span<int> hiddenNumbersPositions)
        {
            if (value.Length != 9 || correction.Length != 5)
            {
                throw new InvalidDataException();
            }

            var array = new byte[36];
            var b = Unsafe.As<int[], byte[]>(ref value);
            Unsafe.CopyBlock(ref array[0], ref b[0], 36);

            for (var i = 0; i < 5; i++)
            {
                Buffer.SetByte(array, (hiddenNumbersPositions[i] * 4), Buffer.GetByte(correction, i * 4));
            }

            return array;
        }

        private static int[] GetValidArrayUsingUnsafeBlock2(in Span<int> value, in Span<int> correction, in Span<int> hiddenNumbersPositions)
        {
            if (value.Length != 9 || correction.Length != 5)
            {
                throw new InvalidDataException();
            }

            var intArray = new int[9];

            unsafe
            {
                fixed (void* ptrSource = value, ptrDest = intArray)
                {
                    Buffer.MemoryCopy(ptrSource, ptrDest, Buffer.ByteLength(intArray), 36);
                }
            }

            for (var i = 0; i < 5; i++)
            {
                intArray[hiddenNumbersPositions[i]] = correction[i];
            }

            return intArray;
        }

        private static int[] GetValidArrayUsingUnsafeBlock(in Span<int> value, in int[] correction, in Span<int> hiddenNumbersPositions)
        {
            if (value.Length != 9 || correction.Length != 5)
            {
                throw new InvalidDataException();
            }

            var intArray = new int[9];

            unsafe
            {
                // 配列のポインタを取得する
                fixed (void* ptrSource = value, ptrDest = intArray)
                {
                    Buffer.MemoryCopy(ptrSource, ptrDest, Buffer.ByteLength(intArray), 36);
                }
            }

            for (var i = 0; i < 5; i++)
            {
                Buffer.SetByte(intArray, (hiddenNumbersPositions[i] * 4), Buffer.GetByte(correction, i * 4));
            }

            return intArray;
        }
        #endregion

        // ReSharper disable once SuggestBaseTypeForParameter
        private static IEnumerable<int[]> GetPermutationEnumerable(int[] hiddenNumbers)
        {
            if (hiddenNumbers.Length != 5)
            {
                throw new InvalidDataException();
            }

            var first = hiddenNumbers[0];
            var second = hiddenNumbers[1];
            var third = hiddenNumbers[2];
            var fourth = hiddenNumbers[3];
            var fifth = hiddenNumbers[4];

            yield return new[] { first, second, third, fourth, fifth };
            yield return new[] { first, second, third, fifth, fourth };
            yield return new[] { first, second, fourth, third, fifth };
            yield return new[] { first, second, fourth, fifth, third };
            yield return new[] { first, second, fifth, third, fourth };
            yield return new[] { first, second, fifth, fourth, third };
            yield return new[] { first, third, second, fourth, fifth };
            yield return new[] { first, third, second, fifth, fourth };
            yield return new[] { first, third, fourth, second, fifth };
            yield return new[] { first, third, fourth, fifth, second };
            yield return new[] { first, third, fifth, second, fourth };
            yield return new[] { first, third, fifth, fourth, second };
            yield return new[] { first, fourth, second, third, fifth };
            yield return new[] { first, fourth, second, fifth, third };
            yield return new[] { first, fourth, third, second, fifth };
            yield return new[] { first, fourth, third, fifth, second };
            yield return new[] { first, fourth, fifth, second, third };
            yield return new[] { first, fourth, fifth, third, second };
            yield return new[] { first, fifth, second, third, fourth };
            yield return new[] { first, fifth, second, fourth, third };
            yield return new[] { first, fifth, third, second, fourth };
            yield return new[] { first, fifth, third, fourth, second };
            yield return new[] { first, fifth, fourth, second, third };
            yield return new[] { first, fifth, fourth, third, second };
            yield return new[] { second, first, third, fourth, fifth };
            yield return new[] { second, first, third, fifth, fourth };
            yield return new[] { second, first, fourth, third, fifth };
            yield return new[] { second, first, fourth, fifth, third };
            yield return new[] { second, first, fifth, third, fourth };
            yield return new[] { second, first, fifth, fourth, third };
            yield return new[] { second, third, first, fourth, fifth };
            yield return new[] { second, third, first, fifth, fourth };
            yield return new[] { second, third, fourth, first, fifth };
            yield return new[] { second, third, fourth, fifth, first };
            yield return new[] { second, third, fifth, first, fourth };
            yield return new[] { second, third, fifth, fourth, first };
            yield return new[] { second, fourth, first, third, fifth };
            yield return new[] { second, fourth, first, fifth, third };
            yield return new[] { second, fourth, third, first, fifth };
            yield return new[] { second, fourth, third, fifth, first };
            yield return new[] { second, fourth, fifth, first, third };
            yield return new[] { second, fourth, fifth, third, first };
            yield return new[] { second, fifth, first, third, fourth };
            yield return new[] { second, fifth, first, fourth, third };
            yield return new[] { second, fifth, third, first, fourth };
            yield return new[] { second, fifth, third, fourth, first };
            yield return new[] { second, fifth, fourth, first, third };
            yield return new[] { second, fifth, fourth, third, first };
            yield return new[] { third, first, second, fourth, fifth };
            yield return new[] { third, first, second, fifth, fourth };
            yield return new[] { third, first, fourth, second, fifth };
            yield return new[] { third, first, fourth, fifth, second };
            yield return new[] { third, first, fifth, second, fourth };
            yield return new[] { third, first, fifth, fourth, second };
            yield return new[] { third, second, first, fourth, fifth };
            yield return new[] { third, second, first, fifth, fourth };
            yield return new[] { third, second, fourth, first, fifth };
            yield return new[] { third, second, fourth, fifth, first };
            yield return new[] { third, second, fifth, first, fourth };
            yield return new[] { third, second, fifth, fourth, first };
            yield return new[] { third, fourth, first, second, fifth };
            yield return new[] { third, fourth, first, fifth, second };
            yield return new[] { third, fourth, second, first, fifth };
            yield return new[] { third, fourth, second, fifth, first };
            yield return new[] { third, fourth, fifth, first, second };
            yield return new[] { third, fourth, fifth, second, first };
            yield return new[] { third, fifth, first, second, fourth };
            yield return new[] { third, fifth, first, fourth, second };
            yield return new[] { third, fifth, second, first, fourth };
            yield return new[] { third, fifth, second, fourth, first };
            yield return new[] { third, fifth, fourth, first, second };
            yield return new[] { third, fifth, fourth, second, first };
            yield return new[] { fourth, first, second, third, fifth };
            yield return new[] { fourth, first, second, fifth, third };
            yield return new[] { fourth, first, third, second, fifth };
            yield return new[] { fourth, first, third, fifth, second };
            yield return new[] { fourth, first, fifth, second, third };
            yield return new[] { fourth, first, fifth, third, second };
            yield return new[] { fourth, second, first, third, fifth };
            yield return new[] { fourth, second, first, fifth, third };
            yield return new[] { fourth, second, third, first, fifth };
            yield return new[] { fourth, second, third, fifth, first };
            yield return new[] { fourth, second, fifth, first, third };
            yield return new[] { fourth, second, fifth, third, first };
            yield return new[] { fourth, third, first, second, fifth };
            yield return new[] { fourth, third, first, fifth, second };
            yield return new[] { fourth, third, second, first, fifth };
            yield return new[] { fourth, third, second, fifth, first };
            yield return new[] { fourth, third, fifth, first, second };
            yield return new[] { fourth, third, fifth, second, first };
            yield return new[] { fourth, fifth, first, second, third };
            yield return new[] { fourth, fifth, first, third, second };
            yield return new[] { fourth, fifth, second, first, third };
            yield return new[] { fourth, fifth, second, third, first };
            yield return new[] { fourth, fifth, third, first, second };
            yield return new[] { fourth, fifth, third, second, first };
            yield return new[] { fifth, first, second, third, fourth };
            yield return new[] { fifth, first, second, fourth, third };
            yield return new[] { fifth, first, third, second, fourth };
            yield return new[] { fifth, first, third, fourth, second };
            yield return new[] { fifth, first, fourth, second, third };
            yield return new[] { fifth, first, fourth, third, second };
            yield return new[] { fifth, second, first, third, fourth };
            yield return new[] { fifth, second, first, fourth, third };
            yield return new[] { fifth, second, third, first, fourth };
            yield return new[] { fifth, second, third, fourth, first };
            yield return new[] { fifth, second, fourth, first, third };
            yield return new[] { fifth, second, fourth, third, first };
            yield return new[] { fifth, third, first, second, fourth };
            yield return new[] { fifth, third, first, fourth, second };
            yield return new[] { fifth, third, second, first, fourth };
            yield return new[] { fifth, third, second, fourth, first };
            yield return new[] { fifth, third, fourth, first, second };
            yield return new[] { fifth, third, fourth, second, first };
            yield return new[] { fifth, fourth, first, second, third };
            yield return new[] { fifth, fourth, first, third, second };
            yield return new[] { fifth, fourth, second, first, third };
            yield return new[] { fifth, fourth, second, third, first };
            yield return new[] { fifth, fourth, third, first, second };
            yield return new[] { fifth, fourth, third, second, first };
        }

    }

    public static class MyExtensions
    {
        public static int[] MgpDic => new int[19] { 10000, 36, 720, 360, 80, 252, 108, 72, 54, 180, 72, 180, 119, 36, 306, 1080, 144, 1800, 3600 };

        public static int ToMgp(this int sum)
        {
            if (sum < 6 || 24 < sum)
            {
                throw new ArgumentOutOfRangeException();
            }

            return MgpDic[sum - 6];
        }

        public static (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping) Calculate(this int[] array)
        {
            if (array.Length != 9)
            {
                throw new InvalidDataException("not 9");
            }

            static int Sum(ReadOnlySpan<int> values)
            {
                var result = 0;

                foreach (var x in values)
                {
                    result += x;
                }

                return result;
            }

            return (Sum(array[..3]).ToMgp(), Sum(array[3..6]).ToMgp(), Sum(array[6..9]).ToMgp(), (array[0] + array[3] + array[6]).ToMgp(), (array[1] + array[4] + array[7]).ToMgp(), (array[2] + array[5] + array[8]).ToMgp(), (array[0] + array[4] + array[8]).ToMgp(), (array[2] + array[4] + array[6]).ToMgp());
        }


        public static (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping) Calculate2(this byte[] array)
        {
            if (array.Length != 36)
            {
                throw new InvalidDataException("not 9");
            }

            return (
                (array[0 * 4] + array[1 * 4] + array[2 * 4]).ToMgp(),
                (array[3 * 4] + array[4 * 4] + array[5 * 4]).ToMgp(),
                (array[6 * 4] + array[7 * 4] + array[8 * 4]).ToMgp(),
                (array[0 * 4] + array[3 * 4] + array[6 * 4]).ToMgp(),
                (array[1 * 4] + array[4 * 4] + array[7 * 4]).ToMgp(),
                (array[2 * 4] + array[5 * 4] + array[8 * 4]).ToMgp(),
                (array[0 * 4] + array[4 * 4] + array[8 * 4]).ToMgp(),
                (array[2 * 4] + array[4 * 4] + array[6 * 4]).ToMgp()
                );
        }



        public static IEnumerable<KeyValuePair<string, double>> ToKeyValuePairs(this (int topRow, int middleRow, int bottomRow, int leftColumn, int middleColumn, int rightColumn, int downwardSloping, int upwardSloping)[] resultsOfCalculations)
        {
            yield return new KeyValuePair<string, double>("Top Row", resultsOfCalculations.Select(x => x.topRow).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("Middle Row", resultsOfCalculations.Select(x => x.middleRow).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("BottomRow", resultsOfCalculations.Select(x => x.bottomRow).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("Left Column", resultsOfCalculations.Select(x => x.leftColumn).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("Middle Column", resultsOfCalculations.Select(x => x.middleColumn).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("Right Column", resultsOfCalculations.Select(x => x.rightColumn).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("Downward Sloping", resultsOfCalculations.Select(x => x.downwardSloping).Sum() / 120.0);
            yield return new KeyValuePair<string, double>("Upward Sloping", resultsOfCalculations.Select(x => x.upwardSloping).Sum() / 120.0);
        }
    }

}