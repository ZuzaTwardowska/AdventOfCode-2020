using System;
using System.IO;
using System.Collections.Generic;

namespace _14
{
    class Program
    {
        static int[] ValToBinary(ulong value)
        {
            int[] val = new int[36];
            for(int i = 35; i>=0;i--)
            {
                if (value % 2 == 1)
                {
                    val[i] = 1;
                    value -= 1;
                }
                else val[i] = 0;
                value /= 2;
            }
            return val;
        }
        static ulong BinaryToVal(int[] value)
        {
            ulong val = 0;
            ulong b = 1;
            for(int i = 35;i>=0;i--)
            {
                if(value[i]==1)
                {
                    val += b;
                }
                b *= 2;
            }
            return val;
        }
        static ulong ApplyMask(string mask,ulong value)
        {
            int[] val = ValToBinary(value);
            for(int i=0;i<36;i++)
            {
                switch(mask[i])
                {
                    case '1':
                        val[i] = 1;
                        break;
                    case '0':
                        val[i] = 0;
                        break;
                    default:
                        break;
                }
            }
            return BinaryToVal(val);
        }
        static void float2(int d,int[] bits, Dictionary<ulong, ulong> memory,ulong value,int[] c)
        {
            if (d < 0) return;
            c[bits[d]] = 0;
            if (memory.ContainsKey(BinaryToVal(c))) memory[BinaryToVal(c)] = value;
            else memory.Add(BinaryToVal(c), value);
            float2(d-1, bits, memory, value, c);
            c[bits[d]] = 1;
            if (memory.ContainsKey(BinaryToVal(c))) memory[BinaryToVal(c)] = value;
            else memory.Add(BinaryToVal(c), value);
            float2(d - 1, bits, memory, value, c);
        }
        static void Part2(Dictionary<ulong,ulong> memory,string mask,ulong value,int cell)
        {
            int[] c = ValToBinary((ulong)cell);
            List<int> bits = new List<int>();
            for (int i = 0; i < 36; i++)
            {
                switch (mask[i])
                {
                    case '1':
                        c[i] = 1;
                        break;
                    case '0':
                        break;
                    case 'X':
                        bits.Add(i);
                        c[i] = 0;
                        break;
                    default:
                        break;
                }
            }
            int[] bit = bits.ToArray();
            float2(bit.Length-1, bit, memory, value, c);
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            string mask= "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            string line;
            Dictionary<int, ulong> memory = new Dictionary<int, ulong>();
            Dictionary<ulong, ulong> memory2 = new Dictionary<ulong, ulong>();
            while(stream.Peek()>0)
            {
                line = stream.ReadLine();
                if(line.Contains("mask"))
                {
                    mask = line.Substring(line.IndexOf("mask") + "mask = ".Length);
                }
                else
                {
                    int cell = int.Parse(line.Remove(line.IndexOf("]")).Substring(line.IndexOf("[") + 1));
                    ulong value = ulong.Parse(line.Substring(line.IndexOf("=") + 1));
                    Part2(memory2, mask, value, cell);
                    value = ApplyMask(mask, value);
                    if (memory.ContainsKey(cell)) memory[cell] = value;
                    else memory.Add(cell, value);
                }
            }
            stream.Close();
            ulong sum = 0, sum2 = 0;
            foreach(ulong i in memory.Values)
            {
                sum += i;
            }
            foreach(ulong i in memory2.Values)
            {
                sum2 += i;
            }
            Console.WriteLine("1.Sum in memory: {0}\n", sum);
            Console.WriteLine("2.Sum in memory: {0}\n", sum2);
        }
    }
}
