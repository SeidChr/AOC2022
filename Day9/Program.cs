using Common;

using static Day9Helper;

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

var rope = Enumerable.Range(0, 10).Select(_ => (0, 0)).ToArray();

HashSet<(int x, int y)> positions = new() { rope[^1] };

"input.txt".Process((line, li) =>
{
    Console.WriteLine(line);

    var ropeLength = rope.Length;

    // Console.WriteLine(line);
    var distance = line[2..].ToIntI();
    for (var i = 0; i < distance; i++)
    {
        var p1Move = Move(rope[0], line[0]);
        for (var j = 0; j < ropeLength; j++)
        {
            var p1 = rope[j];
            rope[j] = p1Move;

            if (j >= ropeLength - 1)
            {
                break;
            }

            var p2 = rope[j + 1];
            var p2Move = Follow(p1, p1Move, p2);

            if (p2 == p2Move)
            {
                // Console.WriteLine($"{j}: {p1}->{p1Move} --> {p2}->{p2Move} --|");
                break;
            }
            else
            {
                // Console.WriteLine($"{j}: {p1}->{p1Move} --> {p2}->{p2Move}");
                p1Move = p2Move;
            }
        }
        // rope[^1] = p1Move;
        _ = positions.Add(rope[^1]);
    }
});

Console.WriteLine(positions.Count);


// void SwingRope(int p1Index, (int x, int y) p1, (int x, int y) p1Move, (int x, int y) p2)
// {
//     if (p1Index == rope.Length - 2)
//     var p2Move = Follow(p1, p1Move, p2);

//     if (p2 == p2Move)
//     {
//         return;
//     }

//     SwingRope(p1Index + 1, p2, p2Move, rope[p1Index + 1]);
// }