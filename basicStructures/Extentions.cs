using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicStructures
{
    public static class Extentions
    {
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
        public static int Min(params int[] numbers)
        {
            var min = numbers[0];

            for (int index = 1; index < numbers.Length; index++)
            {
                if (numbers[index] < min)
                {
                    min = numbers[index];
                }
            }

            return min;
        }
    }
}
