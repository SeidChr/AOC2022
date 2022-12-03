var filename = "input.txt";

var watch = new System.Diagnostics.Stopwatch();

watch.Start();

var result = File
    .ReadLinesAsync(filename)
    .ToBlockingEnumerable()
    .Select(line =>
    {
        var opponent = GetOpponentMove(line);
        var control = GetMyselfChar(line);
        var riggedMyself = GetRiggedMyselfMove(control, opponent);
        return (GetScore(GetMyselfMove(control), opponent), GetScore(riggedMyself, opponent));
    })
    .Aggregate((x, y) => (x.Item1 += y.Item1, x.Item2 += y.Item2));

watch.Stop();

Console.WriteLine($"1: {result.Item1} 2: {result.Item2} in {watch.ElapsedMilliseconds}ms");

static Move GetRiggedMyselfMove(char control, Move opponent)
    => control switch
    {
        'X' => LooseTo(opponent),
        'Y' => opponent,
        'Z' => WinOver(opponent),
    };

static char GetMyselfChar(string line) => line[2];
static Move GetMyselfMove(char myself) => (Move)(myself - 87);
static Move GetOpponentMove(string line) => (Move)(line[0] - 64);

static Move WinOver(Move opponent) => opponent switch
{
    Move.Rock => Move.Paper,
    Move.Paper => Move.Scissors,
    Move.Scissors => Move.Rock,
};

static Move LooseTo(Move opponent) => opponent switch
{
    Move.Rock => Move.Scissors,
    Move.Paper => Move.Rock,
    Move.Scissors => Move.Paper,
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
        _ => throw new NotImplementedException(),
    };

public enum Move
{
    Unknown = 0,
    Rock = 1,
    Paper = 2,
    Scissors = 3,
}

