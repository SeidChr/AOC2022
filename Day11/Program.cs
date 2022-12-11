// See https://aka.ms/new-console-template for more information
using Day11.Data;
using Common;

var monkeys = Input.MakeMonkeys();

20.Times(() =>
    monkeys.ForEach(monkey =>
    {
        while (monkey.Items.TryDequeue(out var worryLevel))
        {
            monkey.CountInspection();

            worryLevel = monkey.Operation(worryLevel);
            worryLevel /= 3; // int division shoud round down automatically
            var targetMonkeyIndex = worryLevel % monkey.TestDivisor == 0
                ? monkey.TrueTargetMonkey
                : monkey.FalseTargetMonkey;
            monkeys[targetMonkeyIndex].Items.Enqueue(worryLevel);
        };
    }));

var (first, second) = monkeys.Select(monkey => monkey.Inspections).OrderDescending().Take(2);
var product = first * second;

Console.WriteLine(product);


monkeys = Input.MakeTestMonkeys();
var testRounds = new List<int>() { 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };

var commonDivisor = monkeys.Select(monkey => monkey.TestDivisor).Aggregate((a, b) => a * b);

10000.Times(round =>
{
    // Console.WriteLine(round + 1);
    monkeys.ForEach(monkey =>
    {
        while (monkey.Items.TryDequeue(out var worryLevel))
        {
            monkey.CountInspection();

            worryLevel = monkey.Operation(worryLevel);
            // worryLevel /= 3; // int division shoud round down automatically
            worryLevel %= commonDivisor; // from reddit ... not working ¯\_(ツ)_/¯ ... guessing math operations doesnt work for me.
            var targetMonkeyIndex = worryLevel % monkey.TestDivisor == 0
                ? monkey.TrueTargetMonkey
                : monkey.FalseTargetMonkey;
            monkeys[targetMonkeyIndex].Items.Enqueue(worryLevel);
        };
    });

    //_ = Console.ReadLine();

    if (testRounds.Contains(round + 1))
    {
        Console.WriteLine($"== After round {round + 1} ==");
        monkeys.ForEach((monkey, i) => Console.WriteLine($"Monkey {i} inspected items {monkey.Inspections} times."));
    }
});

(first, second) = monkeys.Select(monkey => monkey.Inspections).OrderDescending().Take(2);
product = first * second;

Console.WriteLine(product);
