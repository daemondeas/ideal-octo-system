using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day9
    {
        public static void SeventeenthPuzzle(string program)
        {
            RunIntCodeProgram(ExtendMemory(ParseIntCode(program)));
        }

        private static List<long> ExtendMemory(List<long> program)
        {
            var size = program.Count() * 9;
            for (var i = 0; i < size; i++)
            {
                program.Add(0);
            }

            return program;
        }

        private static List<long> ParseIntCode(string input)
        {
            return input.Split(',').Select(p => p.StartsWith('-')
             ? -long.Parse(p.Substring(1))
             : long.Parse(p)).ToList();
        }

        private static List<long> RunIntCodeProgram(List<long> program)
        {
            var i = 0;
            var relativeBase = 0;
            long a, b;
            while (program[i] != 99)
            {
                int address;
                switch (program[i] % 100)
                {
                    case 1:
                        (a, b) = GetParameters(i, relativeBase, program);
                        address = program[i] / 10000 == 2
                            ? (int)program[i + 3] + relativeBase
                            : (int)program[i + 3];
                        program[address] = a + b;
                        i += 4;
                        break;
                    case 2:
                        (a, b) = GetParameters(i, relativeBase, program);
                        address = program[i] / 10000 == 2
                            ? (int)program[i + 3] + relativeBase
                            : (int)program[i + 3];
                        program[address] = a * b;
                        i += 4;
                        break;
                    case 3:
                        Console.Write("input: ");
                        a = int.Parse(Console.ReadLine());
                        address = program[i] / 100 == 2
                            ? (int)program[i + 1] + relativeBase
                            : (int)program[i + 1];
                        program[address] = a;
                        i += 2;
                        break;
                    case 4:
                        (a, _) = GetParameters(i, relativeBase, program);
                        Console.WriteLine($"output: {a}");
                        i += 2;
                        break;
                    case 5:
                        (a, b) = GetParameters(i, relativeBase, program);
                        i = (int)(a != 0 ? b : i + 3);
                        break;
                    case 6:
                        (a, b) = GetParameters(i, relativeBase, program);
                        i = (int)(a == 0 ? b : i + 3);
                        break;
                    case 7:
                        (a, b) = GetParameters(i, relativeBase, program);
                        address = program[i] / 10000 == 2
                            ? (int)program[i + 3] + relativeBase
                            : (int)program[i + 3];
                        program[address] = a < b ? 1 : 0;
                        i += 4;
                        break;
                    case 8:
                        (a, b) = GetParameters(i, relativeBase, program);
                        address = program[i] / 10000 == 2
                            ? (int)program[i + 3] + relativeBase
                            : (int)program[i + 3];
                        program[address] = a == b ? 1 : 0;
                        i += 4;
                        break;
                    case 9:
                        (a, _) = GetParameters(i, relativeBase, program);
                        relativeBase += (int)a;
                        i += 2;
                        break;
                    default:
                        Console.WriteLine($"Unknown instruction: {program[i]}");
                        return program;
                }
            }

            return program;
        }

        private static (long, long) GetParameters(int ptr, int rb, List<long> program)
        {
            long a, b;
            if (program[ptr] % 100 == 4 || program[ptr] % 100 == 9)
            {
                var mode = program[ptr] / 100;
                a = mode == 0
                    ? program[(int)program[ptr + 1]]
                    : mode == 1
                        ? program[ptr + 1]
                        : program[(int)program[ptr + 1] + rb];
                return (a, 0);
            }

            var modeA = (program[ptr] % 1000) / 100;
            var modeB = (program[ptr] % 10000) / 1000;
            a = modeA == 0
                ? program[(int)program[ptr + 1]]
                : modeA == 1
                    ? program[ptr + 1]
                    : program[(int)program[ptr + 1] + rb];
            b = modeB == 0
                ? program[(int)program[ptr + 2]]
                : modeB == 1
                    ? program[ptr + 2]
                    : program[(int)program[ptr + 2] + rb];

            return (a, b);
        }
    }
}
