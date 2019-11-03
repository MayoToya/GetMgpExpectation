using System;
using System.Collections.Generic;
using System.Linq;

namespace dwyl.GetPermutationCode
{
    internal class Program
    {
        private static void Main()
        {
            var results = GetPermutationEnumerable(Enumerable.Range(1, 5));

            var stringResults = results.Select(x => x.Select(Converter));

            foreach (var stringResult in stringResults)
            {
                Console.WriteLine($"yield return new[] {{{string.Join(", ", stringResult)}}};");
            }
        }

        private static string Converter(int i)
        {
            return i switch { 1 => "first", 2 => "second", 3 => "third", 4 => "fourth", 5 => "fifth", _ => throw new InvalidOperationException() };
        }


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

    }

}
