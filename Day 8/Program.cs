using System;
using System.IO;
using System.Collections.Generic;

namespace _8
{
    class Program
    {
        static int Accumulate(Dictionary<string,bool> lines)
        {
            string[] temp = new string[lines.Count];
            int j = 0;
            int count = 0;
            foreach(string s in lines.Keys)
            {
                temp[j++] = s;
            }
            for (int i = 0; i < temp.Length; i++)
            {
                if (lines[temp[i]] == true) return count;
                lines[temp[i]] = true;
                if (temp[i].Contains("nop"))
                {
                    continue;
                }
                if (temp[i].Contains("jmp"))
                {
                    if((temp[i])[temp[i].IndexOf("jmp") + 4] == '+') i += int.Parse(temp[i].Substring(temp[i].IndexOf("jmp") + 5));
                    else i -= int.Parse(temp[i].Substring(temp[i].IndexOf("jmp") + 5));
                    i--;
                }
                if (temp[i].Contains("acc"))
                {
                    if((temp[i])[temp[i].IndexOf("acc") + 4] == '+') count += int.Parse(temp[i].Substring(temp[i].IndexOf("acc") + 5));
                    else count -= int.Parse(temp[i].Substring(temp[i].IndexOf("acc") + 5));

                }
            }
            return count;
        }
        static int Accumulate2(Dictionary<string, bool> lines)
        {
            string[] temp = new string[lines.Count];
            int j = 0;
            int count = 0;
            foreach (string s in lines.Keys)
            {
                temp[j++] = s;
            }
            for (int i = 0; i < temp.Length; i++)
            {
                if (lines[temp[i]] == true) return 0;
                lines[temp[i]] = true;
                if (temp[i].Contains("nop"))
                {
                    continue;
                }
                if (temp[i].Contains("jmp"))
                {
                    if ((temp[i])[temp[i].IndexOf("jmp") + 4] == '+') i += int.Parse(temp[i].Substring(temp[i].IndexOf("jmp") + 5));
                    else i -= int.Parse(temp[i].Substring(temp[i].IndexOf("jmp") + 5));
                    i--;
                }
                if (temp[i].Contains("acc"))
                {
                    if ((temp[i])[temp[i].IndexOf("acc") + 4] == '+') count += int.Parse(temp[i].Substring(temp[i].IndexOf("acc") + 5));
                    else count -= int.Parse(temp[i].Substring(temp[i].IndexOf("acc") + 5));

                }
            }
            return count;
        }
        static int Terminate(Dictionary<string, bool> lines)
        {
            int count = 0;
            Dictionary<string, bool> temp = new Dictionary<string, bool>();
            int round = 0;
            string a;
            while (count == 0)
            {
                int c = 0;
                foreach (string s in lines.Keys)
                {
                    if (s.Contains("jmp") || s.Contains("nop"))
                    {
                        c++;
                        if (c == round)
                        {
                            if (s.Contains("jmp")) a = s.Replace("jmp", "nop");
                            else a = s.Replace("nop", "jmp");
                            temp.Add(a, false);
                            continue;
                        }
                    }
                    temp.Add(s, false);
                }
                round++;
                count = Accumulate2(temp);
                temp = new Dictionary<string, bool>();
            }
            return count;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int count = 0;
            Dictionary<string,bool> lines = new Dictionary<string,bool>();
            int i = 0;
            while (stream.Peek() >= 0)
            {
                lines.Add(i.ToString() + stream.ReadLine(), false);
                i++;
            }
            stream.Close();
            count = Accumulate(lines);
            Console.WriteLine("Accumulator before second time = {0}\n", count);
            count = Terminate(lines);
            Console.WriteLine("Accumulator when terminates = {0}\n", count);
        }
    }
}
