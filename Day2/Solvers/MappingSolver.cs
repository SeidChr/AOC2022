namespace Day2.Solvers;

public class MappingSolver : OrdinarySolver
{
    private readonly Dictionary<Move, ValueTuple<Move, Move>> map = new()
    {
        [Move.Rock] = (Move.Paper, Move.Scissors),
        [Move.Paper] = (Move.Scissors, Move.Rock),
        [Move.Scissors] = (Move.Rock, Move.Paper),
    };

    public override Move WinOver(Move opponent) => this.map![opponent].Item1;

    public override Move LooseTo(Move opponent) => this.map![opponent].Item2;
}