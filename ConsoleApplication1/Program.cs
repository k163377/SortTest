using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static async Task<List<int>> CheckArray(List<int> arr)
        {
            //長さ2以下が来る予定
            if (!(arr.Count < 2 || arr[0] < arr[1]))//長さが2未満かちゃんと並んでれば何もしない
            {
                int i = arr[0];
                arr[0] = arr[1];
                arr[1] = i;
            }

            return arr;
        }
        
        public static async Task<List<int>> QuickSort_Main(List<int> arr)
        {
            //長さ3以上のarrが入ってくる予定
            var small = new List<int>();
            var big = new List<int>();
            
            for (var i = 1; i < arr.Count; i++)
            {
                if (arr[0] > arr[i]) small.Add(arr[i]);
                else big.Add(arr[i]);
            }

            var smalltask = (small.Count < 3) ? CheckArray(small) : QuickSort_Main(small);
            var bigtask = (big.Count < 3) ? CheckArray(big) : QuickSort_Main(big);

            await smalltask;
            await bigtask;
            small.AddRange(big);
            return small;
        }
        
        public static int[] QuickSort(int[] arr)
        {
            var l = new List<int>(arr);
            return  ((arr.Length < 3) ? CheckArray(l) : QuickSort_Main(l)).Result.ToArray();
        }

        public static int[] GenerateRandomArray()
        {
            const int Len = 50000;
            int[] array = new int[Len];
            Random r = new Random();

            for (int i = 0; i < Len; i++)
            {
                array[i] = r.Next(100);
            }

            return array;
        }
        
        public static void Main(string[] args)
        {
            var arr = GenerateRandomArray();
            
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            arr = QuickSort(arr);
            sw.Stop();
            
            Console.WriteLine(sw.Elapsed.Milliseconds);
        }
    }
}