using System;

namespace SolverLib
{
    public class Annuity
    {
        public double Rate { get; set; }
        public double Payment { get; set; }
        public byte Yeards { get; set; }
        public Annuity(float rate, double payment, byte yeards)
        {
            this.Rate = rate * 0.01 + 1;
            this.Payment = payment;
            this.Yeards = yeards;
        }

        private double GetRate()
        {
            double newRate = 1;
            for (int i = 1; i < Yeards; i++)
            {
                newRate += Math.Pow(Rate, i);
            }
            return newRate;
        }

        public double SolveRaw() => Payment * GetRate() / Math.Pow(Rate, Yeards);

        public double Solve() => Math.Round(SolveRaw());
    }
}
