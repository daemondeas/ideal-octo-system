using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day10
    {
        public static void NineteenthPuzzle(string[] map)
        {
            var positions = GetAsteroidPositions(map);
            var asteroids = GetNumberOfVisibleAsteroids(positions);
            var bestAsteroid = GetBestAsteroid(asteroids);

            Console.WriteLine($"({bestAsteroid.X}, {bestAsteroid.Y})");
            Console.WriteLine(asteroids[bestAsteroid]);
        }

        public static void TwentiethPuzzle(string[] map)
        {
            var positions = GetAsteroidPositions(map);
            var asteroids = GetNumberOfVisibleAsteroids(positions);
            var bestAsteroid = GetBestAsteroid(asteroids);
            var startingAngle = Math.Atan2(-1, 0);
            var angles = GetAngles(bestAsteroid, positions);
            var orderedAngles = OrderAnglesClockWise(angles, startingAngle);
            var count = 0;
            var i = 0;
            var lastShotAsteroid = new Point(-1, -1);
            while (count < 200)
            {
                ++count;
                lastShotAsteroid = ShootAsteroid(bestAsteroid, positions, orderedAngles[i]);
                Console.WriteLine($"{count:D3}: ({lastShotAsteroid.X}, {lastShotAsteroid.Y}) - {orderedAngles[i]}");
                i++;
                if (i == orderedAngles.Count())
                {
                    i = 0;
                }
            }

            Console.WriteLine($"({lastShotAsteroid.X}, {lastShotAsteroid.Y})");
        }

        private static Point GetBestAsteroid(Dictionary<Point, int> asteroids)
        {
            var max = 0;
            var bestAsteroid = new Point(-1, -1);
            foreach (var asteroid in asteroids)
            {
                if (asteroid.Value > max)
                {
                    max = asteroid.Value;
                    bestAsteroid = asteroid.Key;
                }
            }

            return bestAsteroid;
        }

        private static List<Point> GetAsteroidPositions(string[] map)
        {
            var result = new List<Point>();
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        result.Add(new Point(j, i));
                    }
                }
            }

            return result;
        }

        private static Dictionary<Point, int> GetNumberOfVisibleAsteroids(List<Point> asteroids)
        {
            var result = new Dictionary<Point, int>();
            for(var i = 0; i < asteroids.Count(); i++)
            {
                result[asteroids[i]] = GetAngles(asteroids[i], asteroids).Count();
            }

            return result;
        }

        private static List<double> GetAngles(Point beholder, List<Point> map)
        {
            var result = new List<double>();
            foreach (var asteroid in map)
            {
                if (asteroid == beholder)
                {
                    continue;
                }

                result.Add(Math.Atan2(
                    asteroid.Y - beholder.Y,
                    asteroid.X - beholder.X));
            }

            return result.Distinct().ToList();
        }

        private static Point ShootAsteroid(Point beholder, List<Point> map, double angle)
        {
            var closestAsteroidOnLine = GetClosestPoint(beholder, map, angle);
            map.Remove(closestAsteroidOnLine);
            return closestAsteroidOnLine;
        }

        private static Point GetClosestPoint(Point beholder, List<Point> map, double angle)
        {
            var asteroids = GetPointsOnLine(beholder, map, angle);
            var closestAsteroid = new Point(-1, -1);
            var minDistance = double.MaxValue;
            foreach (var asteroid in asteroids)
            {
                var distance = Distance(beholder, asteroid);
                if (distance < minDistance)
                {
                    closestAsteroid = asteroid;
                    minDistance = distance;
                }
            }

            return closestAsteroid;
        }

        private static List<Point> GetPointsOnLine(Point beholder, List<Point> map, double angle)
        {
            var result = new List<Point>();
            foreach (var asteroid in map)
            {
                if (asteroid == beholder)
                {
                    continue;
                }

#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
                if (Math.Atan2(
                    asteroid.Y - beholder.Y,
                    asteroid.X - beholder.X) == angle)
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
                {
                    result.Add(asteroid);
                }
            }

            return result;
        }

        private static double Distance(Point a, Point b)
        {
            var xDist = Math.Abs(a.X - b.X);
            var yDist = Math.Abs(a.Y - b.Y);
            return Math.Sqrt(xDist * xDist + yDist * yDist);
        }

        private static List<double> OrderAnglesClockWise(List<double> angles, double start)
        {
            var firstPart = angles.Where(a => a >= start).OrderBy(a => a);
            var lastPart = angles.Where(a => a < start).OrderBy(a => a);
            return firstPart.Concat(lastPart).ToList();
        }
    }
}
