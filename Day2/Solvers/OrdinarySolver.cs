namespace Day2.Solvers;

using Day2;

public class OrdinarySolver : SolverBase
{
    public override Move LooseTo(Move opponent) => opponent switch
    {
        // (($_ + 1) % 3) + 1
        Move.Rock => Move.Scissors,  // 1 -> 3
        Move.Paper => Move.Rock,     // 2 -> 1
        Move.Scissors => Move.Paper, // 3 -> 2
    };

    public override Move WinOver(Move opponent) => opponent switch
    {
        // ( $_ % 3 ) + 1
        Move.Rock => Move.Paper,     // 1 -> 2
        Move.Paper => Move.Scissors, // 2 -> 3
        Move.Scissors => Move.Rock,  // 3 -> 1
    };

    public override int GetScore(Move myself, Move opponent)
        => (myself, opponent) switch
        {
            (Move.Rock, Move.Paper) => 0 + 1,
            (Move.Rock, Move.Rock) => 3 + 1,
            (Move.Rock, Move.Scissors) => 6 + 1,
            (Move.Paper, Move.Scissors) => 0 + 2,
            (Move.Paper, Move.Paper) => 3 + 2,
            (Move.Paper, Move.Rock) => 6 + 2,
            (Move.Scissors, Move.Rock) => 0 + 3,
            (Move.Scissors, Move.Scissors) => 3 + 3,
            (Move.Scissors, Move.Paper) => 6 + 3,
        };
}