using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _19
{
    class Program
    {
        static int Count(Dictionary<int, List<string>> rules,Dictionary<int, string> short_rules, List<string> messages)
        {
            List<string> combinations = new List<string>();
            foreach (var rule in rules[0].First().Split(" "))
            {
                int rule_num = int.Parse(rule);
                if (short_rules.ContainsKey(rule_num)) // if a simple rule is reached
                {
                    if (combinations.Count == 0) // add simple rules as combinations
                    {
                        combinations.Add(short_rules[rule_num].First().ToString());
                    }
                    else // add a simple rule as ending to each combination
                    {
                        List<string> tempComb = new List<string>();
                        foreach (var c in combinations)
                        {
                            tempComb.Add(c + short_rules[rule_num].First().ToString());
                        }
                        combinations = new List<string>(tempComb);
                    }
                }
                else // recursively for normal rules
                {
                    List<string> recComb = RecursiveCombinations(rules,short_rules,messages,rule_num);
                    if (combinations.Count == 0) // just add if there is no earlier rule
                    {
                        combinations.AddRange(recComb);
                    }
                    else // add as ending to each existing combination
                    {
                        List<string> tempComb = new List<string>();
                        foreach (var c in combinations)
                        {
                            foreach (var r in recComb)
                            {
                                tempComb.Add(c + r);
                            }
                        }
                        combinations = new List<string>(tempComb);
                    }
                }
            }
            return combinations.Intersect(messages).Count();
        }
        static List<string> RecursiveCombinations(Dictionary<int, List<string>> rules, Dictionary<int, string> short_rules, List<string> messages,int rule_num)
        {
            List<string> combination = new List<string>();
            foreach (var subrule in rules[rule_num])
            {
                List<string> recComb = new List<string>();
                foreach (var rule in subrule.Split(" "))
                {
                    if (short_rules.ContainsKey(int.Parse(rule)))
                    {
                        if (recComb.Count == 0)
                        {
                            recComb.Add(short_rules[int.Parse(rule)].First().ToString());
                        }
                        else
                        {
                            List<string> tempComb = new List<string>();
                            foreach (var r in recComb)
                            {
                                tempComb.Add(r + short_rules[int.Parse(rule)].First().ToString());
                            }
                            recComb = new List<string>(tempComb);
                        }
                    }
                    else
                    {
                        List<string> recCombTemp = RecursiveCombinations(rules, short_rules, messages,int.Parse(rule));
                        if (recComb.Count == 0)
                        {
                            recComb.AddRange(recCombTemp);
                        }
                        else
                        {
                            List<string> tempComb = new List<string>();
                            foreach (var r in recComb)
                            {
                                foreach (var rr in recCombTemp)
                                {
                                    tempComb.Add(r + rr);
                                }
                            }
                            recComb = new List<string>(tempComb);
                        }
                    }
                }
                combination.AddRange(recComb);
            }
            return combination;
        }
        static int PartTwo(Dictionary<int, string> rules, Dictionary<int, string> short_rules, List<string> messages)
        {
            int numberEightOccurece = 0;
            int numberElevenOccurece = 0;
            int max_length = messages.Select(s => s.Length).Max() / 8;
            string regex = String.Empty;
            Regex numbers = new Regex(@"\d+");

            while (true)
            {
                if (String.IsNullOrEmpty(regex))
                {
                    regex = rules[0];
                }

                Dictionary<int, string> resovedRules = new Dictionary<int, string>();

                string[] startRegex = regex.Split(" ");
                foreach (string s in startRegex)
                {
                    if (s == " " || s == "|" || s == "a" || s == "b")
                    {
                        continue;
                    }

                    MatchCollection numberCollection = numbers.Matches(s);
                    string number = String.Empty;
                    if (numberCollection.Count() == 0)
                    {
                        continue;
                    }
                    else
                    {
                        number = numberCollection.First().Value;
                        int ruleNum = int.Parse(number);
                        if (ruleNum == 8)
                        {
                            numberEightOccurece++;
                        }
                        else if (ruleNum == 11)
                        {
                            numberElevenOccurece++;
                        }

                        if (resovedRules.ContainsKey(ruleNum))
                        {
                            break;
                        }
                        else
                        {
                            resovedRules.Add(ruleNum, "");
                        }

                        string replace = String.Empty;
                        if (short_rules.ContainsKey(ruleNum))
                        {
                            replace = short_rules[ruleNum];
                        }
                        else
                        {
                            replace = rules[ruleNum];

                            if ((ruleNum == 8 && numberEightOccurece == max_length) || (ruleNum == 11 && numberElevenOccurece == max_length))
                            {
                                replace = rules[ruleNum].Split("|")[0].Trim();
                            }
                        }
                        Regex numberWithBoundary = new Regex(@"\b" + number + @"\b");
                        regex = numberWithBoundary.Replace(regex, $"({replace})");
                    }
                }
                if (!numbers.IsMatch(regex))
                {
                    break;
                }
            }
            regex = regex.Replace(" ", "");
            int counter = 0;
            Regex regexWithBoundary = new Regex(@"\b" + regex + @"\b");
            foreach (string s in messages)
            {
                if (regexWithBoundary.IsMatch(s))
                {
                    counter++;
                }
            }
            return counter;
        }

        static void Main(string[] args)
        {
            string path = "data.txt";
            string[] lines = File.ReadAllLines(path);
            Dictionary<int, List<string>> rules = new Dictionary<int, List<string>>();
            Dictionary<int, string> short_rules = new Dictionary<int, string>();
            List<string> messages = new List<string>();
            Dictionary<int, string> rules1 = new Dictionary<int, string>();
            int i = 0;
            do
            {
                int num = int.Parse(lines[i].Remove(lines[i].IndexOf(':')));
                rules1.Add(num, lines[i].Split(":")[1].Trim());
                if (lines[i].Contains("\""))
                {
                    short_rules.Add(num, lines[i].Split(":")[1].Trim().Replace("\"", ""));
                    i++;
                    continue;
                }
                List<string> subrules = new List<string>();
                foreach (var s in lines[i].Split(":")[1].Trim().Split("|"))
                {
                    subrules.Add(s.Trim());
                }
                rules.Add(num, subrules);
                i++;
            } while (lines[i] != string.Empty);
            i++;
            while (i < lines.Length)
            {
                messages.Add(lines[i++]);
            }
            
            Console.WriteLine("Part One: " + Count(rules,short_rules,messages));

            rules1[8] = "42 | 42 8";
            rules1[11] = "42 31 | 42 11 31";

            Console.WriteLine("Part Two: " + PartTwo(rules1,short_rules, messages));

        }
    }
}
