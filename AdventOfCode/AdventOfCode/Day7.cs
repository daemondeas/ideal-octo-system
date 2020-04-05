using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day7
    {
        public static void ThirteenthPuzzle(string program)
        {
            var permutations = GetPermutations(new[] { 0, 1, 2, 3, 4 });

            Console.WriteLine(permutations.Max(p => RunProgramOnAmplifiers(program, p)));
        }

        public static void FourteenthPuzzle(string program)
        {
            var permutations = GetPermutations(new[] { 5, 6, 7, 8, 9 });

            Console.WriteLine(permutations.Max(p => RunProgramWithFeedbackLoopOnAmplifiers(program, p)));
        }

        private static int RunProgramOnAmplifiers(string program, int[] phaserSettings)
        {
            var input = 0;
            foreach (var setting in phaserSettings)
            {
                var inputs = new List<int> { setting, input };
                var output = RunIntCodeProgram(ParseIntCode(program), inputs);
                input = output.Last();
            }

            return input;
        }

        private static int RunProgramWithFeedbackLoopOnAmplifiers(string program, int[] phaserSettings)
        {
            var input = 0;
            var phasers = phaserSettings.Select(p => new PausableIntCodeProgram
            {
                Program = ParseIntCode(program),
                Input = new List<int> { p },
                Output = new List<int>()
            }).ToArray();

            while(phasers.Any(p => !p.ProgramHasFinished))
            {
                for(var i = 0; i < phasers.Count(); i++)
                {
                    phasers[i].Input.Add(input);
                    RunPausableIntCodeProgram(phasers[i]);
                    input = phasers[i].Output.Last();
                }
            }

            return input;
        }

        private static List<T[]> GetPermutations<T> (T[] elements)
        {
            var result = new List<T[]>(Factorial(elements.Length));
            if (elements.Length < 2)
            {
                result.Add(elements);
                return result;
            }

            for (var i = 0; i < elements.Length; i++)
            {
                var leftOver = new T[elements.Length - 1];
                for (var j = 0; j < elements.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var index = i > j ? j : j - 1;
                    leftOver[index] = elements[j];
                }

                var leftOverPermutations = GetPermutations(leftOver);
                foreach (var permutation in leftOverPermutations)
                {
                    var fullPermutation = new T[elements.Length];
                    fullPermutation[0] = elements[i];
                    Array.Copy(permutation, 0, fullPermutation, 1, permutation.Length);
                    result.Add(fullPermutation);
                }
            }

            return result;
        }

        private static int Factorial(int n)
        {
            return n < 2 ? 1 : n * Factorial(n - 1);
        }

        private static List<int> ParseIntCode(string input)
        {
            return input.Split(',').Select(p => p.StartsWith('-')
             ? -int.Parse(p.Substring(1))
             : int.Parse(p)).ToList();
        }

        private static List<int> RunIntCodeProgram(List<int> program, List<int> input)
        {
            var i = 0;
            var inputPointer = 0;
            int a, b;
            var output = new List<int>();
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
                        a = input[inputPointer++];
                        program[program[i + 1]] = a;
                        i += 2;
                        break;
                    case 4:
                        (a, _) = GetParameters(i, program);
                        output.Add(a);
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

            return output;
        }

        private static bool RunPausableIntCodeProgram(PausableIntCodeProgram pausableProgram)
        {
            var program = pausableProgram.Program;
            var i = pausableProgram.ProgramCounter;
            var inputPointer = pausableProgram.InputPointer;
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
                        a = pausableProgram.Input[inputPointer++];
                        program[program[i + 1]] = a;
                        i += 2;
                        break;
                    case 4:
                        (a, _) = GetParameters(i, program);
                        pausableProgram.Output.Add(a);
                        i += 2;
                        pausableProgram.Program = program;
                        pausableProgram.ProgramCounter = i;
                        pausableProgram.InputPointer = inputPointer;
                        return false;
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
                        return true;
                }
            }

            return true;
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
