using System;
using System.IO;
using System.Collections.Generic;

namespace _7
{
    class Program
    {
        static int Check(Dictionary<string, bool> bags, Queue<string> temp)
        {
            string current;
            string add;
            int count = 0;
            List<string> temp2 = new List<string>();
            foreach(string s in bags.Keys)
            {
                temp2.Add(s);
            }
            while(temp.Count != 0)
            {
                current = temp.Dequeue();
                foreach(string s in temp2)
                {
                    if (s.Contains(current) && s.IndexOf(current) != 0 && bags[s] == false)
                    {
                        bags[s] = true;
                        count++;
                        add = s.Remove(s.IndexOf(" bags contain"));
                        temp.Enqueue(add);
                    }
                }
            }
            return count;
        }
        static int CountBags(Dictionary<string, bool> bags, string a)
        {
            int count = 0;
            int val;
            string temp;
            List<string> temp2 = new List<string>();
            foreach (string s in bags.Keys)
            {
                temp2.Add(s);
            }
            foreach (string s in temp2)
            {
                if (s.Contains(a) && s.IndexOf(a) == 0)
                {
                    if (s.Contains("no other bags")) return 1;
                    for (int i = 0; i < s.Length; i++)
                    {
                        if ("123456789".Contains(s[i]))
                        {
                            val = int.Parse(s[i].ToString());
                            temp = s.Substring(i + 2);
                            temp = temp.Remove(temp.IndexOf(" bag"));
                            count += val * CountBags(bags, temp);
                        }
                    }
                }
            }
            if (a == "shiny gold") return count;
            return count + 1;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int count = 0;
            Dictionary<string, bool> bags = new Dictionary<string, bool>();
            while (stream.Peek() >= 0)
            {
                bags.Add(stream.ReadLine(),false);
            }
            stream.Close();
            Queue<string> temp = new Queue<string>();
            temp.Enqueue("shiny gold");

            count = Check(bags,temp);
            Console.WriteLine("First answear = {0}\n", count);
            count = CountBags(bags, "shiny gold");
            Console.WriteLine("Second answear = {0}\n", count);
        }
    }
}
