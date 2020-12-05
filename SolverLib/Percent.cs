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

        public bool IsDataValid(out string[] errors)
        {
            var errorsList = new List<string>();
            if (FirtsPayment + SecondPayment < CreditSize)
                errorsList.Add("Сумма первого и второго платежа не может быть меньше размера кредита");

            errors = errorsList.ToArray();
            return errors.Length == 0;
        }

        public double Solve()
        {
            var d = (Math.Pow(FirtsPayment, 2)) + (4 * CreditSize * SecondPayment);
            return Math.Round((((FirtsPayment+Math.Sqrt(d))/(2*CreditSize))-1)*100);
        }
    }
}
