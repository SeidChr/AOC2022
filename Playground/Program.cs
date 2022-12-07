using System.Collections;

Console.WriteLine("123456789"[0..9]);
Console.WriteLine("123456789"[0..^0]);
Console.WriteLine("123456789"[0..^1]);
Console.WriteLine("123456789"[0..^2]);

foreach (var i in 1..6)
{
    Console.WriteLine(i);
}

Console.WriteLine(string.Concat((1..6).AsEnumerable().Select(x => x * 2)));

public static class Ext
{
    public static RangeEnumerator GetEnumerator(this Range range) => new(range);
    public static RangeEnumeratorClass GetEnumeratorClass(this Range range) => new(range);

    public static IEnumerable<int> AsEnumerable(this Range range)
    {
        var e = range.GetEnumeratorClass();

        while (e.MoveNext())
        {
            yield return e.Current;
        }
    }

    public class RangeEnumeratorClass : IEnumerator<int>
    {
        private readonly int start;

        private readonly int end;

        public RangeEnumeratorClass(Range range)
        {
            if (range.End.IsFromEnd)
            {
                throw new NotSupportedException("Cannot use a Range from end as enumerator");
            }

            this.start = range.Start.Value - 1;
            this.Current = this.start;
            this.end = range.End.Value;
        }

        public int Current { get; private set; }

        object IEnumerator.Current => this.Current;

        public void Dispose() => GC.SuppressFinalize(this);

        public bool MoveNext()
        {
            if (this.Current <= this.end)
            {
                this.Current++;
            }

            return this.Current <= this.end;
        }

        public void Reset() => this.Current = this.start;
    }

    public ref struct RangeEnumerator
    {
        private readonly int start;

        private readonly int end;

        public RangeEnumerator(Range range)
        {
            if (range.End.IsFromEnd)
            {
                throw new NotSupportedException("Cannot use a Range from end as enumerator");
            }

            this.start = range.Start.Value - 1;
            this.Current = this.start;
            this.end = range.End.Value;
        }

        public int Current { get; private set; }

        public bool MoveNext()
        {
            if (this.Current <= this.end)
            {
                this.Current++;
            }

            return this.Current <= this.end;
        }

        public void Reset() => this.Current = this.start;
    }
}