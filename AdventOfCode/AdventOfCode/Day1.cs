using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Day1
    {
        public static void FirstPuzzle(int[] moduleWeights)
        {
            var weightSum = moduleWeights.Sum(CalculateModuleFuelNeed);
            Console.WriteLine(weightSum);
            Console.ReadLine();
        }

        public static void SecondPuzzle(int[] moduleWeights)
        {
            var weightSum = moduleWeights.Sum(CalculateModuleFuelIncludingFuelWeightNeed);
            Console.WriteLine(weightSum);
            Console.ReadLine();
        }

        private static int CalculateModuleFuelNeed(int moduleWeight)
        {
            return moduleWeight / 3 - 2;
        }

        private static int CalculateModuleFuelIncludingFuelWeightNeed(int moduleWeight)
        {
            var fuel = CalculateModuleFuelNeed(moduleWeight);
            var moreFuel = fuel;
            while ((moreFuel = CalculateModuleFuelNeed(moreFuel)) > 0)
            {
                fuel += moreFuel;
            }

            return fuel;
        }
    }
}
