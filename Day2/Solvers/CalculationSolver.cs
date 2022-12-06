namespace Day2.Solvers;

public class CalculationSolver : OrdinarySolver
{
    public override Move LooseTo(Move opponent) => (Move)FLooseTo((int)opponent);

    public override Move WinOver(Move opponent) => (Move)FWinOver((int)opponent);

    private static int FWinOver(int opponent) => (opponent % 3) + 1;

    private static int FLooseTo(int opponent) => ((opponent + 1) % 3) + 1;
}