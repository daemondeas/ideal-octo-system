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
                    case "p7":
                        Day4.SeventhPuzzle(args[1]);
                        break;
                    case "p8":
                        Day4.EigthPuzzle(args[1]);
                        break;
                    case "p9":
                        Day5.NinthPuzzle(args[1]);
                        break;
                    case "p10":
                        Day5.TenthPuzzle(args[1]);
                        break;
                    case "p11":
                        Day6.EleventhPuzzle(args.Skip(1).ToArray());
                        break;
                    case "p12":
                        Day6.TwelthPuzzle(args.Skip(1).ToArray());
                        break;
                    default:
                        Console.WriteLine("Hello World!");
                        break;
                }
            }
        }
    }
}
