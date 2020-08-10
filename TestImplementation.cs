using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;

namespace Itixo.linq.skoleni
{
    public static class TestImplementation
    {
        public static string ReverseNotNullSentence(this string sentence) => string.Join(" ", sentence.Split(" ").Reverse());

        public static string FirstLetterToUpperCase(this string sentece) =>
            string.Join(
                " ",
                sentece.Split(" ").Select(word => $"{char.ToUpper(word.First())}{word.Substring(1, word.Length - 1)}")
            );

        public static string MultiplyCharAccordingToPattern(this string sentece) =>
            string.Join(
                "-",
                sentece.ToCharArray()
                    .Select(
                        (ch, index) => new string(ch, index + 1).FirstLetterToUpperCase())
            );

        public static string FindMinAndMaxInSentece(this string sentece) =>
            $"{sentece.ToIntArray().Min()} {sentece.ToIntArray().Max()}";

        private static IEnumerable<int> ToIntArray(this string sentece)
            => sentece.Split(" ").Select(int.Parse);

        public static string FindMissingCharInAlphabet(this string sentence)
            => string.Join(
                " ",
                Enumerable.Range(sentence.First(), sentence.Length)
                    .Select(ch => (char) ch)
                    .Except(sentence));

        //public static string FindMissingCharInAlphabetContains(this string sentence)
        //    => sentence.Select(ch => (int)ch).First(ch => !Enumerable.Range(sentence.First(), sentence.Length).Contains(ch)).ToString();

        public static char FindMissingCharInAlphabetContains(this string sentence)
            =>(char) Enumerable.Range(sentence.First(), sentence.Length).FirstOrDefault(x => !sentence.Contains((char)x));

        //public static (int, int)[] FindOrderedDuplicities(this int[] array)
        //=> array.Distinct().OrderBy(val => val).Select(val => (val, array.Count(a => val == a))).ToArray();

        public static (int, int)[] FindOrderedDuplicities(this int[] array)
            => array.Distinct().OrderBy(val => val).GroupBy(key => key, val => array.Count(x => val == x)).Select(x => (x.Key, x.FirstOrDefault())).ToArray();

        public static int SumSquearesFromOneTo(this int input)
            => (int) Enumerable.Range(1, input).Sum(x => x * x);

        public static int CreateNewNumberButOrdered(this int input)
            => int.Parse(string.Join("", input.ToString().ToCharArray().OrderByDescending(x => x).ToArray()));

        public static int[] MultiplayTwoCollections(this int[] array, int[] array2)
            => array.Zip(array2, (first, second) => first * second).ToArray();

        public static string FindLongest(this string[] pole, int k) =>
            k > pole.Length 
                ? string.Empty 
                : pole.Select((val, i) => string.Join("", pole.Skip(i).Take(k)))
                    .OrderByDescending(x => x.Length)
                    .First();

        public static string[] MakeDaTower(this int k)
        {
            return Enumerable.Range(1, k).Select((floor) => $"{GenerateSpaces(k, floor)}{new string('*', ((floor * 2) - 1))}{GenerateSpaces(k, floor)}").ToArray();
        }

        private static string GenerateSpaces(int k, int floor)
        {
            return new string(' ', k-floor);
        }

        public static bool CheckBrackets(this string text)
        {
            return text.Count(x => x =='(') == text.Count(x => x == ')') 
                   && text.Length > 0 
                   && text.Where(ch => ch == '(').Select(ch => ch).All(ch => text.IndexOf(')') > text.IndexOf(ch));
        }

        public static int[] GenerateRandom(this int x)
        {
            return Enumerable.Range(1, x).Select(x => new Random().Next()).ToArray();
        }

        public static Person[] MapToPerson(this string[] names)
        {
            return names.Select(name => new Person(name)).ToArray();
        }

        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> inputs, Predicate<T> predicate)
        {
            foreach (var input in inputs)
            {
                if (predicate(input))
                {
                    yield return input;
                }
            }
        }

        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> values) where T : IComparable
        {
            return !values.Any() ? new List<T>() : 
            QuickSort(values.Where(elem => elem.CompareTo(values.ElementAt(0)) < 0))
                .Concat( new []{ values.ElementAt(0) })
                .Concat(QuickSort(values.Where(elem => elem.CompareTo(values.ElementAt(0)) >= 0)));
        }
    }

    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}