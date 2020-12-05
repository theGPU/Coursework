using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SolverLib
{
    public class Differentiated
    {
        double Rate { get; set; }
        int Months { get; set; }
        int PaymentMonth { get; set; }
        double Payment { get; set; }

        public Differentiated(double rate, int months, int paymentMonth, double payment)
        {
            this.Rate = rate;
            this.Months = months;
            this.PaymentMonth = paymentMonth;
            this.Payment = payment;
        }

        public bool IsDataValid(out string[] errors)
        {
            var errorsList = new List<string>();
            if (Rate < 0)
                errorsList.Add("Rate не может быть отрицательным");
            if (Months < 1)
                errorsList.Add("Months не может быть меньше 1");
            if (PaymentMonth < 1)
                errorsList.Add("PaymentMonth не может быть меньше 1");
            if (PaymentMonth > Months)
                errorsList.Add("PaymentMonth не может быть больше Months");

            errors = errorsList.ToArray();
            return errors.Length == 0;
        }

        public double Solve()
        {
            var rate = Rate / 100 + 1;
            double s = Payment*Months / ((PaymentMonth * rate) - (PaymentMonth - 1));
            int multiplier = 1 + Enumerable.Range(1, Months - 1).Sum() / Months;
            return Math.Round(s*(multiplier * rate - (multiplier - 1))*1000);
        }
    }
}
