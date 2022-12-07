namespace Common;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

public static class Extensions
{
    public static ValueTuple<T1, T2> Sum<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> results)
        where T1 : INumber<T1>
        where T2 : INumber<T2>
        => results.Aggregate((x, y) => (x.Item1 + y.Item1, x.Item2 + y.Item2));

    public static ValueTuple<T1, T2, T3> Sum<T1, T2, T3>(this IEnumerable<ValueTuple<T1, T2, T3>> results)
        where T1 : INumber<T1>
        where T2 : INumber<T2>
        where T3 : INumber<T3>
        => results.Aggregate((x, y) => (x.Item1 + y.Item1, x.Item2 + y.Item2, x.Item3 + y.Item3));

    public static TimeSpan Process(
        this string filename,
        Action<string, int> process)
    {
        var watch = new Stopwatch();

        watch.Start();

        File
            .ReadLinesAsync(filename)
            .ToBlockingEnumerable()
            .ForEach(process);

        watch.Stop();

        return watch.Elapsed;
    }

    public static void WriteSum<T1, T2>(
        this string filename,
        Func<string, int, ValueTuple<T1, T2>> process)
        where T1 : INumber<T1>
        where T2 : INumber<T2>
        => WriteSumBody(
            filename,
            process,
            Sum<T1, T2>,
            (result, watch) => $"1: {result.Item1} 2: {result.Item2} in {watch.ElapsedMilliseconds}ms");

    public static void WriteSum<T1, T2, T3>(
        this string filename,
        Func<string, int, ValueTuple<T1, T2, T3>> process)
        where T1 : INumber<T1>
        where T2 : INumber<T2>
        where T3 : INumber<T3>
        => WriteSumBody(
            filename,
            process,
            Sum<T1, T2, T3>,
            (result, watch) => $"1: {result.Item1} 2: {result.Item2} 3: {result.Item3} in {watch.ElapsedMilliseconds}ms");

    private static void WriteSumBody<T1>(
        string filename,
        Func<string, int, T1> process,
        Func<IEnumerable<T1>, T1> sum,
        Func<T1, Stopwatch, string> getOutput)
        where T1 : ITuple
    {
        var watch = new Stopwatch();

        watch.Start();

        var result = sum(File
            .ReadLinesAsync(filename)
            .ToBlockingEnumerable()
            .Select(process));

        watch.Stop();

        Console.WriteLine(getOutput(result, watch));
    }

    // https://themuuj.com/blog/2020/08/csharp-collection-deconstructing/
    // deconstructs a collection to two values
    public static void Deconstruct<T>(
        this IEnumerable<T> collection,
        [MaybeNull] out T item1,
        [MaybeNull] out T item2)
    {
        var enumerator = collection.GetEnumerator();
        item1 = Next(enumerator);
        item2 = Next(enumerator);
    }

    // deconstructs a collection to three values
    public static void Deconstruct<T>(
        this IEnumerable<T> collection,
        [MaybeNull] out T item1,
        [MaybeNull] out T item2,
        [MaybeNull] out T item3)
    {
        var enumerator = collection.GetEnumerator();
        item1 = Next(enumerator);
        item2 = Next(enumerator);
        item3 = Next(enumerator);
    }

    // deconstructs a collection to four values
    public static void Deconstruct<T>(
        this IEnumerable<T> collection,
        [MaybeNull] out T item1,
        [MaybeNull] out T item2,
        [MaybeNull] out T item3,
        [MaybeNull] out T item4)
    {
        var enumerator = collection.GetEnumerator();
        item1 = Next(enumerator);
        item2 = Next(enumerator);
        item3 = Next(enumerator);
        item4 = Next(enumerator);
    }

    // helper method to advance enumerator and return next value
    [return: MaybeNull]
    private static T Next<T>(IEnumerator<T> enumerator)
        => enumerator.MoveNext()
        ? enumerator.Current
        : default;

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> process)
    {
        foreach (var element in enumerable)
        {
            process(element);
        }
    }

    public static void ForEach<T1>(this IEnumerable<T1> enumerable, Action<T1, int> process)
    {
        var i = 0;
        foreach (var element in enumerable)
        {
            process(element, i++);
        }
    }

    public static int ToIntI(this string input)
        => int.Parse(input, CultureInfo.InvariantCulture);

    public static bool StartsWithIIC(this string input, string pattern)
        => input.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase);

    public static void Times(this int input, Action action)
    {
        for (var i = 0; i < input; i++)
        {
            action();
        }
    }
}