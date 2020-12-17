using System;
using System.IO;
using System.Collections.Generic;

namespace _6
{
    class Program
    {
        static int Sum(string line)
        {
            int res = 0;
            List<char> temp = new List<char>();
            foreach(char c in line)
            {
                if (c == ' ') continue;
                if(!temp.Contains(c))
                {
                    temp.Add(c);
                    res++;
                }
            }
            return res;
        }
        static int Sum(List<string> arg)
        {
            List<char> temp = new List<char>();
            foreach(char c in arg[0])
            {
                temp.Add(c);
            }
            foreach (string s in arg)
            {
                foreach(char c in arg[0])
                {
                    if (!s.Contains(c)) temp.Remove(c);
                }
            }
            return temp.Count;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int count = 0;
            string line = String.Empty, current;
            while (stream.Peek() >= 0)
            {
                current = stream.ReadLine();
                if (!String.IsNullOrEmpty(current))
                {
                    line += current;
                    continue;
                }
                count += Sum(line);
                line = String.Empty;
            }
            count += Sum(line);
            stream.Close();
            Console.WriteLine("Sum for 'anyone said yes' = {0}\n", count);
            stream = new StreamReader(path);
            count = 0;
            List<string> temp = new List<string>();
            while (stream.Peek() >= 0)
            {
                current = stream.ReadLine();
                if (!String.IsNullOrEmpty(current))
                {
                    temp.Add(current);
                    continue;
                }
                count += Sum(temp);
                temp = new List<string>();
            }
            count += Sum(temp);
            stream.Close();
            Console.WriteLine("Sum for 'everyone said yes' = {0}\n", count);
        }
    }
}
