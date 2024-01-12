using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace basicStructures
{
    class Polynomial
    {
        public static Polynomial operator *(Polynomial polynomial1, Polynomial polynomial2)
        {
            int[] newKoefficients = new int[polynomial1.Power + polynomial2.Power + 1];

            for (int koefPower1 = 0; koefPower1 < polynomial1.Koefficients.Length; koefPower1++)
            {
                for (int koefPower2 = 0; koefPower2 < polynomial2.Koefficients.Length; koefPower2++)
                {
                    var koefIndex = koefPower1 + koefPower2;
                    newKoefficients[koefIndex] += polynomial1.Koefficients[koefPower1] * polynomial2.Koefficients[koefPower2];
                }
            }

            return new Polynomial(newKoefficients);
        }

        public int[] Koefficients
        {
            get; set;
        }
        public int Power
        {
            get => Koefficients.Length - 1;
        }

        private string PrintTerm(int power)
        {
            var koefficient = Koefficients[power];

            var sign = koefficient < 0 ? "-" : (power == 0 ? "" : "+");
            var koefficientString = (koefficient == 1 || koefficient == -1) ? "" : Math.Abs(koefficient).ToString();
            var term = power == 0 ? "" : (power == 1 ? "x" : "x^" + power.ToString());

            if (koefficient != 0)
            {
                return $"{sign}{koefficientString}{term}";
            }
            else
            {
                return "";
            }
        }

        public override string ToString()
        {
            StringBuilder result = new();

            for (int power = 0; power < Koefficients.Length; power++)
            {
                result.Append(PrintTerm(power));
            }

            return result.ToString();
        }

        public Polynomial(int[] koefficients)
        {
            this.Koefficients = koefficients;
        }
    }    
}
