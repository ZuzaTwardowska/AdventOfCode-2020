using System;
using System.IO;
using System.Collections.Generic;

namespace _5
{
    class Program
    {
        static int Number(string s)
        {
            int min = 0, max = 127, left = 0, right = 7;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'B') min += (max - min + 1) / 2;
                if (s[i] == 'F') max -= (max - min + 1) / 2;
                if (s[i] == 'L') right -= (right - left + 1) / 2;
                if (s[i] == 'R') left += (right - left + 1) / 2;
            }
            if (max != min) Console.WriteLine("Something went wrong\n");
            if (left != right) Console.WriteLine("Something went wrong\n");
            return min * 8 + left;
        }
        static void Main(string[] args)
        {
            int seat;
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            Dictionary<int, bool> seats = new Dictionary<int, bool>();
            for (int i = 0; i <= 127 * 8 + 7; i++)
            {
                seats.Add(i, false);
            }
            int max = 0;
            while (stream.Peek() >= 0)
            {
                seat = Number(stream.ReadLine());
                if (seat > max) max = seat;
                seats[seat] = true;
            }
            Console.WriteLine("Highest seat number: {0}\n", max);
            stream.Close();
            foreach(int x in seats.Keys)
            {
                if (seats[x] == false && seats[x + 1] == true && seats[x - 1] == true)
                {
                    Console.WriteLine("Your seat is: " + x);
                    break;
                }
            }

        }
        
    }
}
