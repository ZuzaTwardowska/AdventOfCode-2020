using System;
using System.IO;
using System.Collections.Generic;

namespace _12
{
    public class Ship
    {
        int direction;
        protected int east, north;
        public Ship()
        {
            direction = 90;
            east = 0;
            north = 0;
        }
        public void NewAction(string action)
        {
            switch (action[0])
            {
                case 'E':
                    east += int.Parse(action.Substring(1));
                    break;
                case 'W':
                    east -= int.Parse(action.Substring(1));
                    break;
                case 'N':
                    north += int.Parse(action.Substring(1));
                    break;
                case 'S':
                    north -= int.Parse(action.Substring(1));
                    break;
                case 'F':
                    if (direction == 0) north += int.Parse(action.Substring(1));
                    else if (direction == 180) north -= int.Parse(action.Substring(1));
                    else if (direction == 90) east += int.Parse(action.Substring(1));
                    else if (direction == 270) east -= int.Parse(action.Substring(1));
                    break;
                case 'L':
                    direction -= int.Parse(action.Substring(1));
                    break;
                case 'R':
                    direction += int.Parse(action.Substring(1));
                    break;
                default:
                    break;
            }
            direction %= 360;
            if (direction < 0) direction = 360 + direction;
        }
        public int Record() => Math.Abs(east) + Math.Abs(north);
    }
    public class WayportShip : Ship
    {
        int east_waypoint, north_waypoint;
        public WayportShip() : base()
        {
            east_waypoint = 10;
            north_waypoint = 1;
        }
        public new void NewAction(string action)
        {
            switch (action[0])
            {
                case 'E':
                    east_waypoint += int.Parse(action.Substring(1));
                    break;
                case 'W':
                    east_waypoint -= int.Parse(action.Substring(1));
                    break;
                case 'N':
                    north_waypoint += int.Parse(action.Substring(1));
                    break;
                case 'S':
                    north_waypoint -= int.Parse(action.Substring(1));
                    break;
                case 'F':
                    north += int.Parse(action.Substring(1)) * north_waypoint;
                    east += int.Parse(action.Substring(1)) * east_waypoint;
                    break;
                case 'L':
                    RotateWaypoint(360 - int.Parse(action.Substring(1)));
                    break;
                case 'R':
                    RotateWaypoint(int.Parse(action.Substring(1)));
                    break;
                default:
                    break;
            }
        }
        void RotateWaypoint(int degrees)
        {
            if (degrees % 360 == 0) return;
            degrees %= 360;
            if (degrees % 180 != 0)
            {
                int p = east_waypoint;
                east_waypoint = north_waypoint;
                north_waypoint = p;
            }
            switch (degrees)
            {
                case 180:
                    north_waypoint *= -1;
                    east_waypoint *= -1;
                    break;
                case 90:
                    north_waypoint *= -1;
                    break;
                case 270:
                    east_waypoint *= -1;
                    break;
                default:
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            Ship ship = new Ship();
            while (stream.Peek() >= 0)
            {
                ship.NewAction(stream.ReadLine());
            }
            stream.Close();
            Console.WriteLine("1.Manhattan distance: {0}",ship.Record());
            stream = new StreamReader(path);
            WayportShip whip = new WayportShip();
            while (stream.Peek() >= 0)
            {
                whip.NewAction(stream.ReadLine());
            }
            stream.Close();
            Console.WriteLine("2.Manhattan distance: {0}", whip.Record());
        }
    }
}
