using Common;

int[,] matrice = null!;

var side = 0;

"input.txt".Process((line, y) =>
{
    if (y == 0)
    {
        side = line.Length;
        matrice = new int[side, side];
    }

    for (var x = 0; x < side; x++)
    {
        matrice[x, y] = line[x] - 48;
    }
});

var (visibleCount, maxScenicRating) = GetValues();

Console.WriteLine($"1: {visibleCount} 2: {maxScenicRating}");

// ////////////////////////////////////////////////////////////////////////

(int visibleCount, int maxScenicRating) GetValues()
{
    var visibleCount = 0;
    var maxScenicRating = 0;
    var offside = side - 1;

    for (var y = 1; y < offside; y++)
    {
        for (var x = 1; x < offside; x++)
        {
            if (IsVisibleTop(x, y)
                || IsVisibleLeft(x, y)
                || IsVisibleRight(x, y)
                || IsVisibleBottom(x, y))
            {
                visibleCount++;
            }

            var top = GetVisibleTreesTop(x, y);
            var bottom = GetVisibleTreesBottom(x, y);
            var left = GetVisibleTreesLeft(x, y);
            var right = GetVisibleTreesRight(x, y);

            var scenicRating = top * bottom * left * right;

            // Console.WriteLine($"[{x}, {y}] = {matrice[x, y]} // t{top} * b{bottom} * l{left} * r{right} = {scenicRating}");

            if (scenicRating > maxScenicRating)
            {
                maxScenicRating = scenicRating;
            }
        }
    }

    visibleCount += (side * 4) - 4;

    return (visibleCount, maxScenicRating);
}

bool IsVisibleTop(int x, int y) => GetVerticalSizes(x, 0, y - 1).All(g => g < matrice[x, y]);
bool IsVisibleBottom(int x, int y) => GetVerticalSizes(x, y + 1, side - 1).All(g => g < matrice[x, y]);
bool IsVisibleLeft(int x, int y) => GetHorizontalSizes(y, 0, x - 1).All(g => g < matrice[x, y]);
bool IsVisibleRight(int x, int y) => GetHorizontalSizes(y, x + 1, side - 1).All(g => g < matrice[x, y]);

int GetVisibleTreesTop(int x, int y) => BeEdgy(GetVerticalSizes(x, y - 1, 0), x, y);
int GetVisibleTreesBottom(int x, int y) => BeEdgy(GetVerticalSizes(x, y + 1, side - 1), x, y);
int GetVisibleTreesLeft(int x, int y) => BeEdgy(GetHorizontalSizes(y, x - 1, 0), x, y);
int GetVisibleTreesRight(int x, int y) => BeEdgy(GetHorizontalSizes(y, x + 1, side - 1), x, y);

int BeEdgy(IEnumerable<int> treeSizes, int x, int y)
{
    var list = treeSizes.ToList();
    var result = list.TakeWhile(g => g < matrice[x, y]).Count();
    if (result < list.Count)
    {
        result++;
    }

    return result;
}

IEnumerable<int> GetVerticalSizes(int x, int yStart, int yEnd)
    => GetSizes(yStart, yEnd, y => matrice[x, y]);

IEnumerable<int> GetHorizontalSizes(int y, int xStart, int xEnd)
    => GetSizes(xStart, xEnd, x => matrice[x, y]);

IEnumerable<int> GetSizes(int start, int end, Func<int, int> action)
{
    if (start > end)
    {
        foreach (var i in Enumerable.Range(end, start - end + 1).Reverse())
        {
            yield return action(i);
        }
    }
    else
    {
        foreach (var i in Enumerable.Range(start, end - start + 1))
        {
            yield return action(i);
        }
    }
}