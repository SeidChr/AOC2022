public static class Day9Helper
{
    public static (int x, int y) Distance((int x, int y) p1, (int x, int y) p2)
        => (p2.x - p1.x, p2.y - p1.y);

    public static int MaxDistance((int x, int y) p1, (int x, int y) p2)
        => MaxDistance(Distance(p1, p2));

    public static int MaxDistance((int x, int y) distance)
    {
        var (x, y) = Abs(distance);
        return Math.Max(x, y);
    }

    public static (int x, int y) Move((int x, int y) head, char direction)
    {
        var result = head;
        switch (direction)
        {
            case 'U':
                result.y++;
                break;
            case 'D':
                result.y--;
                break;
            case 'R':
                result.x++;
                break;
            case 'L':
                result.x--;
                break;
            default:
                break;
        }

        return result;
    }

    public static (int x, int y) Follow((int x, int y) p1Start, (int x, int y) p1End, (int x, int y) p2)
    {
        // do we need to move?
        // equality needs to be checked outside, to stop the loop.
        if (MaxDistance(p1End, p2) <= 1)
        {
            return p2;
        }

        var dist = Distance(p1Start, p1End);
        if (Diagonal(dist))
        {
            return Add(p2, dist);
        }
        else
        {
            return p1Start;
        }
    }

    public static (int x, int y) Abs((int x, int y) p)
        => (Math.Abs(p.x), Math.Abs(p.y));

    public static (int x, int y) Add((int x, int y) p1, (int x, int y) p2)
        => (p1.x + p2.x, p1.y + p2.y);

    public static bool Diagonal((int x, int y) distance)
        => distance.x != 0 && distance.y != 0;

    public static bool Diagonal((int x, int y) p1, (int x, int y) p2)
        => Diagonal(Distance(p1, p2));
}