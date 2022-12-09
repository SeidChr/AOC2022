using Common;

// TASK: count positions of the tail (including starting position)
// Tail follows Head directly. it will always catch up to an adiacent tile.
// one step is the maximum distance

// tail can be in 9 relative positions towards head (including overlaying)
// horizontal or vertical opposite to moving direction, follows in moving direction
// diagonal will jump diagonally behind head once, then follow in moving direction

//          2
//          1
// y -2 -1  0  1  2
//         -1
//         -2
//          x

(int x, int y) head = new(0, 0);
(int x, int y) tail = new(0, 0);

HashSet<(int, int)> set = new()
{
    tail
};

"input.txt".Process((line, li) =>
{
    // Console.WriteLine(line);
    var distance = line[2..].ToIntI();
    for (var i = 0; i < distance; i++)
    {
        Step(line[0]);
    }
});

Console.WriteLine(set.Count);

void Step(char direction)
{
    switch (direction)
    {
        case 'U':
            if (tail.y < head.y)
            {
                tail = head;
            }
            head.y++;
            break;
        case 'D':
            if (tail.y > head.y)
            {
                tail = head;
            }
            head.y--;
            break;
        case 'R':
            if (tail.x < head.x)
            {
                tail = head;
            }
            head.x++;
            break;
        case 'L':
            if (tail.x > head.x)
            {
                tail = head;
            }
            head.x--;
            break;
    }

    _ = set.Add(tail);
}