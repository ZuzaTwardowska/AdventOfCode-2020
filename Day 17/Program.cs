using System;
using System.Collections.Generic;
using System.IO;

namespace _17
{
    class Program
    {
        static class Cube
        {
            public static SortedSet<(int, int, int)> active_cubes = new SortedSet<(int, int, int)>();
            public static int max_x = 0, max_y = 0, max_z = 0;

            public static int CountActive(int x, int y, int z)
            {
                int c = 0;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            if (i == 0 && j == 0 && k == 0) continue;
                            if (Cube.active_cubes.Contains((x + i, y + j, z + k))) c++;
                        }
                    }
                }
                return c;
            }
            public static void Cycle(int cycle)
            {
                SortedSet<(int, int, int)> new_active_cubes = new SortedSet<(int, int, int)>();

                for (int z = -max_z - cycle; z <= max_z + cycle; z++)
                {
                    for (int x = -max_x - cycle; x <= max_x + cycle; x++)
                    {
                        for (int y = -max_y - cycle; y <= max_y + cycle; y++)
                        {
                            if (active_cubes.Contains((x, y, z)))
                            {
                                int count = CountActive(x, y, z);
                                if (count == 2 || count == 3) new_active_cubes.Add((x, y, z));
                            }
                            else
                            {
                                int count = CountActive(x, y, z);
                                if (count == 3) new_active_cubes.Add((x, y, z));
                            }
                        }
                    }
                }
                active_cubes = new_active_cubes;
            }
        }
        static class Cube4d
        {
            public static SortedSet<(int, int, int, int)> active_cubes = new SortedSet<(int, int, int, int)>();
            public static int max_x = 0, max_y = 0, max_z = 0, max_w = 0;

            public static int CountActive(int x, int y, int z, int w)
            {
                int c = 0;
                for (int d = -1; d < 2; d++)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            for (int k = -1; k < 2; k++)
                            {
                                if (i == 0 && j == 0 && k == 0 && d == 0) continue;
                                if (Cube4d.active_cubes.Contains((x + i, y + j, z + k, w + d))) c++;
                            }
                        }
                    }
                }
                return c;
            }
            public static void Cycle(int cycle)
            {
                SortedSet<(int, int, int, int)> new_active_cubes = new SortedSet<(int, int, int, int)>();
                for (int w = -max_w - cycle; w <= max_w + cycle; w++)
                {
                    for (int z = -max_z - cycle; z <= max_z + cycle; z++)
                    {
                        for (int x = -max_x - cycle; x <= max_x + cycle; x++)
                        {
                            for (int y = -max_y - cycle; y <= max_y + cycle; y++)
                            {
                                if (active_cubes.Contains((x, y, z,w)))
                                {
                                    int count = CountActive(x, y, z,w);
                                    if (count == 2 || count == 3) new_active_cubes.Add((x, y, z,w));
                                }
                                else
                                {
                                    int count = CountActive(x, y, z,w);
                                    if (count == 3) new_active_cubes.Add((x, y, z,w));
                                }
                            }
                        }
                    }
                }
                active_cubes = new_active_cubes;
            }
        }
        
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int number_of_cycles = 6;
            string line;
            int x = 0;
            while (stream.Peek() > 0)
            {
                line = stream.ReadLine();
                for(int y = 0;y<line.Length;y++)
                {
                    if (line[y] == '#')
                    {
                        if (Cube.max_x < x) Cube.max_x = x;
                        if (Cube.max_y < y) Cube.max_y = y;
                        Cube.active_cubes.Add((x, y, 0));
                    }
                }
                x++;
            }
            for(int i=0;i<number_of_cycles;i++)
            {
                Cube.Cycle(i+1);
            }
            Console.WriteLine("Part One: "+ Cube.active_cubes.Count);
            x = 0;
            stream.Close();
            stream = new StreamReader(path);
            while (stream.Peek() > 0)
            {
                line = stream.ReadLine();
                for (int y = 0; y < line.Length; y++)
                {
                    if (line[y] == '#')
                    {
                        if (Cube4d.max_x < x) Cube4d.max_x = x;
                        if (Cube4d.max_y < y) Cube4d.max_y = y;
                        Cube4d.active_cubes.Add((x, y, 0,0));
                    }
                }
                x++;
            }
            stream.Close();
            for (int i = 0; i < number_of_cycles; i++)
            {
                Cube4d.Cycle(i + 1);
            }
            Console.WriteLine("Part Two: " + Cube4d.active_cubes.Count);
        }
    }
}
