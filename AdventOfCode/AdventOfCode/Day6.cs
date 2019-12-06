using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day6
    {
        public static void EleventhPuzzle(string[] orbits)
        {
            var map = GetOrbitMap(orbits);
            var length = CalculateChecksum(map);

            Console.WriteLine(length);
        }

        public static void TwelthPuzzle(string[] orbits)
        {
            var map = GetOrbitMap(orbits);
            var length = GetShortestPath("YOU", "SAN", map);

            Console.WriteLine(length);
        }

        private static Dictionary<string, string> GetOrbitMap(string[] orbits)
        {
            var result = new Dictionary<string, string>();
            foreach(var orbit in orbits)
            {
                var objects = orbit.Split(')');
                result[objects[1]] = objects[0];
            }

            return result;
        }

        private static int CalculateChecksum(Dictionary<string, string> map)
        {
            var length = 0;
            foreach(var obj in map.Keys)
            {
                length += GetOrbitChainLength(obj, map);
            }

            return length;
        }

        private static int GetOrbitChainLength(string obj, Dictionary<string, string> map)
        {
            return obj == "COM"
                ? 0
                : 1 + GetOrbitChainLength(map[obj], map);
        }

        private static int GetShortestPath(string a, string b, Dictionary<string, string> map)
        {
            var aDistances = GetObjectDistances(a, map);
            var bDistances = GetObjectDistances(b, map);
            var commonObjects = aDistances.Keys.Intersect(bDistances.Keys);
            return commonObjects.Min(c => aDistances[c] + bDistances[c]);
        }

        private static Dictionary<string, int> GetObjectDistances(
            string source,
            Dictionary<string, string> map)
        {
            var result = new Dictionary<string, int>();
            var i = 0;
            var nextObject = map[source];
            while(nextObject != "COM")
            {
                result.Add(nextObject, i++);
                nextObject = map[nextObject];
            }

            result.Add(nextObject, i);
            return result;
        }
    }
}
