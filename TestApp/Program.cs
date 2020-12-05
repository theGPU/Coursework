using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            //Console.WriteLine(new SolverLib.Annuity(12, 3512320, 3).Solve());
            //Console.WriteLine(new SolverLib.Annuity(13, 5107600, 2).Solve());
            //Console.WriteLine(new SolverLib.Annuity(10, 2928200, 4).Solve());

            //Console.WriteLine(new SolverLib.Annuity(12, 3512320, 3).SolveRaw());
            //Console.WriteLine(new SolverLib.Annuity(13, 5107600, 2).SolveRaw());
            //Console.WriteLine(new SolverLib.Annuity(10, 2928200, 4).SolveRaw());

            //2
            //Console.WriteLine(new SolverLib.Percent(1000000, 560000, 644100).Solve());
            //Console.WriteLine(new SolverLib.Percent(1000000, 510000, 649000).Solve());
            //Console.WriteLine(new SolverLib.Percent(1000000, 550000, 638400).Solve());

            //3
            Console.WriteLine(new SolverLib.Differentiated(3, 9, 5, 57.5).Solve());
        }
    }
}
