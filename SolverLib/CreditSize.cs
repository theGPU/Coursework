using System;
using System.Collections.Generic;

namespace SolverLib
{
    public class CreditSize
    {
        public double Rate { get; set; }
        public double Payment { get; set; }
        public byte Yeards { get; set; }
        public CreditSize(double rate, double payment, byte yeards)
        {
            this.Rate = rate * 0.01 + 1;
            this.Payment = payment;
            this.Yeards = yeards;
        }

        public bool IsDataValid(out string[] errors)
        {
            var errorsList = new List<string>();
            if (Rate < 0)
                errorsList.Add("Rate не может быть меньше 0");
            if (Payment < 0)
                errorsList.Add("Payment не может быть меньше 0");

            errors = errorsList.ToArray();
            return errors.Length == 0;
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

        public double Solve() => Math.Round(Payment * GetRate() / Math.Pow(Rate, Yeards));
    }
}
