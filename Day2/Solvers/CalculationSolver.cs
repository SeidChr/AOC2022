namespace Day2.Solvers;

public class CalculationSolver : OrdinarySolver
{
    public override Move LooseTo(Move opponent) => (Move)FLooseTo((int)opponent);

    public override Move WinOver(Move opponent) => (Move)FWinOver((int)opponent);

    private static int FWinOver(int opponent) => (opponent % 3) + 1;

    private static int FLooseTo(int opponent) => ((opponent + 1) % 3) + 1;

    public override int GetScore(Move myself, Move opponent)
        => (myself, opponent) switch
        {
            (Move.Rock, Move.Paper) => 0 + 1,        // 1 2 -> 1 // 0 1 -> 0 // -1  0 -> 0    1 -1   = 0    -1   =  0
            (Move.Paper, Move.Scissors) => 0 + 2,    // 2 3 -> 2 // 1 2 -> 1 //  0 +1 -> 1    0 +1   = 1    +1   =  1
            (Move.Scissors, Move.Rock) => 0 + 3,     // 3 1 -> 3 // 2 0 -> 2 // +1 -1 -> 2    2 +1-1 = 2    +1-1 =  0
            (Move.Rock, Move.Rock) => 3 + 1,         // 1 1 -> 4 // 0 0 -> 3 // -1 -1 -> 3    5 -1-1 = 3    -1-1 = -2
            (Move.Paper, Move.Paper) => 3 + 2,       // 2 2 -> 5 // 1 1 -> 4 //  0  0 -> 4    4      = 4         =  0
            (Move.Scissors, Move.Scissors) => 3 + 3, // 3 3 -> 6 // 2 2 -> 5 // +1 +1 -> 5    3 +1+1 = 5    +1+1 =  2
            (Move.Rock, Move.Scissors) => 6 + 1,     // 1 3 -> 7 // 0 2 -> 6 // -1 +1 -> 6    6 -1+1 = 6    -1+1 =  0
            (Move.Paper, Move.Rock) => 6 + 2,        // 2 1 -> 8 // 1 0 -> 7 //  0 -1 -> 7    8 -1   = 7    -1   = -1
            (Move.Scissors, Move.Paper) => 6 + 3,    // 3 2 -> 9 // 2 1 -> 8 // +1  0 -> 8    7 +1   = 8    +1   =  1
        };
}