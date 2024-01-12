using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace basicStructures
{
    public static class SortingAlgorithms
    {
        public static void QuickSort<T>(this T[] array, int from, int to) where T : IComparable
        {
            var left = from;
            var right = to;
            var barrier = array[left];

            while (left <= right)
            {
                while (barrier.CompareTo(array[left]) > 0)
                {
                    left++;
                }

                while (barrier.CompareTo(array[right]) < 0)
                {
                    right--;
                }

                if (left <= right)
                {
                    var temp = array[left];

                    array[left] = array[right];
                    array[right] = temp;

                    left++; right--;
                }
            }

            if (from < right) { QuickSort(array, from, right); }
            if (left < to) { QuickSort(array, left, to); }
        }
        public static T[] BubbleSort<T>(this T[] array) where T : IComparable
        {
            var copy = array.ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i < j && array[i].CompareTo(array[j]) > 0)
                    {
                        var t = copy[i];
                        copy[i] = array[j];
                        array[j] = t;
                    }
                }
            }

            return copy;
        }
    }
}
