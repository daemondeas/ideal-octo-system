using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day13
    {
        public static void TwentyfifthPuzzle(string program)
        {
            var game = new PausableLongCodeProgram
            {
                Program = ExtendMemory(ParseIntCode(program)),
                Input = new List<long>(),
                Output = new List<long>()
            };
            var board = RunGame(game);

            Console.WriteLine(board.Values.Count(t => t == 2));
        }

        public static void TwentysixthPuzzle(string program)
        {
            var game = new PausableLongCodeProgram
            {
                Program = ExtendMemory(ParseIntCode(program)),
                Input = new List<long>(),
                Output = new List<long>()
            };
            game.Program[0] = 2;

            PlayGame(game);
        }

        private static void PlayGame(PausableLongCodeProgram game)
        {
            var ball = -1;
            var paddle = -1;
            var board = new Dictionary<Point, int>();
            var length = game.Output.Count();
            board[new Point(-1, 0)] = 0;
            while (!RunIntCodeProgram(game))
            {
                RunIntCodeProgram(game);
                RunIntCodeProgram(game);
                var i = game.Output.Count() - 3;
                var value = (int)game.Output[i + 2];
                board[new Point((int)game.Output[i], (int)game.Output[i + 1])]
                    = value;
                if (value == 3 && game.Output[i] != -1)
                {
                    paddle = (int)game.Output[i];
                }
                else if (value == 4 && game.Output[i] != -1)
                {
                    ball = (int)game.Output[i];
                }

                if (length == game.Output.Count())
                {
                    if (ball == paddle && game.Input.Last() != 0)
                    {
                        game.Input.Add(0);
                    }
                    else if (ball < paddle)
                    {
                        game.Input.Add(-1);
                    }
                    else if (ball > paddle)
                    {
                        game.Input.Add(1);
                    }
                }
                
                RenderGameBoard(board);
                length = game.Output.Count();
            }
        }

        private static void RenderGameBoard(Dictionary<Point, int> board)
        {
            var y = board.Keys.Max(k => k.Y);
            var x = board.Keys.Max(k => k.X);
            Console.Clear();
            Console.WriteLine($"({x}, {y}) - {board.Count()}");
            Console.WriteLine(board[new Point(-1, 0)]);

            for (var i = 0; i < y; i++)
            {
                for (var j = 0; j < x; j++)
                {
                    var tile = board.TryGetValue(new Point(j, i), out var t)
                        ? t
                        : 0;
                    switch (tile)
                    {
                        case 0:
                            Console.Write(' ');
                            break;
                        case 1:
                            Console.Write('#');
                            break;
                        case 2:
                            Console.Write('*');
                            break;
                        case 3:
                            Console.Write('=');
                            break;
                        case 4:
                            Console.Write('O');
                            break;
                    }
                }

                Console.Write('\n');
            }
        }

        private static Dictionary<Point, int> RunGame(PausableLongCodeProgram game)
        {
            var board = new Dictionary<Point, int>();
            while (!RunIntCodeProgram(game))
            {
                RunIntCodeProgram(game);
                RunIntCodeProgram(game);
                var i = game.Output.Count() - 3;
                board[new Point((int)game.Output[i], (int)game.Output[i + 1])]
                    = (int)game.Output[i + 2];
            }

            return board;
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

        private static bool RunIntCodeProgram(PausableLongCodeProgram pausableProgram)
        {
            var program = pausableProgram.Program;
            var i = pausableProgram.ProgramCounter;
            var inputPointer = pausableProgram.InputPointer;
            var relativeBase = pausableProgram.RelativeBase;
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
                        if (inputPointer == pausableProgram.Input.Count())
                        {
                            pausableProgram.Program = program;
                            pausableProgram.ProgramCounter = i;
                            pausableProgram.InputPointer = inputPointer;
                            pausableProgram.RelativeBase = relativeBase;
                            return false;
                        }
                        a = pausableProgram.Input[inputPointer++];
                        /*Console.Write("Gief Input: ");
                        var inp = Console.ReadLine();
                        a = inp == "a" ? -1 : inp == "d" ? 1 : 0;*/
                        address = program[i] / 100 == 2
                            ? (int)program[i + 1] + relativeBase
                            : (int)program[i + 1];
                        program[address] = a;
                        Console.WriteLine($"Read input: {a}");
                        i += 2;
                        break;
                    case 4:
                        (a, _) = GetParameters(i, relativeBase, program);
                        pausableProgram.Output.Add(a);
                        i += 2;
                        pausableProgram.Program = program;
                        pausableProgram.ProgramCounter = i;
                        pausableProgram.InputPointer = inputPointer;
                        pausableProgram.RelativeBase = relativeBase;
                        return false;
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
                        return true;
                }
            }

            pausableProgram.Program = program;
            pausableProgram.ProgramCounter = i;
            pausableProgram.InputPointer = inputPointer;
            pausableProgram.RelativeBase = relativeBase;
            return true;
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
