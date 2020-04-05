using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day8
    {
        public static void FifteenthPuzzle(string picture)
        {
            var parsedPicture = GetLayers(picture, 25, 6);
            var checkSum = CalculateChecksum(parsedPicture);
            Console.WriteLine(checkSum);
        }

        public static void SixteenthPuzzle(string picture)
        {
            var parsedPicture = GetLayers(picture, 25, 6);
            var decodedPicture = DecodePicure(parsedPicture, 25, 6);
            PrintPicture(decodedPicture);
        }

        private static int CalculateChecksum(List<int[]> picture)
        {
            var weightedLayers = picture.Select(l => (l.Count(p => p == 0), l))
                .OrderBy(wl => wl.Item1);
            return weightedLayers.First().l.Count(p => p == 1)
                * weightedLayers.First().l.Count(p => p == 2);
        }

        private static List<int[]> GetLayers(string picture, int width, int height)
        {
            var size = width * height;
            var layers = new List<int[]>(picture.Length / size);
            for (var i = 0; i < picture.Length; i += size)
            {
                layers.Add(GetLayer(picture.Substring(i, size)));
            }

            return layers;
        }

        private static int[] GetLayer(string layer)
        {
            return layer.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        }

        private static void PrintPicture(List<int[]> picture)
        {
            for (var i = 0; i < picture.Count(); i++)
            {
                for(var j = 0; j < picture[i].Length; j++)
                {
                    Console.Write(picture[i][j] == 0 ? ' ' : '#');
                }

                Console.Write('\n');
            }
        }

        private static List<int[]> DecodePicure(List<int[]> layers, int width, int height)
        {
            var picture = new List<int[]>(height);
            for (var i = 0; i < height; i++)
            {
                var row = new int[width];
                for (var j = 0; j < width; j++)
                {
                    row[j] = layers.Select(l => l[i * width + j]).First(p => p != 2);
                }

                picture.Add(row);
            }

            return picture;
        }
    }
}
