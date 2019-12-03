using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day3
    {
        public static void FifthPuzzle(string firstWire, string secondWire)
        {
            var firstWirePoints = GetWirePoints(firstWire);
            var secondWirePoints = GetWirePoints(secondWire);
            var closestIntersection = GetClosestIntersection(
                firstWirePoints,
                secondWirePoints);
            Console.WriteLine($"Closest distance: {closestIntersection}");
        }

        public static void SixthPuzzle(string firstWire, string secondWire)
        {
            var firstWirePoints = GetWeigthedPoints(firstWire);
            var secondWirePoints = GetWeigthedPoints(secondWire);
            var closestIntersection = GetFewestStepsIntersection(
                firstWirePoints,
                secondWirePoints);
            Console.WriteLine($"First: {closestIntersection} steps");
        }

        private static int GetClosestIntersection(
            HashSet<Point> wire1,
            HashSet<Point> wire2)
        {
            var intersections = wire1.Intersect(wire2);
            return intersections.Min(GetDistanceFromCentralPort);
        }

        private static int GetFewestStepsIntersection(
            Dictionary<Point, int> wire1,
            Dictionary<Point, int> wire2)
        {
            var intersections = wire1.Keys.Intersect(wire2.Keys);
            return intersections.Min(i => wire1[i] + wire2[i]);
        }

        private static HashSet<Point> GetWirePoints(string instructions)
        {
            var result = new HashSet<Point>();
            var splitInstructions = instructions.Split(',');
            var currentPoint = new Point(0, 0);
            foreach(var instruction in splitInstructions)
            {
                var linePoints = TraverseLine(currentPoint, instruction);
                currentPoint = linePoints.Last();
                foreach(var point in linePoints)
                {
                    result.Add(point);
                }
            }

            return result;
        }

        private static Dictionary<Point, int> GetWeigthedPoints(string instructions)
        {
            var result = new Dictionary<Point, int>();
            var splitInstructions = instructions.Split(',');
            var currentPoint = new Point(0, 0);
            var currentSteps = 0;
            foreach (var instruction in splitInstructions)
            {
                var linePoints = TraverseLine(currentPoint, instruction);
                currentPoint = linePoints.Last();
                foreach (var point in linePoints)
                {
                    if (result.ContainsKey(point))
                    {
                        ++currentSteps;
                        continue;
                    }

                    result.Add(point, ++currentSteps);
                }
            }

            return result;
        }

        private static List<Point> TraverseLine(Point start, string instruction)
        {
            var length = int.Parse(instruction.Substring(1));
            var result = new List<Point>(length);
            Func<Point, Point> traversalFunction = (instruction[0]) switch
            {
                'R' => p => new Point(p.X + 1, p.Y),
                'U' => p => new Point(p.X, p.Y + 1),
                'L' => p => new Point(p.X - 1, p.Y),
                'D' => p => new Point(p.X, p.Y - 1),
                _ => p => p,
            };
            var nextPoint = new Point(start.X, start.Y);
            for (var i = 0; i < length; i++)
            {
                nextPoint = traversalFunction(nextPoint);
                result.Add(nextPoint);
            }

            return result;
        }

        private static int GetDistanceFromCentralPort(Point point)
        {
            return Math.Abs(point.X) + Math.Abs(point.Y);
        }
    }
}
