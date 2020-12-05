using System;
using System.Collections.Generic;
using System.Text;

namespace SolverLib
{
    public class Percent
    {
        ulong CreditSize { get; set; }
        ulong FirtsPayment { get; set; }
        ulong SecondPayment { get; set; }
        public Percent(ulong creditSize, ulong firstPayment, ulong secondPayment)
        {
            this.CreditSize = creditSize;
            this.FirtsPayment = firstPayment;
            this.SecondPayment = secondPayment;
        }

        public double Solve()
        {
            var d = (Math.Pow(FirtsPayment, 2)) + (4 * CreditSize * SecondPayment);
            return Math.Round((((FirtsPayment+Math.Sqrt(d))/(2*CreditSize))-1)*100);
        }

        public static double Solve(ulong creditSize, ulong firstPayment, ulong secondPayment)
        {
            var d = (Math.Pow(firstPayment, 2)) + (4 * creditSize * secondPayment);
            return Math.Round((((firstPayment + Math.Sqrt(d)) / (2 * creditSize)) - 1) * 100);
        }
    }
}
