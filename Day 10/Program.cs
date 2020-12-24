using System;
using System.IO;
using System.Collections.Generic;

namespace _10
{
    class Program
    {
        static int OneAndThree(SortedSet<int> plugs)
        {
            int actual = 0, count1 = 0, count3 = 0;
            foreach(int i in plugs)
            {
                if (i - actual == 1) count1++;
                if (i - actual == 3) count3++;
                if (i - actual > 3) return 0;
                actual = i;
            }
            return count1 * (count3 + 1);
        }
        static long OptionsNumber(int[] plugs,int i,Dictionary<int,long> storage)
        {
            long count = 0;
            if (plugs[i] == plugs[plugs.Length - 1]) return 1;
            if(storage.ContainsKey(i))
            {
                return storage[i];
            }
            for (int j = i + 1;j<plugs.Length;j++)
            {
                if (plugs[j] - plugs[i] <= 3) count += OptionsNumber(plugs, j,storage);
                else break;
            }
            if(!storage.ContainsKey(i))
            {
                storage.Add(i, count);
            }
            return count;

        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            SortedSet<int> plugs = new SortedSet<int>();
            while (stream.Peek() >= 0)
            {
                plugs.Add(int.Parse(stream.ReadLine()));
            }
            stream.Close();
            Console.WriteLine("One and Three multiplication: {0}", OneAndThree(plugs));
            int[] plugstab = new int[plugs.Count + 1];
            int j = 1;
            plugstab[0] = 0;
            foreach(int i in plugs)
            {
                plugstab[j++] = i;
            }
            // without storing already counted data, it works definitely too slow
            Dictionary<int, long> storage = new Dictionary<int, long>();
            Console.WriteLine("Number od possibilities: {0}", OptionsNumber(plugstab,0,storage));
        }
    }
}
