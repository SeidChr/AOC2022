namespace Common;

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

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

    public static void WriteSum<T1, T2>(
        this string filename,
        Func<string, int, ValueTuple<T1, T2>> process)
        where T1 : INumber<T1>
        where T2 : INumber<T2>
    {
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();

        var result = File
            .ReadLinesAsync(filename)
            .ToBlockingEnumerable()
            .Select(process)
            .Sum();

        watch.Stop();

        Console.WriteLine($"1: {result.Item1} 2: {result.Item2} in {watch.ElapsedMilliseconds}ms");
    }

    public static void WriteSum<T1, T2, T3>(
        this string filename,
        Func<string, int, ValueTuple<T1, T2, T3>> process)
        where T1 : INumber<T1>
        where T2 : INumber<T2>
        where T3 : INumber<T3>
    {
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();

        var result = File
            .ReadLinesAsync(filename)
            .ToBlockingEnumerable()
            .Select(process)
            .Sum();

        watch.Stop();

        Console.WriteLine($"1: {result.Item1} 2: {result.Item2} 3: {result.Item3} in {watch.ElapsedMilliseconds}ms");
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
    private static T Next<T>(IEnumerator<T> enumerator) =>
        enumerator.MoveNext() ? enumerator.Current : default;
}