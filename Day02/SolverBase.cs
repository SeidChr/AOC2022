namespace Day2;

public abstract class SolverBase : ISolver
{
    public Move GetRiggedMyselfMove(char control, Move opponent)
        => control switch
        {
            'X' => this.LooseTo(opponent),
            'Y' => opponent,
            'Z' => this.WinOver(opponent),
        };

    public char GetMyselfChar(string line) => line[2];

    public Move GetMyselfMove(char myself) => (Move)(myself - 87);

    public Move GetOpponentMove(string line) => (Move)(line[0] - 64);

    public abstract int GetScore(Move myself, Move opponent);

    public abstract Move LooseTo(Move opponent);

    public abstract Move WinOver(Move opponent);

}