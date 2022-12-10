using System.Globalization;

using Common;

List<string> stackLines = new();
var parseStacks = true;

List<Stack<char>> stacks = new();
Stack<char> tempStack = new();

var elapsed = "input.txt".Process((line, index) =>
{
    // TODO: think of how to institutionalize multi-type-parsing (parse multiple sections in the same file)
    // pass in multiple process functions, and a separator condition function, then utilize an 
    // enumerator to the "current" process function one after the other
    // this way constantly checking for the whitespace can be avoided by switching to the next processor
    if (parseStacks)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            parseStacks = false;
            ParseStacks();
            return;
        }

        stackLines.Add(line);
    }
    else
    {
        // select ProcessMoveA or ProcessMoveB to solve either first or second task
        // both work on the same stacks, so do not use them together.
        ProcessMoveB(line);
    }
});

Console.WriteLine($"1: {string.Concat(stacks.Select(stack => stack.Peek()))} in {elapsed.TotalMilliseconds:0}ms");

(int boxes, int source, int destination) GetDirections(string line)
{
    var array = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var boxes = array[1].ToInvariantInt();
    var source = array[3].ToInvariantInt();
    var destination = array[5].ToInvariantInt();
    return (boxes, source, destination);
}

void ProcessMoveA(string line)
{
    // move 3 from 5 to 7
    var (boxes, source, destination) = GetDirections(line);

    boxes.Times(() => stacks[destination - 1].Push(stacks[source - 1].Pop()));
}

void ProcessMoveB(string line)
{
    // move 3 from 5 to 7
    var (boxes, source, destination) = GetDirections(line);

    boxes.Times(() => tempStack.Push(stacks[source - 1].Pop()));
    boxes.Times(() => stacks[destination - 1].Push(tempStack.Pop()));
}

void ParseStacks()
{
    const int step = 4;
    var nrOfStacks = int.Parse(stackLines.Last().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Last(), CultureInfo.InvariantCulture);
    Console.WriteLine("Number of Stacks:" + nrOfStacks);
    Enumerable.Range(1, nrOfStacks).ForEach(i => stacks.Add(new Stack<char>()));

    stackLines
        .Where(line => !string.IsNullOrWhiteSpace(line))
        .Reverse()
        .Skip(1)
        .ForEach(line => stacks
            .ForEach((stack, i) =>
            {
                var lineIndex = (i * step) + 1;

                if (lineIndex < line.Length)
                {
                    var character = line[lineIndex];

                    if (!char.IsWhiteSpace(character))
                    {
                        stack.Push(character);
                    }
                }
            }));

    stacks.ForEach((stack, i) => Console.WriteLine($"{i + 1}: {string.Join(", ", stacks[i].Reverse())} <- TOP"));
}