using System;
using System.Collections.Generic;
using System.IO;

namespace _1
{
    class Program
    {
        static List<int> ReadFile(string file)
        {
            List<int> res = new List<int>();
            string path = Directory.GetCurrentDirectory() + "\\" + file;
            if (!File.Exists(file))
            {
                Console.WriteLine("Couldn't find the file.\n");
                return res;
            }
            StreamReader stream = new StreamReader(path);
            while (stream.Peek() >= 0)
            {
                res.Add(int.Parse(stream.ReadLine()));
            }
            return res;
        }
        static void Main()
        {
            List<int> data = ReadFile("data.txt");
            bool break_flag = false;
            foreach (int elem in data)
            {
                foreach (int comp in data)
                {
                    if (elem + comp == 2020)
                    {
                        Console.WriteLine("Result for 2 numbers: {0}", comp * elem);
                        break_flag = true;
                        break;
                    }
                    if (break_flag) break;
                }
            }
            break_flag = false;
            foreach (int elem in data)
            {
                foreach (int comp1 in data)
                {
                    foreach (int comp2 in data)
                    {
                        if (elem + comp1 + comp2 == 2020)
                        {
                            Console.WriteLine("Result for 3 numbers: {0}", comp1 * comp2 * elem);
                            break_flag = true;
                            break;
                        }
                    }
                    if (break_flag) break;
                }
                if (break_flag) break;
            }
        }
    }
}
