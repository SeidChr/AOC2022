using Common;

var cycle = 1;
var sum = 0;
var x = 1;

"input.txt".Process((line, li) =>
{
    if (cycle > 240)
    {
        return;
    }


    if (line.StartsWithIIC("addx"))
    {
        AddX(line[5..].ToIntI());
    }
    else
    {
        NoOp();
    }
});

Console.WriteLine(sum);

void AddX(int parsed)
{
    ConsumeCycle();
    ConsumeCycle();
    x += parsed;
}

void NoOp() => ConsumeCycle();

void ConsumeCycle()
{
    Draw();

    if ((cycle - 20) % 40 == 0)
    {
        sum += cycle * x;
    }

    cycle += 1;
}

void Draw()
{
    // draw a pixel
    var pixel = cycle - 1;
    pixel %= 40;
    var pixelInRange = pixel >= x - 1 && pixel <= x + 1;
    Console.Write(pixelInRange ? "#" : ".");

    // make newline at end of line
    if (cycle % 40 == 0)
    {
        // 40 cycles per line
        // newline after cylce 40
        Console.WriteLine();
    }
}