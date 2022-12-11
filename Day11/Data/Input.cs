namespace Day11.Data;

public static class Input
{
    // of cause i parsed this ;)
    public static List<Monkey> MakeMonkeys()
        => new()
        {
            new(17, 2, 7, old => old * 19,  83, 97, 59, 67),
            new(19, 7, 0, old => old + 2,   71, 70, 79, 88, 56, 70),
            new( 7, 4, 3, old => old + 7,   98, 51, 51, 63, 80, 85, 84, 95),
            new(11, 6, 4, old => old + 1,   77, 90, 82, 80, 79),
            new(13, 6, 5, old => old * 5,   68),
            new( 3, 1, 0, old => old + 5,   60, 94),
            new( 5, 5, 1, old => old * old, 81, 51, 85),
            new( 2, 2, 3, old => old + 3,   98, 81, 63, 65, 84, 71, 84),
        };

    public static List<Monkey> MakeTestMonkeys()
        => new()
        {
            new(23, 2, 3, old => old * 19,  79, 98),
            new(19, 2, 0, old => old + 6,   54, 65, 75, 74 ),
            new(13, 1, 3, old => old * old, 79, 60, 97),
            new(17, 0, 1, old => old + 3,   74),
        };
}

public class Monkey
{
    public Monkey(
        int testDivisor,
        int trueTargetMonkey,
        int falseTargetMonkey,
        Func<int, int> operation,
        params int[] items)
    {
        this.Items = new Queue<int>(items);
        this.Operation = operation;
        this.TestDivisor = testDivisor;
        this.TrueTargetMonkey = trueTargetMonkey;
        this.FalseTargetMonkey = falseTargetMonkey;
    }

    public long Inspections { get; private set; }

    public Queue<int> Items { get; init; } = new();

    public Func<int, int> Operation { get; init; }

    public int TestDivisor { get; init; }

    public int TrueTargetMonkey { get; init; }

    public int FalseTargetMonkey { get; init; }

    public void CountInspection() => this.Inspections++;
}