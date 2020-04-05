using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day11
    {
        public static void TwentyfirstPuzzle(string program)
        {
            var robot = new PausableLongCodeProgram
            {
                Program = ExtendMemory(ParseIntCode(program)),
                Input = new List<long> { 0 },
                Output = new List<long>()
            };
            var paintedArea = RunRobot(robot);
            Console.WriteLine(paintedArea.Keys.Count());
        }

        public static void TwentysecondPuzzle(string program)
        {
            var robot = new PausableLongCodeProgram
            {
                Program = ExtendMemory(ParseIntCode(program)),
                Input = new List<long> { 1 },
                Output = new List<long>()
            };
            var paintedArea = RunRobot(robot);
            PrintPaintArea(paintedArea);
        }

        private static void PrintPaintArea(Dictionary<Point, int> area)
        {
            var xMin = area.Keys.Min(p => p.X);
            var xMax = area.Keys.Max(p => p.X);
            var yMin = area.Keys.Min(p => p.Y);
            var yMax = area.Keys.Max(p => p.Y);

            for (var i = yMin; i <= yMax; i++)
            {
                for (var j = xMin; j <= xMax; j++)
                {
                    var colour = area.TryGetValue(new Point(j, i), out var c)
                        ? c
                        : 0;
                    Console.Write(colour == 0 ? ' ' : '#');
                }

                Console.Write('\n');
            }
        }

        private static Dictionary<Point, int> RunRobot(PausableLongCodeProgram robot)
        {
            var result = new Dictionary<Point, int>();
            var currentPoint = new Point(0, 0);
            var direction = Direction.Up;
            while (!RunIntCodeProgram(robot))
            {
                RunIntCodeProgram(robot);
                var length = robot.Output.Count();
                result[currentPoint] = (int)robot.Output[length - 2];
                direction = direction.Turn((int)robot.Output[length - 1]);
                currentPoint = TraverseOneStep(currentPoint, direction);
                var colour = result.TryGetValue(currentPoint, out var c)
                    ? c
                    : 0;
                robot.Input.Add(colour);
            }

            return result;
        }

        private static Direction Turn(this Direction currentDirection, int turn)
        {
            if (turn == 0)
            {
                return (Direction)(((int)currentDirection + 1) % 4);
            }

            return currentDirection == Direction.Up
                ? Direction.Right
                : currentDirection - 1;
        }

        private static Point TraverseOneStep(Point position, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(position.X, position.Y - 1);
                case Direction.Left:
                    return new Point(position.X - 1, position.Y);
                case Direction.Down:
                    return new Point(position.X, position.Y + 1);
                case Direction.Right:
                    return new Point(position.X + 1, position.Y);
                default:
                    throw new IndexOutOfRangeException();
            }
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
                        a = pausableProgram.Input[inputPointer++];
                        address = program[i] / 100 == 2
                            ? (int)program[i + 1] + relativeBase
                            : (int)program[i + 1];
                        program[address] = a;
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
