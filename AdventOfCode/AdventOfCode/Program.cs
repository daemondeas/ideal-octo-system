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
                    case "p13":
                        Day7.ThirteenthPuzzle(args[1]);
                        break;
                    case "p14":
                        Day7.FourteenthPuzzle(args[1]);
                        break;
                    case "p15":
                        Day8.FifteenthPuzzle(args[1]);
                        break;
                    case "p16":
                        Day8.SixteenthPuzzle(args[1]);
                        break;
                    case "p17":
                    case "p18":
                        Day9.SeventeenthPuzzle(args[1]);
                        break;
                    case "p19":
                        Day10.NineteenthPuzzle(args.Skip(1).ToArray());
                        break;
                    case "p20":
                        Day10.TwentiethPuzzle(args.Skip(1).ToArray());
                        break;
                    case "p21":
                        Day11.TwentyfirstPuzzle(args[1]);
                        break;
                    case "p22":
                        Day11.TwentysecondPuzzle(args[1]);
                        break;
                    case "p23":
                        Day12.TwentythirdPuzzle();
                        break;
                    case "p24":
                        Day12.TwentyfourthPuzzle();
                        break;
                    case "p25":
                        Day13.TwentyfifthPuzzle(args[1]);
                        break;
                    case "p26":
                        Day13.TwentysixthPuzzle(args[1]);
                        break;
                    case "p27":
                        Day14.TwentyseventhPuzzle();
                        break;
                    case "p28":
                        Day14.TwentyeighthPuzzle();
                        break;
                    default:
                        Console.WriteLine("Hello World!");
                        break;
                }
            }
        }
    }
}
