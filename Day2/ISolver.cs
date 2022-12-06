public interface ISolver
{
    char GetMyselfChar(string line);

    Move GetMyselfMove(char myself);

    Move GetOpponentMove(string line);

    Move GetRiggedMyselfMove(char control, Move opponent);

    int GetScore(Move myself, Move opponent);

    Move LooseTo(Move opponent);

    Move WinOver(Move opponent);
}
