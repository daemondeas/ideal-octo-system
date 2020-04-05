using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day12
    {
        public static void TwentythirdPuzzle()
        {
            var moons = new[]
            {
                new Moon(-7, -8, 9),
                new Moon(-12, -3, -4),
                new Moon(6, -17, -9),
                new Moon(4, -10, -6)
            };

            ApplyTimeSteps(moons, 1000);
            Console.WriteLine(moons.Sum(m => m.Energy));
        }

        public static void TwentyfourthPuzzle()
        {
            var moons = new[]
            {
                new Moon(-7, -8, 9),
                new Moon(-12, -3, -4),
                new Moon(6, -17, -9),
                new Moon(4, -10, -6)
            };

            Console.WriteLine(GetTimeStepsBeforeRepeat(moons));
        }

        private static ulong GetTimeStepsBeforeRepeat(Moon[] moons)
        {
            var count = 0UL;
            var xPeriod = 0UL;
            var yPeriod = 0UL;
            var zPeriod = 0UL;
            while (xPeriod == 0 || yPeriod == 0 || zPeriod == 0)
            {
                ++count;
                ApplyTime(moons);
                if (xPeriod == 0 && moons.All(m => m.IsAtStartX))
                {
                    xPeriod = count;
                }

                if (yPeriod == 0 && moons.All(m => m.IsAtStartY))
                {
                    yPeriod = count;
                }

                if (zPeriod == 0 && moons.All(m => m.IsAtStartZ))
                {
                    zPeriod = count;
                }
            } // 1227140100044031

            return LeastCommonMultiple(xPeriod, LeastCommonMultiple(yPeriod, zPeriod));
        }

        private static void ApplyTimeSteps(Moon[] moons, int n)
        {
            for (var i = 0; i < n; i++)
            {
                ApplyTime(moons);
            }
        }

        private static void ApplyTime(Moon[] moons)
        {
            for (var i = 0; i < moons.Length - 1; i++)
            {
                for (var j = i + 1; j < moons.Length; j++)
                {
                    ApplyGravity(moons[i], moons[j]);
                }
            }

            foreach (var moon in moons)
            {
                moon.ApplyVelocity();
            }
        }

        private static void ApplyGravity(Moon a, Moon b)
        {
            var (ax, bx) = GetGravityChanges(a.Position.X, b.Position.X);
            var (ay, by) = GetGravityChanges(a.Position.Y, b.Position.Y);
            var (az, bz) = GetGravityChanges(a.Position.Z, b.Position.Z);

            a.ChangeVelocity(ax, ay, az);
            b.ChangeVelocity(bx, by, bz);
        }

        private static (int, int) GetGravityChanges(int a, int b)
        {
            if (a == b)
            {
                return (0, 0);
            }

            return a < b ? (1, -1) : (-1, 1);
        }

        private static ulong LeastCommonMultiple(ulong a, ulong b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }

        private static ulong GreatestCommonDivisor(ulong a, ulong b)
        {
            return b == 0
                ? a
                : GreatestCommonDivisor(b, a % b);
        }
    }
}
