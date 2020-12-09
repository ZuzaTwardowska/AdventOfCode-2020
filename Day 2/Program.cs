using System;
using System.IO;
namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int valid1 = 0;
            int valid2 = 0;
            int min, max, count;
            char c;
            while (stream.Peek() >= 0)
            {
                string line = stream.ReadLine();
                min = 0;
                max = 0;
                count = 0;
                for (int i = 0; i < line.IndexOf('-'); i++)
                {
                    min *= 10;
                    min += int.Parse(line[i].ToString());
                }
                for (int i = line.IndexOf('-') + 1; i < line.IndexOf(' '); i++)
                {
                    max *= 10;
                    max += int.Parse(line[i].ToString());
                }
                c = line[line.IndexOf(':') - 1];
                line = line.Substring(line.IndexOf(':') + 2);
                foreach(char elem in line)
                {
                    if (elem == c) count++;
                }
                if (count >= min && count <= max)
                {
                    valid1++;
                }
                if ((line[min - 1] == c && line[max - 1] != c) || (line[min - 1] != c && line[max - 1] == c))
                {
                    valid2++;
                }
            }
            Console.WriteLine("{0} passwords correct(first interpretation)\n", valid1);
            Console.WriteLine("{0} passwords correct(second interpretation)\n", valid2);
        }
    }
}
