using System;
using System.IO;
namespace _4
{
    class Program
    {
        static int IsValid(string s)
        {
            string[] tab = { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            if (!s.Contains(tab[0])) return 0;
            if (!s.Contains(tab[1])) return 0;
            if (!s.Contains(tab[2])) return 0;
            if (!s.Contains(tab[3])) return 0;
            if (!s.Contains(tab[4])) return 0;
            if (!s.Contains(tab[5])) return 0;
            if (!s.Contains(tab[6])) return 0;

            string value = s.Substring(s.IndexOf(tab[0]) + tab[0].Length);
            if(value.IndexOf(' ')>0) value = value.Remove(value.IndexOf(' '));
            if (value.Length != 4 || (int.Parse(value) < 1920 || int.Parse(value) > 2002)) return 0;

            value = s.Substring(s.IndexOf(tab[1]) + tab[1].Length);
            if (value.IndexOf(' ') > 0) value = value.Remove(value.IndexOf(' '));
            if (value.Length != 4 || (int.Parse(value) < 2010 || int.Parse(value) > 2020)) return 0;

            value = s.Substring(s.IndexOf(tab[2]) + tab[2].Length);
            if (value.IndexOf(' ') > 0) value = value.Remove(value.IndexOf(' '));
            if (value.Length != 4 || (int.Parse(value) < 2020 || int.Parse(value) > 2030)) return 0;

            value = s.Substring(s.IndexOf(tab[3]) + tab[3].Length);
            if (value.IndexOf(' ') > 0) value = value.Remove(value.IndexOf(' '));
            if (value.Contains("cm"))
            {
                value = value.Remove(value.IndexOf("cm"));
                if (int.Parse(value) < 150 || int.Parse(value) > 193) return 0;
            }
            else if (value.Contains("in"))
            {
                value = value.Remove(value.IndexOf("in"));
                if (int.Parse(value) < 59 || int.Parse(value) > 76) return 0;
            }
            else return 0;

            value = s.Substring(s.IndexOf(tab[4]) + tab[4].Length);
            if (value.IndexOf(' ') > 0) value = value.Remove(value.IndexOf(' '));
            if (value.Length != 7) return 0;
            if (value[0] != '#') return 0;
            for (int i = 1; i < 7; i++)
            {
                if (!"0123456789abcdef".Contains(value[i])) return 0;
            }

            value = s.Substring(s.IndexOf(tab[5]) + tab[5].Length);
            if (value.IndexOf(' ') > 0) value = value.Remove(value.IndexOf(' '));
            if (!(value == "amb" || value == "blu" || value == "brn" || value == "gry" || value == "grn" || value == "hzl" || value == "oth")) return 0;

            value = s.Substring(s.IndexOf(tab[6]) + tab[6].Length);
            if (value.IndexOf(' ') > 0) value = value.Remove(value.IndexOf(' '));
            if (value.Length != 9) return 0;
            for (int i = 0; i < 8; i++)
            {
                if (!"0123456789".Contains(value[i])) return 0;
            }

            return 1;
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int valid = 0;
            string line = String.Empty, current;
            while (stream.Peek() >= 0)
            {
                current = stream.ReadLine();
                if (!String.IsNullOrEmpty(current))
                {
                    line += " " + current;
                    continue;
                }
                valid += IsValid(line);
                line = String.Empty;
            }
            valid += IsValid(line);
            stream.Close();
            Console.WriteLine("Found {0} valid passports\n", valid);
        }
    }
}