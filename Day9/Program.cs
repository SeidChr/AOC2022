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
    var distance = line[2..].ToIntI();
    Lead(line[0], distance);
    Follow();
});


void Lead(char direction, int distance)
{
    switch (direction)
    {
        case 'U':
            head = (head.x, head.y + distance);
            break;
        case 'D':
            head = (head.x, head.y - distance);
            break;
        case 'R':
            head = (head.x + distance, head.y);
            break;
        case 'L':
            head = (head.x - distance, head.y);
            break;
    }

}

void Follow()
{
    (int x, int y) diff = (Math.Abs(head.x - tail.x), Math.Abs(head.y - tail.y));
    switch (diff)
    {
        case { x: > 1 } and { y: > 1 }:
            // diagonal
            break;
        default:
            break;
    }
}