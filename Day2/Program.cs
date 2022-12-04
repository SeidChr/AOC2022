using Common;
var filename = "input.txt";
var map = new Dictionary<Move, ValueTuple<Move, Move>>
{
    [Move.Rock] = (Move.Paper, Move.Scissors),
    [Move.Paper] = (Move.Scissors, Move.Rock),
    [Move.Scissors] = (Move.Rock, Move.Paper),
};

filename
    .WriteSum((line, _) =>
    {
        var opponent = GetOpponentMove(line);
        var control = GetMyselfChar(line);
        // var riggedMyself = GetRiggedMyselfMove(control, opponent);
        // var riggedMyself = MGetRiggedMyselfMove(control, opponent);
        var riggedMyself = FGetRiggedMyselfMove(control, opponent);

        return (GetScore(GetMyselfMove(control), opponent), GetScore(riggedMyself, opponent));
    });

static Move GetRiggedMyselfMove(char control, Move opponent)
    => control switch
    {
        'X' => LooseTo(opponent),
        'Y' => opponent,
        'Z' => WinOver(opponent),
    };

static Move FGetRiggedMyselfMove(char control, Move opponent)
    => control switch
    {
        'X' => (Move)FLooseTo((int)opponent),
        'Y' => opponent,
        'Z' => (Move)FWinOver((int)opponent),
    };

Move MGetRiggedMyselfMove(char control, Move opponent)
    => control switch
    {
        'X' => MLooseTo(opponent),
        'Y' => opponent,
        'Z' => MWinOver(opponent),
    };

static char GetMyselfChar(string line) => line[2];
static Move GetMyselfMove(char myself) => (Move)(myself - 87);
static Move GetOpponentMove(string line) => (Move)(line[0] - 64);
Move MWinOver(Move opponent) => map![opponent].Item1;
Move MLooseTo(Move opponent) => map![opponent].Item2;
static int FWinOver(int opponent) => (opponent % 3) + 1;
static int FLooseTo(int opponent) => ((opponent + 1) % 3) + 1;
static Move WinOver(Move opponent) => opponent switch
{
    // ( $_ % 3 ) + 1
    Move.Rock => Move.Paper,     // 1 -> 2
    Move.Paper => Move.Scissors, // 2 -> 3
    Move.Scissors => Move.Rock,  // 3 -> 1
};

static Move LooseTo(Move opponent) => opponent switch
{
    // (($_ + 1) % 3) + 1
    Move.Rock => Move.Scissors,  // 1 -> 3
    Move.Paper => Move.Rock,     // 2 -> 1
    Move.Scissors => Move.Paper, // 3 -> 2
};

static int GetScore(Move myself, Move opponent)
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

public enum Move
{
    Unknown = 0,
    Rock = 1,
    Paper = 2,
    Scissors = 3,
}

