var filename = "input.txt";

var result = File
    .ReadLinesAsync(filename)
    .ToBlockingEnumerable()
    .Select(GetGame)
    .Sum(game => GetScore(game.myself, game.opponent));

Console.WriteLine(result);

var riggedResult = File
    .ReadLinesAsync(filename)
    .ToBlockingEnumerable()
    .Select(GetRiggedGame)
    .Sum(game => GetScore(game.myself, game.opponent));

Console.WriteLine(riggedResult);

static (Move myself, Move opponent) GetGame(string line)
    => ((Move)(line[2] - 87), (Move)(line[0] - 64));

static (Move myself, Move opponent) GetRiggedGame(string line)
{
    var opponent = (Move)(line[0] - 64);
    var control = line[2];
    var myself = control switch
    {
        'X' => LooseTo(opponent),
        'Y' => opponent,
        'Z' => WinOver(opponent),
    };

    // Console.WriteLine($"{opponent} {control} -> {myself}");
    return (myself, opponent);
}

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
        (Move.Rock, Move.Rock) => 3 + 1,
        (Move.Paper, Move.Paper) => 3 + 2,
        (Move.Scissors, Move.Scissors) => 3 + 3,
        (Move.Rock, Move.Paper) => 0 + 1,
        (Move.Rock, Move.Scissors) => 6 + 1,
        (Move.Paper, Move.Rock) => 6 + 2,
        (Move.Paper, Move.Scissors) => 0 + 2,
        (Move.Scissors, Move.Rock) => 0 + 3,
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
