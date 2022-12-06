using Common;

using Day2;
using Day2.Solvers;

var filename = "input.txt";

new[] { new MappingSolver(), new OrdinarySolver(), new CalculationSolver() }.ForEach(solver => Solve(solver, filename));

static void Solve(ISolver solver, string filename) => filename
    .WriteSum((line, _) =>
    {
        var opponent = solver.GetOpponentMove(line);
        var control = solver.GetMyselfChar(line);
        var riggedMyself = solver.GetRiggedMyselfMove(control, opponent);

        return (solver.GetScore(solver.GetMyselfMove(control), opponent), solver.GetScore(riggedMyself, opponent));
    });
