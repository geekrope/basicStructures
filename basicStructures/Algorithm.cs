using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicStructures
{
    static class Algorithm
    {
        public static int LevenshteinDistanceRecursive(string s1, string s2, int i, int j)
        {
            if (s1 == "" || s2 == "")
            {
                return s1.Length + s2.Length;
            }

            if (s1[i] == s2[j])
            {
                if (i == 0 || j == 0)
                {
                    return i + j;
                }
                else
                {
                    return LevenshteinDistanceRecursive(s1, s2, i - 1, j - 1);
                }
            }

            if (i > 0 && j > 0)
            {
                return Extentions.Min(LevenshteinDistanceRecursive(s1, s2, i - 1, j),
                LevenshteinDistanceRecursive(s1.ReplaceAt(i, s2[j]), s2, i - 1, j - 1),
                LevenshteinDistanceRecursive(s1.Insert(i, s2[j].ToString()), s2, i - 1, j - 1)) + 1;
            }
            else if (i > 0)
            {
                return Extentions.Min(LevenshteinDistanceRecursive(s1, s2, i - 1, j)) + 1;
            }
            else if (j > 0)
            {
                return Extentions.Min(LevenshteinDistanceRecursive(s1, s2, i, j - 1)) + 1;
            }
            else
            {
                return s1[i] == s2[j] ? 0 : 1;
            }
        }
        public static int LevenshteinDistance(string s1, string s2)
        {
            if (s1 == "" || s2 == "")
            {
                return s1.Length + s2.Length;
            }

            var distances = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++) //distance between empty string and non-empty is the greatest length (length of non-empty string)
            {
                distances[i, 0] = i;
            }

            for (int j = 0; j <= s2.Length; j++) //distance between empty string and non-empty is the greatest length (length of non-empty string)
            {
                distances[0, j] = j;
            }

            for (int i = 1; i < s1.Length + 1; i++)
            {
                for (int j = 1; j < s2.Length + 1; j++)
                {
                    var substitutionCost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                    //[i-1, j-1] - insert or replace symbol (we move one step back in each string because symbols of [i-1, j-1] are equal)
                    //[i-1, j] - remove symbol in first string
                    //[i, j-1] - remove symbol in second string
                    distances[i, j] = Extentions.Min(distances[i - 1, j - 1] + substitutionCost, distances[i - 1, j] + 1, distances[i, j - 1] + 1);
                }
            }

            return distances[s1.Length, s2.Length];
        }
    }
}
