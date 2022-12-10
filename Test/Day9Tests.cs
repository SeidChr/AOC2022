namespace Test;
using FluentAssertions;

public class Day9Tests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 1, 1)]
    [InlineData(0, 2, 2)]
    [InlineData(2, 2, 2)]
    [InlineData(2, 3, 3)]
    [InlineData(-1, 0, 1)]
    [InlineData(0, -1, 1)]
    [InlineData(-1, -1, 1)]
    [InlineData(0, -2, 2)]
    [InlineData(-2, -2, 2)]
    [InlineData(-2, -3, 3)]
    public void Distance(int x2, int y2, int ad)
    {
        var p1 = (0, 0);
        var p2 = (x2, y2);
        _ = Day9Helper.MaxDistance(p1, p2).Should().Be(ad);
    }

    [Theory]
    [InlineData(0, 0, false)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, false)]
    [InlineData(1, 1, true)]
    [InlineData(0, 2, false)]
    [InlineData(2, 2, true)]
    [InlineData(2, 3, true)]
    [InlineData(-1, 0, false)]
    [InlineData(0, -1, false)]
    [InlineData(-1, -1, true)]
    [InlineData(0, -2, false)]
    [InlineData(-2, -2, true)]
    [InlineData(-2, -3, true)]
    public void Diagonal(int x2, int y2, bool diagonal)
    {
        var p1 = (0, 0);
        var p2 = (x2, y2);
        _ = Day9Helper.Diagonal(p1, p2).Should().Be(diagonal);
    }

    [Theory]
    [InlineData(1, 1, -1, -1, 0, 0)] // follow diagonally
    [InlineData(1, 1, 1, 1, 1, 1)] // dont move
    [InlineData(1, 1, -1, 0, 0, 1)] // follow diagonally (the strange case)
    [InlineData(1, -1, -1, 0, 0, -1)] // follow diagonally (the strange case)

    [InlineData(1, 0, -1, -1, 0, 0)] // follow up in line
    [InlineData(1, 0, -1, 0, 0, 0)] // follow up in line
    [InlineData(1, 0, -1, 1, 0, 0)] // follow up in line
    public void Follow(int moveX, int moveY, int tailX, int tailY, int tailMoveX, int tailMoveY)
    {
        var pRoot = (0, 0);
        var pMove = (moveX, moveY);
        var pTail = (tailX, tailY);
        var pTailMove = (tailMoveX, tailMoveY);

        _ = Day9Helper.Follow(pRoot, pMove, pTail).Should().Be(pTailMove);
    }
}