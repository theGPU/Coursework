using SolverLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp
{
    public static class Proxy
    {
        public static double CalculateAnnuity(double rate, double payment, byte yeards)
        {
            var annuity = new CreditSize(rate, payment, yeards);
            return annuity.Solve();
        }

        public static double CalculatePercents(ulong creditSize, ulong firstPayment, ulong secondPayment)
        {
            var percents = new Percent(creditSize, firstPayment, secondPayment);
            return percents.Solve();
        }

        public static double CalculateDifferentiated(double rate, int months, int paymentMonth, double payment)
        {
            var differentiated = new Differentiated(rate, months, paymentMonth, payment/1000);
            return differentiated.Solve();
        }
    }
}
