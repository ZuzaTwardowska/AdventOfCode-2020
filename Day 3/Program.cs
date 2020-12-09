using System;
using System.IO;
namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            long result = 1;
            StreamReader stream;
            int i, tree_count, step;
            string line;
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            for (step = 1; step <= 7; step += 2)
            {
                stream = new StreamReader(path);
                i = 0;
                tree_count = 0;
                line = stream.ReadLine();
                Console.WriteLine(line);
                while (stream.Peek() >= 0)
                {
                    line = stream.ReadLine();
                    i += step;
                    if (i >= line.Length) i %= line.Length;
                    if (line[i] == '.') line = line.Remove(i) + 'O' + line.Substring(i + 1);
                    else if (line[i] == '#')
                    {
                        line = line.Remove(i) + 'X' + line.Substring(i + 1);
                        tree_count++;
                    }
                    Console.WriteLine(line);
                }
                stream.Close();
                result *= tree_count;
                Console.WriteLine("Tress encountered for step = {0} : {1}\n", step, tree_count);
            }
            stream = new StreamReader(path);
            i = 0;
            tree_count = 0;
            line = stream.ReadLine();
            step = 1;
            bool skip = true;
            Console.WriteLine(line);
            while (stream.Peek() >= 0)
            {
                line = stream.ReadLine();
                if (skip == true)
                {
                    skip = false;
                    Console.WriteLine(line);
                    continue;
                }
                i += step;
                if (i >= line.Length) i %= line.Length;
                if (line[i] == '.') line = line.Remove(i) + 'O' + line.Substring(i + 1);
                else if (line[i] == '#')
                {
                    line = line.Remove(i) + 'X' + line.Substring(i + 1);
                    tree_count++;
                }
                Console.WriteLine(line);
                skip = true;
            }
            stream.Close();
            result *= tree_count;
            Console.WriteLine("Tress encountered for step = 2 down : {0}\n", tree_count);
            Console.WriteLine("\nResult of multiplication: {0}\n", result);
        }
    }
}
