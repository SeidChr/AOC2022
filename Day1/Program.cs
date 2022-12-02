using System.Globalization;

var array = GetElveCallories().ToBlockingEnumerable().OrderDescending().Take(3).ToArray();
Console.WriteLine(array[0]);
Console.WriteLine(array.Sum());

static async IAsyncEnumerable<int> GetElveCallories()
{
    var current = 0;

    await foreach (var line in File.ReadLinesAsync("input.txt"))
    {
        if (string.IsNullOrWhiteSpace(line) && current > 0)
        {
            yield return current;
            current = 0;
        }
        else
        {
            current += int.Parse(line, CultureInfo.InvariantCulture);
        }
    }

    if (current > 0)
    {
        yield return current;
    }
}
