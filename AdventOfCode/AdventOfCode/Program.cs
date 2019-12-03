using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Hello World!");
            }
            else
            {
                switch (args[0])
                {
                    case "p1":
                        Day1.FirstPuzzle(args.Skip(1).Select(int.Parse).ToArray());
                        break;
                    case "p2":
                        Day1.SecondPuzzle(args.Skip(1).Select(int.Parse).ToArray());
                        break;
                    case "p3":
                        Day2.ThirdPuzzle(args[1]);
                        break;
                    case "p4":
                        Day2.FourthPuzzle(args[1]);
                        break;
                    case "p5":
                        Day3.FifthPuzzle(args[1], args[2]);
                        break;
                    case "p6":
                        Day3.SixthPuzzle(args[1], args[2]);
                        break;
                    default:
                        Console.WriteLine("Hello World!");
                        break;
                }
            }
        }
    }
}
