using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day5
    {
        public static void NinthPuzzle(string program)
        {
            RunIntCodeProgram(ParseIntCode(program));
        }

        public static void TenthPuzzle(string program)
        {

        }

        private static List<int> ParseIntCode(string input)
        {
            return input.Split(',').Select(p => p.StartsWith('-')
             ? -int.Parse(p.Substring(1))
             : int.Parse(p)).ToList();
        }

        private static string ArrayToIntCode(List<int> program)
        {
            return string.Join(',', program);
        }

        private static List<int> RunIntCodeProgram(List<int> program)
        {
            var i = 0;
            int a, b;
            while (program[i] != 99)
            {
                switch (program[i] % 100)
                {
                    case 1:
                        (a, b) = GetParameters(i, program);
                        program[program[i + 3]] = a + b;
                        i += 4;
                        break;
                    case 2:
                        (a, b) = GetParameters(i, program);
                        program[program[i + 3]] = a * b;
                        i += 4;
                        break;
                    case 3:
                        Console.Write("input: ");
                        a = int.Parse(Console.ReadLine());
                        program[program[i + 1]] = a;
                        i += 2;
                        break;
                    case 4:
                        (a, _) = GetParameters(i, program);
                        Console.WriteLine($"output: {a}");
                        i += 2;
                        break;
                    case 5:
                        (a, b) = GetParameters(i, program);
                        i = a != 0 ? b : i + 3;
                        break;
                    case 6:
                        (a, b) = GetParameters(i, program);
                        i = a == 0 ? b : i + 3;
                        break;
                    case 7:
                        (a, b) = GetParameters(i, program);
                        program[program[i + 3]] = a < b ? 1 : 0;
                        i += 4;
                        break;
                    case 8:
                        (a, b) = GetParameters(i, program);
                        program[program[i + 3]] = a == b ? 1 : 0;
                        i += 4;
                        break;
                    default:
                        Console.WriteLine($"Unknown instruction: {program[i]}");
                        return program;
                }
            }

            return program;
        }

        private static (int, int) GetParameters(int ptr, List<int> program)
        {
            int a, b;
            if (program[ptr] % 100 == 4)
            {
                a = (program[ptr] / 100) == 0
                            ? program[program[ptr + 1]]
                            : program[ptr + 1];
                return (a, 0);
            }

            a = ((program[ptr] % 1000) / 100) == 0
                            ? program[program[ptr + 1]]
                            : program[ptr + 1];
            b = (program[ptr] / 1000) == 0
                ? program[program[ptr + 2]]
                : program[ptr + 2];

            return (a, b);
        }
    }
}
