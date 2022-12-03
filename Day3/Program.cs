var filename = "input.txt";

var watch = new System.Diagnostics.Stopwatch();

var lineCache = new string[3];
watch.Start();

var result = File
    .ReadLinesAsync(filename)
    .ToBlockingEnumerable()
    .Select((line, index) =>
    {
        var second = 0;
        var cacheIndex = index % 3;
        lineCache[cacheIndex] = line;

        if (cacheIndex == 2)
        {
            second = GetPriority(lineCache[0].Intersect(lineCache[1]).Intersect(lineCache[2]).First());
        }

        var len = line.Length / 2;
        var first = GetPriority(line.Take(len).Intersect(line.Skip(len)).First());

        return (first, second);
    })
    .Aggregate((x, y) => (x.first += y.first, x.second += y.second));
;

watch.Stop();

Console.WriteLine($"1: {result.first} 2: {result.second} in {watch.ElapsedMilliseconds}ms");

static int GetPriority(int asciiLetter) => asciiLetter - (asciiLetter > 96 ? 96 : 38);
