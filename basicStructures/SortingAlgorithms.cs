using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicStructures
{
    public static class SortingAlgorithms
    {
        public static void QuickSort<T>(this T[] array, int from, int to) where T : IComparable
        {
            if (to - from <= 1)
            {
                return;
            }

            var left = from;
            var right = to;
            var barrier = array[left];

            for (int index = from; index <= to; index++)
            {
                var comparison = barrier.CompareTo(array[index]);

                if (comparison > 0)
                {
                    var temp = array[left];

                    array[left] = array[index];
                    array[index] = temp;

                    left++;
                }
                else if (comparison < 0)
                {
                    var temp = array[right];

                    array[right] = array[index];
                    array[index] = temp;

                    right--;
                }
            }

            QuickSort(array, 0, left);
            QuickSort(array, right, to);
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
