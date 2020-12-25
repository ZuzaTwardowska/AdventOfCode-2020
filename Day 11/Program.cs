using System;
using System.IO;
using System.Collections.Generic;

namespace _11
{
    class Program
    {
        static int CountSeats(Dictionary<int, string> seats)
        {
            bool flag = true;
            while (flag == true)
            {
                Dictionary<int, string> temp = new Dictionary<int, string>();
                flag = false;
                foreach (int i in seats.Keys)
                {
                    temp.Add(i, seats[i]);
                    for (int j = 0; j < seats[i].Length; j++)
                    {
                        if ((seats[i])[j] == 'L')
                        {
                            if (i != 0)
                            {
                                if ((seats[i - 1])[j] == '#') continue;
                                if (j != 0 && (seats[i - 1])[j - 1] == '#') continue;
                                if (j != seats[i].Length - 1 && (seats[i - 1])[j + 1] == '#') continue;
                            }
                            if (j != seats[i].Length - 1 && (seats[i])[j + 1] == '#') continue;
                            if (j != 0 && (seats[i])[j - 1] == '#') continue;
                            if (i != seats.Count - 1)
                            {
                                if ((seats[i + 1])[j] == '#') continue;
                                if (j != 0 && (seats[i + 1])[j - 1] == '#') continue;
                                if (j != seats[i].Length - 1 && (seats[i + 1])[j + 1] == '#') continue;
                            }
                            temp[i] = temp[i].Remove(j) + "#" + temp[i].Substring(j+1);
                            flag = true;
                        }
                        if ((seats[i])[j] == '#')
                        {
                            int count = 0;
                            if (i != 0)
                            {
                                if ((seats[i - 1])[j] == '#') count++;
                                if (j != 0 && (seats[i - 1])[j - 1] == '#') count++;
                                if (j != seats[i].Length - 1 && (seats[i - 1])[j + 1] == '#') count++;
                            }
                            if (j != seats[i].Length - 1 && (seats[i])[j + 1] == '#') count++;
                            if (j != 0 && (seats[i])[j - 1] == '#') count++;
                            if (i != seats.Count - 1)
                            {
                                if ((seats[i + 1])[j] == '#') count++;
                                if (j != 0 && (seats[i + 1])[j - 1] == '#') count++;
                                if (j != seats[i].Length - 1 && (seats[i + 1])[j + 1] == '#') count++;
                            }
                            if (count >= 4)
                            {
                                temp[i] = temp[i].Remove(j) + "L" + temp[i].Substring(j+1);
                                flag = true;
                            }
                        }
                    }
                }
                seats = temp;
            }
            int res = 0;
            foreach(int i in seats.Keys)
            {
                foreach(char c in seats[i])
                {
                    if (c == '#') res++;
                }
            }
            return res;
        }
        static int CountSeats2(Dictionary<int, string> seats)
        {
            bool flag = true;
            while (flag == true)
            {
                Dictionary<int, string> temp = new Dictionary<int, string>();
                flag = false;
                foreach (int i in seats.Keys)
                {
                    temp.Add(i, seats[i]);
                    for (int j = 0; j < seats[i].Length; j++)
                    {
                        if ((seats[i])[j] == 'L')
                        {
                            char c = 'L';
                            for (int k = 1; j - k >= 0; k++)
                            {
                                if (seats[i][j - k] != '.')
                                {
                                    c = seats[i][j - k];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; j + k < seats[i].Length; k++)
                            {
                                if (seats[i][j + k] != '.')
                                {
                                    c = seats[i][j + k];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; i - k >= 0; k++)
                            {
                                if (seats[i - k][j] != '.')
                                {
                                    c = seats[i - k][j];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; i + k < seats.Count; k++)
                            {
                                if (seats[i + k][j] != '.')
                                {
                                    c = seats[i + k][j];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; i - k >= 0 && j - k >= 0; k++)
                            {
                                if (seats[i - k][j - k] != '.')
                                {
                                    c = seats[i - k][j - k];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; i - k >= 0 && j + k < seats[i].Length; k++)
                            {
                                if (seats[i - k][j + k] != '.')
                                {
                                    c = seats[i - k][j + k];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; i + k < seats.Count && j - k >= 0; k++)
                            {
                                if (seats[i + k][j - k] != '.')
                                {
                                    c = seats[i + k][j - k];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            for (int k = 1; i + k < seats.Count && j + k < seats[i].Length; k++)
                            {
                                if (seats[i + k][j + k] != '.')
                                {
                                    c = seats[i + k][j + k];
                                    break;
                                }
                            }
                            if (c == '#') continue;
                            
                            temp[i] = temp[i].Remove(j) + "#" + temp[i].Substring(j + 1);
                            flag = true;
                        }
                        if ((seats[i])[j] == '#')
                        {
                            int count = 0;
                            char c = 'L';
                            for (int k = 1; j - k >= 0; k++)
                            {
                                if (seats[i][j - k] != '.')
                                {
                                    c = seats[i][j - k];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; j + k < seats[i].Length; k++)
                            {
                                if (seats[i][j + k] != '.')
                                {
                                    c = seats[i][j + k];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; i - k >= 0; k++)
                            {
                                if (seats[i - k][j] != '.')
                                {
                                    c = seats[i - k][j];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; i + k < seats.Count; k++)
                            {
                                if (seats[i + k][j] != '.')
                                {
                                    c = seats[i + k][j];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; i - k >= 0 && j - k >= 0; k++)
                            {
                                if (seats[i - k][j - k] != '.')
                                {
                                    c = seats[i - k][j - k];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; i - k >= 0 && j + k < seats[i].Length; k++)
                            {
                                if (seats[i - k][j + k] != '.')
                                {
                                    c = seats[i - k][j + k];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; i + k < seats.Count && j - k >= 0; k++)
                            {
                                if (seats[i + k][j - k] != '.')
                                {
                                    c = seats[i + k][j - k];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            c = 'L';
                            for (int k = 1; i + k < seats.Count && j + k < seats[i].Length; k++)
                            {
                                if (seats[i + k][j + k] != '.')
                                {
                                    c = seats[i + k][j + k];
                                    break;
                                }
                            }
                            if (c == '#') count++;
                            if (count >= 5)
                            {
                                temp[i] = temp[i].Remove(j) + "L" + temp[i].Substring(j + 1);
                                flag = true;
                            }
                        }
                    }
                }
                seats = temp;
            }
            int res = 0;
            foreach (int i in seats.Keys)
            {
                foreach (char c in seats[i])
                {
                    if (c == '#') res++;
                }
            }
            return res;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            Dictionary<int, string> seats = new Dictionary<int, string>();
            int i = 0;
            while (stream.Peek() >= 0)
            {
                seats.Add(i++,stream.ReadLine());
            }
            stream.Close();
            Console.WriteLine("1.Occupied seats: {0}", CountSeats(seats));
            Console.WriteLine("2.Occupied seats: {0}", CountSeats2(seats));
        }
    }
}
