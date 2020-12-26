using System;
using System.IO;
using System.Collections.Generic;

namespace _12
{
    class Program
    {
        static (int,int,int) NewAction(int east,int north,int direction,string action)
        {
            switch(action[0])
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
                    if(direction == 0) north += int.Parse(action.Substring(1));
                    else if(direction == 180) north -= int.Parse(action.Substring(1));
                    else if(direction == 90) east += int.Parse(action.Substring(1));
                    else if(direction == 270) east -= int.Parse(action.Substring(1));
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
            return (east, north, direction);
        }
        static (int,int) RotateWaypoint(int degrees,int east, int north)
        {
            if (degrees % 360 == 0) return (east, north);
            degrees %= 360;
            if (degrees % 180 != 0)
            {
                int p = east;
                east = north;
                north = p;
            }
            switch(degrees)
            {
                case 180:
                    north *= -1;
                    east *= -1;
                    break;
                case 90:
                    north *= -1;
                    break;
                case 270:
                    east *= -1;
                    break;
                default:
                    break;
            }
            return (east, north);
        }
        static ((int, int),(int,int)) NewActionButWaypoint(int east, int north, string action,int east_waypoint, int north_waypoint)
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
                    (east_waypoint, north_waypoint) = RotateWaypoint(360 - int.Parse(action.Substring(1)), east_waypoint, north_waypoint);
                    break;
                case 'R':
                    (east_waypoint, north_waypoint) = RotateWaypoint(int.Parse(action.Substring(1)), east_waypoint, north_waypoint);
                    break;
                default:
                    break;
            }
            return ((east, north),(east_waypoint, north_waypoint));
        }
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\data.txt";
            StreamReader stream = new StreamReader(path);
            int east = 0, north = 0, direction = 90;
            while (stream.Peek() >= 0)
            {
                (east, north, direction) = NewAction(east, north, direction,stream.ReadLine());
            }
            stream.Close();
            Console.WriteLine("1.Manhattan distance: {0}",Math.Abs(east)+Math.Abs(north));
            stream = new StreamReader(path);
            east = 0;
            north = 0;
            int east_waypoint = 10, north_waypoint = 1;
            while (stream.Peek() >= 0)
            {
                ((east, north),(east_waypoint, north_waypoint)) = NewActionButWaypoint(east, north, stream.ReadLine(), east_waypoint, north_waypoint);
            }
            stream.Close();
            Console.WriteLine("2.Manhattan distance: {0}", Math.Abs(east) + Math.Abs(north));
        }
    }
}
