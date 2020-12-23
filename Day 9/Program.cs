using System;
using System.IO;
using System.Collections.Generic;

namespace _9
{
    class Program
    {
        static int FirstInvalid(List<string> lines)
        {
            Queue<string> temp = new Queue<string>();
            int i = 0;
            bool flag = false;
            foreach(string s in lines)
            { 
                if (i < 25)
                {
                    i++;
                    temp.Enqueue(s);
                    continue;
                }
                foreach(string n in temp)
                {
                    foreach(string m in temp)
                    {
                        if (int.Parse(n) + int.Parse(m) == int.Parse(s)) flag = true;
                        if (flag == true) break;
                    }
                    if (flag == true) break;
                }
                if (flag == false) return int.Parse(s);
                temp.Dequeue();
                temp.Enqueue(s);
                flag = false;
            }
            return 0;
        }
        static int MinMaxSearch(List<string> lines, int num)
        {
            int max = 0, sum = 0, min = num;
            string a;
            Queue<string> temp = new Queue<string>();
            foreach (string s in lines)
            {
                if (int.Parse(s) == num) return 0;
                while(sum > num)
                {
                    a = temp.Dequeue();
                    sum -= int.Parse(a);
                }
                if (sum == num) break;
                temp.Enqueue(s);
                sum += int.Parse(s);
                if (sum == num) break;
                
            }
            foreach (string s in temp)
            {
                if (int.Parse(s) < min) min = int.Parse(s);
                if (int.Parse(s) > max) max = int.Parse(s);
            }
            return min + max;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            List<string> lines = new List<string>();
            while (stream.Peek() >= 0)
            {
                lines.Add(stream.ReadLine());
            }
            stream.Close();
            int num = FirstInvalid(lines);
            Console.WriteLine("First invalid number: {0}\n", num);
            num = MinMaxSearch(lines,num);
            Console.WriteLine("Max + Min sequence element: {0}\n", num);
        }
    }
}
