using System;

namespace basicStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var array = new int[] { 1, 4, 5, 3, 11, 45, 7, 458, 3, 5, 23, 5, 235 };

            array.QuickSort(0, array.Length - 1);
        }
    }
}