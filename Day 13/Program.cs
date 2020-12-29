using System;
using System.IO;
using System.Collections.Generic;

namespace _13
{
    class Program
    {
        static int FindBus(int timestamp,List<int> bus)
        {
            int temp = timestamp;
            while(true)
            {
                foreach(int i in bus)
                {
                    if (temp % i == 0)
                    {
                        return (temp - timestamp) * i;
                    }
                }
                temp++;
            }
        }
        static long Timestamp(List<long[]> list)
        {
            list.Sort((x, y) => { return (int)(-x[0] + y[0]); });
            long[][] elems = list.ToArray();
            long time = elems[0][0], period = 1;
            foreach(long[] elem in elems)
            {
                while ((time+elem[1]) % elem[0] != 0) time += period;
                period *= elem[0];
            }
            return time;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int timestamp = int.Parse(stream.ReadLine());
            string[] buses = stream.ReadLine().Split(',');
            List<int> bus = new List<int>();
            List<long[]> bus_offset = new List<long[]>();
            int t = 0;
            foreach(string i in buses)
            {
                if (i != "x")
                {
                    bus.Add(int.Parse(i));
                    long[] tab = { long.Parse(i), t }; 
                    bus_offset.Add(tab);
                }
                t++;
            }
            Console.WriteLine("Earliest bus multiplied by wait time: {0}\n", FindBus(timestamp, bus));
            Console.WriteLine("Timestamp:{0}", Timestamp(bus_offset));
        }
    }
}
