using Common;

int[,] matrice = null!;

var side = 0;

"testinput.txt".Process((line, y) =>
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

    for (var x = 1; x < offside; x++)
    {
        for (var y = 1; y < offside; y++)
        {
            if (IsVisibleTop(x, y)
                || IsVisibleLeft(x, y)
                || IsVisibleRight(x, y)
                || IsVisibleBottom(x, y))
            {
                visibleCount++;
            }

            var scenicRating = GetVisibleTreesTop(x, y)
                * GetVisibleTreesBottom(x, y)
                * GetVisibleTreesLeft(x, y)
                * GetVisibleTreesRight(x, y);

            if (scenicRating > maxScenicRating)
            {
                Console.WriteLine($"x:{x} y:{y} = {matrice[x, y]} = {scenicRating}");
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

int GetVisibleTreesTop(int x, int y) => GetVerticalSizes(x, y - 1, 0).TakeWhile(g => g <= matrice[x, y]).Count();
int GetVisibleTreesBottom(int x, int y) => GetVerticalSizes(x, y + 1, side - 1).TakeWhile(g => g <= matrice[x, y]).Count();
int GetVisibleTreesLeft(int x, int y) => GetVerticalSizes(y, x - 1, 0).TakeWhile(g => g <= matrice[x, y]).Count();
int GetVisibleTreesRight(int x, int y) => GetVerticalSizes(y, x + 1, side - 1).TakeWhile(g => g <= matrice[x, y]).Count();

IEnumerable<int> GetVerticalSizes(int x, int yLow, int yHigh) => GetSizes(x, yLow, yHigh, (x, y) => matrice[x, y]);
IEnumerable<int> GetHorizontalSizes(int y, int xLow, int xHigh) => GetSizes(y, xLow, xHigh, (y, x) => matrice[x, y]);

IEnumerable<int> GetSizes(int a1, int b1, int b2, Func<int, int, int> action)
{
    var low = b1;
    var high = b2;

    if (b2 < b1)
    {
        low = b2;
        high = b1;
    }

    for (var a2 = low; a2 < high + 1; a2++)
    {
        yield return action(a1, a2);
    }
}