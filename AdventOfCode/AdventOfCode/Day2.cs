using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day2
    {
        public static void ThirdPuzzle(string program)
        {
            var newProgram = ArrayToIntCode(
                RunIntCodeProgram(
                    ConvertTo1202Program(
                        ParseIntCode(program))));
            Console.WriteLine(newProgram);
            Console.ReadLine();
        }

        public static void FourthPuzzle(string program)
        {
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var newProgram = RunIntCodeProgram(
                        ConvertToSpecificProgram(
                            ParseIntCode(program), i, j));
                    if (newProgram[0] == 19690720)
                    {
                        Console.WriteLine(ArrayToIntCode(newProgram));
                        Console.ReadLine();
                        return;
                    }
                }
            }
        }

        private static List<int> ParseIntCode(string input)
        {
            return input.Split(',').Select(int.Parse).ToList();
        }

        private static string ArrayToIntCode(List<int> program)
        {
            return string.Join(',', program);
        }

        private static List<int> RunIntCodeProgram(List<int> program)
        {
            for (var i = 0; program[i] != 99; i += 4)
            {
                switch (program[i])
                {
                    case 1:
                        program[program[i + 3]] = program[program[i + 1]]
                            + program[program[i + 2]];
                        break;
                    case 2:
                        program[program[i + 3]] = program[program[i + 1]]
                            * program[program[i + 2]];
                        break;
                }
            }

            return program;
        }

        private static List<int> ConvertTo1202Program(List<int> program)
        {
            program[1] = 12;
            program[2] = 2;
            return program;
        }

        private static List<int> ConvertToSpecificProgram(List<int> program, int noun, int verb)
        {
            program[1] = noun;
            program[2] = verb;
            return program;
        }
    }
}
