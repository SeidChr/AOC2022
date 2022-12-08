using Common;

var path = new Stack<string>();

Dictionary<string, long> pathSizeMap = new();
Dictionary<string, long> recursivePathSizeMap = new();

const string changeDirCommand = "$ cd ";

"input.txt".Process((line, i) =>
{
    switch (line)
    {
        case { } when line.StartsWithIIC("$ ls"):
        case { } when line.StartsWithIIC("dir "):
            // item in file listing -> ignore
            // initiate file listing -> ignore
            break;

        case { } when line.StartsWithIIC("$ cd .."):
            GoUp();
            break;

        case { } when line.StartsWithIIC(changeDirCommand):
            GoDown(line);
            break;

        // actual file, stars with size -> aggregate
        case { }:
            ProcessFileSize(line);
            break;

        default:
            throw new NotImplementedException();
    };
});

var allPaths = pathSizeMap.Keys.ToList();

// non-recursive dir sizes
// allPaths.ForEach(key => Console.WriteLine($"{key} = {pathSizeMap[key]}"));

// add recursive sizes
allPaths.ForEach(key => recursivePathSizeMap[key] = allPaths
    .Where(p => p.StartsWithIIC(key))
    .Select(p => pathSizeMap[p])
    .Sum());

// recursive dir sizes
// allPaths.ForEach(key => Console.WriteLine($"{key} = {recursivePathSizeMap[key]}"));

Console.WriteLine("1: " + allPaths.Select(key => recursivePathSizeMap[key]).Where(value => value <= 100000).Sum());

// total size:               70000000
// required free for update: 30000000
// all used space = recursivePathSizeMap["/"]
// total size - all used space = current free space
// required free space - current free space = minimum amount to be freed
// find smallest sum larger than that

var totalSize = 70000000;
var requiredSpace = 30000000;
var allUsedSpace = recursivePathSizeMap["/"];
var currentFreeSpace = totalSize - allUsedSpace;
var minFree = requiredSpace - currentFreeSpace;
Console.WriteLine("2: " + allPaths.Select(key => recursivePathSizeMap[key]).Where(value => value >= minFree).Order().First());


string Current()
    => string.Join(" ", path.Reverse());

void ProcessFileSize(string line)
{
    long size = line.Split(' ')[0].ToIntI();
    var fullPath = Current();
    if (!pathSizeMap.ContainsKey(fullPath))
    {
        pathSizeMap[fullPath] = 0;
    }

    pathSizeMap[fullPath] += size;
}

void GoDown(string line)
{
    path.Push(line[changeDirCommand.Length..]);
    var fullPath = Current();
    if (!pathSizeMap.ContainsKey(fullPath))
    {
        pathSizeMap[fullPath] = 0;
    }
}

void GoUp() => path.Pop();

