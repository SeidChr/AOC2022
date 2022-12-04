using Common;
var filename = "input.txt";
var lineCache = new string[3];

filename
    .WriteSum((line, index) =>
    {
        var second = 0;
        var cacheIndex = index % 3;
        lineCache[cacheIndex] = line;

        if (cacheIndex == 2)
        {
            second = GetPriority(
                lineCache[0]
                .Intersect(lineCache[1])
                .Intersect(lineCache[2])
                .First());
        }

        var len = line.Length / 2;
        var first = GetPriority(
            line.Take(len)
            .Intersect(line.Skip(len))
            .First());

        return (first, second);
    });

static int GetPriority(int asciiLetter) => asciiLetter - (asciiLetter > 96 ? 96 : 38);
