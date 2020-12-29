using System;
using System.Collections.Generic;

namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "9,19,1,6,0,5,4";
            int current_turn = 1, last = 0, curr=0;
            Dictionary<int, int[]> game = new Dictionary<int, int[]>();
            foreach(string i in data.Split(','))
            {
                int[] tab = { current_turn, 0 };
                game.Add(int.Parse(i), tab);
                current_turn++;
                last = int.Parse(i);
            }
            while(current_turn <= 30000000)
            {
                if (game[last][1] == 0) curr = 0;
                else curr = game[last][0] - game[last][1];
                if (game.ContainsKey(curr))
                {
                    game[curr][1] = game[curr][0];
                    game[curr][0] = current_turn;
                }
                else
                {
                    int[] tab = { current_turn, 0 };
                    game.Add(curr, tab);
                }
                last = curr;
                if (current_turn == 2020) Console.WriteLine("2020th number:{0}", last);
                current_turn++;
            }
            Console.WriteLine("30000000th number:{0}", last);
        }
    }
}
