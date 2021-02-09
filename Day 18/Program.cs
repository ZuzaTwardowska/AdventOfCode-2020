using System;
using System.IO;
using System.Collections.Generic;

namespace _18
{
    class Program
    {
        static ulong Calculate(string s)
        {
            while (s.Contains('('))
            {
                string sub = s;
                while (sub.Contains('(') && sub.IndexOf('(') < sub.IndexOf(')'))
                {
                    sub = sub.Substring(sub.IndexOf('(') + 1);
                }
                sub = sub.Remove(sub.IndexOf(')'));
                ulong c = Calculate(sub);
                s = s.Remove(s.IndexOf('(' + sub)) + c.ToString() + s.Substring(s.IndexOf(sub + ')') + sub.Length + 1);
            }
            ulong count = 0;
            char operation = '+';
            int num = 0;
            for(int i=0;i<s.Length;i++)
            {
                if (s[i] == ' ') continue;
                if(s[i]=='+' || s[i]=='*')
                {
                    operation = s[i];
                    continue;
                }
                while(i < s.Length && s[i] != ' ')
                {
                    num *= 10;
                    num += int.Parse(s[i].ToString());
                    i++;
                }
                if(operation == '+')
                {
                    count += (ulong)num;
                }
                else
                {
                    count *= (ulong)num;
                }
                num = 0;
            }
            return count;
        }
        static ulong Calculate2(string s)
        {
            while (s.Contains('('))
            {
                string sub = s;
                while (sub.Contains('(') && sub.IndexOf('(') < sub.IndexOf(')'))
                {
                    sub = sub.Substring(sub.IndexOf('(') + 1);
                }
                sub = sub.Remove(sub.IndexOf(')'));
                ulong c = Calculate2(sub);
                s = s.Remove(s.IndexOf('(' + sub)) + c.ToString() + s.Substring(s.IndexOf(sub + ')') + sub.Length + 1);
            }
            Stack<ulong> values = new Stack<ulong>();
            ulong num = 0;
            char last_operation = '*';
            foreach(char c in s)
            {
                switch(c)
                {
                    case ' ': 
                        break;
                    case '+':
                    case '*':
                        values.Push(num);
                        if(last_operation=='+')
                        {
                            values.Push(values.Pop() + values.Pop());
                        }
                        last_operation = c;
                        num = 0;
                        break;
                    default:
                        num *= 10;
                        num += ulong.Parse(c.ToString());
                        break;
                }
            }
            values.Push(num);
            if (last_operation == '+')
            {
                values.Push(values.Pop() + values.Pop());
            }
            ulong res = 1;
            while(values.Count>0)
            {
                res *= values.Pop();
            }
            return res;
        }
        
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            List<string> equations = new List<string>();
            while (stream.Peek() > 0)
            {
                equations.Add(stream.ReadLine());
            }

            ulong sum = 0, sum2 = 0;
            foreach (string s in equations)
            {
                if (s == "\n") continue;
                sum += Calculate(s);
                sum2 += Calculate2(s);
            }
            Console.WriteLine("Part One: " + sum);
            Console.WriteLine("Part Two: " + sum2);
        }
    }
}
