using System;
using System.IO;
using System.Collections.Generic;

namespace _16
{
    class Program
    {
        class Field
        {
            public string name;
            public int[][] range;
            public int position;
            public Field(string line)
            {
                name = line.Remove(line.IndexOf(':'));
                if (line.Contains(" or "))
                {
                    range = new int[2][];
                    range[0] = new int[2];
                    range[1] = new int[2];
                    range[0][0] = int.Parse(line.Remove(line.IndexOf("-")).Substring(line.IndexOf(":") + 1));
                    range[0][1] = int.Parse(line.Remove(line.IndexOf(" or ")).Substring(line.IndexOf("-") + 1));
                    range[1][0] = int.Parse(line.Remove(line.LastIndexOf("-")).Substring(line.IndexOf("or ") + "or ".Length));
                    range[1][1] = int.Parse(line.Substring(line.LastIndexOf("-") + 1));
                }
                else
                {
                    range = new int[1][];
                    range[0] = new int[2];
                    range[0][0] = int.Parse(line.Remove(line.IndexOf("-")).Substring(line.IndexOf(":") + 1));
                    range[0][1] = int.Parse(line.Substring(line.IndexOf("-")));
                }
            }
        }
        static int CountInvalid(List<Field> fields,List<int[]> nearby)
        {
            int sum = 0;
            bool ok;
            foreach(int[] ticket in nearby)
            {
                foreach(int val in ticket)
                {
                    ok = false;
                    foreach(Field f in fields)
                    {
                        foreach (int[] tab in f.range)
                        {
                            if (val <= tab[1] && val >= tab[0])
                            {
                                ok = true;
                                break;
                            }
                        }
                        if (ok == true) break;
                    }
                    if (ok == false) sum += val;
                }
            }
            return sum;
        }
        static List<int[]> DeleteInvalid(List<Field> fields, List<int[]> nearby)
        {
            bool ok = false;
            List<int[]> temp = new List<int[]>();
            foreach (int[] ticket in nearby)
            {
                foreach (int val in ticket)
                {
                    ok = false;
                    foreach (Field f in fields)
                    {
                        foreach (int[] tab in f.range)
                        {
                            if (val <= tab[1] && val >= tab[0])
                            {
                                ok = true;
                                break;
                            }
                        }
                        if (ok == true) break;
                    }
                    if (ok == false) break;
                }
                if (ok == true) temp.Add(ticket);
            }
            return temp;
        }
        static void NamePositions(List<Field> fields, List<int[]> nearby)
        {
            bool fail = false;
            Dictionary<Field,List<int>> poss = new Dictionary<Field, List<int>>();

            foreach(Field f in fields)
            {
                poss.Add(f, new List<int>());
                for (int i = 0; i < fields.Count; i++)
                {
                    foreach (int[] ticket in nearby)
                    {
                        fail = false;
                        if (ticket[i] > f.range[0][1] || ticket[i] < f.range[0][0])
                        {
                            fail = true;
                        }
                        if (f.range.Length == 2 && fail == true)
                        {
                            fail = false;
                            if (ticket[i] > f.range[1][1] || ticket[i] < f.range[1][0])
                            {
                                fail = true;
                            }
                        }
                        if (fail == true) break;
                    }
                    if (fail == true) continue;
                    poss[f].Add(i);
                }
            }
            int count = 0;
            while(count!=fields.Count)
            {
                foreach(Field f in fields)
                {
                    if(poss[f].Count == 1)
                    {
                        f.position = poss[f].ToArray()[0];
                        foreach(Field ff in poss.Keys)
                        {
                            poss[ff].Remove(f.position);
                        }
                        count++;
                    }
                }
            }
        }
        static long DMultiply(List<Field> fields, string ticket)
        {
            string[] t = ticket.Split(',');
            long res = 1;
            foreach(Field f in fields)
            {
                if (f.name.Contains("departure"))
                {
                    res *= int.Parse(t[f.position]);
                }
            }
            return res;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            string line, my_ticket="";
            List<Field> fields = new List<Field>();
            List<int[]> nearby = new List<int[]>();
            while (stream.Peek()>0)
            {                
                line = stream.ReadLine();
                if (line.Contains("your ticket"))
                {
                    my_ticket = stream.ReadLine();
                    continue;
                }
                if(line.Contains("nearby tickets"))
                {
                    while(stream.Peek()>0)
                    {
                        string[] ticket = stream.ReadLine().Split(',');
                        List<int> t = new List<int>();
                        foreach(string s in ticket)
                        {
                            t.Add(int.Parse(s));
                        }
                        nearby.Add(t.ToArray());
                    }
                    break;
                }
                if(line!=string.Empty) fields.Add(new Field(line));
            }
            stream.Close();
            Console.WriteLine("Ticket scanning error rate: {0}", CountInvalid(fields,nearby));
            nearby = DeleteInvalid(fields, nearby);
            NamePositions(fields, nearby);
            Console.WriteLine("Departure multiplications: {0}", DMultiply(fields, my_ticket));
        }
    }
}
