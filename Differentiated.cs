using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SolverLib
{
    public class Differentiated
    {
        float Rate { get; set; }
        int Months { get; set; }
        int PaymentMonth { get; set; }
        double Payment { get; set; }

        public Differentiated(float rate, int months, int paymentMonth, double payment)
        {
            this.Rate = rate;
            this.Months = months;
            this.PaymentMonth = paymentMonth;
            this.Payment = payment;
        }

        public double Solve()
        {
            var rate = Rate / 100 + 1;
            uint s = (uint) (Payment / ((PaymentMonth * rate - (PaymentMonth - 1)) / Months));
            int multiplier = 1 + Enumerable.Range(1, Months - 1).Sum() / Months;
            return Math.Round(s*(multiplier * rate - (multiplier - 1))*1000);
        }
    }
}
