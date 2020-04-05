using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day14
    {
        public static void TwentyseventhPuzzle()
        {
            var (amountOfOre, _) = GetCostForChemical(ParseInput(input), "FUEL", "ORE");
            Console.WriteLine(amountOfOre);
        }

        public static void TwentyeighthPuzzle()
        {
            var maxProduce = GetMaxProduceForGivenAmount(ParseInput(input), "FUEL", "ORE", 1000000000000);
            Console.WriteLine(maxProduce);
        }

        private static long GetMaxProduceForGivenAmount(List<Chemical> prices, string chemical, string unit, long amount)
        {
            /*var (cost, leftOvers) = GetCostForChemical(prices, chemical, unit);
            var produce = amount / cost;
            var oreLeftOver = amount % cost;
            for (var i = 0; i < leftOvers.Count(); i++)
            {
                var element = leftOvers.ElementAt(i);
                leftOvers[element.Key] = element.Value * produce;
            }*/
            long cost;
            Dictionary<string, long> leftOvers = null;
            var oreLeftOver = amount;
            var produce = 0L;
            var increment = 10000000;
            while (increment >= 1)
            {
                (cost, leftOvers) = GetCostForChemical(prices, chemical, unit, leftOvers, increment);
                if (cost <= oreLeftOver)
                {
                    oreLeftOver -= cost;
                    produce += increment;
                }
                else
                {
                    increment /= 10;
                }
            }

            return produce;
        }

        private static (long, Dictionary<string, long>) GetCostForChemical(
            List<Chemical> prices,
            string chemical,
            string unit,
            Dictionary<string, long> previousLeftOvers = null,
            long times = 1)
        {
            prices.Add(new Chemical { Name = unit, Amount = 1 });
            var leftOvers = previousLeftOvers ?? new Dictionary<string, long>();
            var costCollection = new List<(string, long)>();
            costCollection.AddRange(prices.Single(p => p.Name == chemical)
                .Cost.Select(c => (c.Name, c.Amount * times)));

            while (costCollection.Any(c => c.Item1 != unit))
            {
                var itemToReplace = costCollection.First(c => c.Item1 != unit);
                costCollection.Remove(itemToReplace);
                var alreadyHave = leftOvers.TryGetValue(itemToReplace.Item1, out var v)
                        ? v
                        : 0;
                var needed = itemToReplace.Item2 - alreadyHave;
                if (needed <= 0)
                {
                    leftOvers[itemToReplace.Item1] = -needed;
                    continue;
                }

                var price = prices.Single(p => p.Name == itemToReplace.Item1);
                var factor = needed / price.Amount;
                if (needed % price.Amount != 0)
                {
                    ++factor;
                }

                var produced = price.Amount * factor;
                leftOvers[itemToReplace.Item1] = produced - needed;

                costCollection.AddRange(price.Cost.Select(c => (c.Name, c.Amount * factor)));
            }

            var result = 0L;
            foreach (var cost in costCollection)
            {
                result += cost.Item2;
            }

            return (result, leftOvers);
        }

        private static List<Chemical> ParseInput(string[] inp)
        {
            return inp.Select(Parse).ToList();
        }

        private static Chemical Parse(string inp)
        {
            var parts = inp.Split(" => ");
            var result = ParseSingle(parts[1]);
            var costs = parts[0].Split(", ");
            result.Cost = new List<Chemical>(costs.Length + 1);
            foreach (var cost in costs)
            {
                result.Cost.Add(ParseSingle(cost));
            }

            return result;
        }

        private static Chemical ParseSingle(string inp)
        {
            var parts = inp.Split(' ');
            return new Chemical
            {
                Amount = long.Parse(parts[0]),
                Name = parts[1]
            };
        }

        private static string[] input =
        {
            "11 RVCS => 8 CBMDT",
            "29 QXPB, 8 QRGRH => 8 LGMKD",
            "3 VPRVD => 6 PMFZG",
            "1 CNWNQ, 11 MJVXS => 6 SPLM",
            "13 SPDRZ, 13 PMFZG => 2 BLFM",
            "8 QWPFN => 7 LWVB",
            "1 SPLM => 8 TKWQ",
            "2 QRGRH, 6 CNWNQ => 7 DTZW",
            "2 DMLT, 1 SPLM, 1 TMDK => 9 NKNS",
            "1 MJVXS, 1 HLBV => 7 PQCQH",
            "1 JZHZP, 9 LWVB => 7 MJSCQ",
            "29 DGFR => 7 QRGRH",
            "14 XFLKQ, 2 NKNS, 4 KMNJF, 3 MLZGQ, 7 TKWQ, 24 WTDW, 11 CBMDT => 4 GJKX",
            "4 TKWQ, 1 WLCFR => 4 PDKGT",
            "2 NKNS => 4 GDKL",
            "4 WRZST => 9 XFLKQ",
            "19 DGFR => 4 VPRVD",
            "10 MJSCQ, 4 QWPFN, 4 QXPB => 2 MLZGQ",
            "1 JZHZP => 7 QWPFN",
            "1 XFLKQ => 9 FQGVL",
            "3 GQGXC => 9 VHGP",
            "3 NQZTV, 1 JZHZP => 2 NVZWL",
            "38 WLCFR, 15 GJKX, 44 LGMKD, 2 CBVXG, 2 GDKL, 77 FQGVL, 10 MKRCZ, 29 WJQD, 33 BWXGC, 19 PQCQH, 24 BKXD => 1 FUEL",
            "102 ORE => 5 DGFR",
            "17 NWKLB, 1 SBPLK => 5 HRQM",
            "3 BWXGC => 8 TQDP",
            "1 TQDP => 2 PSZDZ",
            "2 MJVXS => 9 WNXG",
            "2 NBTW, 1 HRQM => 2 SVHBH",
            "8 CNWNQ, 1 DTZW => 4 RVCS",
            "4 VHGP, 20 WNXG, 2 SVHBH => 3 SPDRZ",
            "110 ORE => 5 TXMC",
            "10 QRGRH => 5 NWKLB",
            "1 SBPLK => 3 MJVXS",
            "9 DGFR => 5 RFSRL",
            "5 LBTV => 3 DMLT",
            "1 NWKLB, 1 KMNJF, 1 HDQXB, 6 LBTV, 2 PSZDZ, 34 PMFZG, 2 SVHBH => 2 WJQD",
            "1 RVCS => 5 MKRCZ",
            "14 NQZTV, 3 FPLT, 1 SJMS => 2 GQGXC",
            "18 RFSRL, 13 VHGP, 23 NBTW => 5 WTDW",
            "1 VHGP, 6 TKWQ => 7 QXPB",
            "1 JZHZP, 1 CNWNQ => 5 KMNJF",
            "109 ORE => 9 BWXGC",
            "2 CNWNQ, 1 PDKGT, 2 KMNJF => 5 HDQXB",
            "1 PDKGT, 18 WRZST, 9 MJSCQ, 3 VHGP, 1 BLFM, 1 LGMKD, 7 WLCFR => 2 BKXD",
            "11 MLJK => 6 FPLT",
            "8 DGFR, 2 TXMC, 3 WJRC => 9 SJMS",
            "2 SBPLK => 1 LBTV",
            "22 QWPFN => 4 WRZST",
            "5 WRZST, 22 WNXG, 1 VHGP => 7 NBTW",
            "7 RVCS => 9 TMDK",
            "1 DGFR, 14 TXMC => 5 JZHZP",
            "2 JZHZP => 3 SBPLK",
            "19 PDKGT => 8 HLBV",
            "195 ORE => 6 WJRC",
            "6 GQGXC => 8 CNWNQ",
            "1 NVZWL, 4 GQGXC => 2 CBVXG",
            "1 NVZWL, 1 KMNJF => 8 WLCFR",
            "153 ORE => 4 MLJK",
            "1 BWXGC => 6 NQZTV"
        };

        private static string[] testInput1 =
        {
            "10 ORE => 10 A",
            "1 ORE => 1 B",
            "7 A, 1 B => 1 C",
            "7 A, 1 C => 1 D",
            "7 A, 1 D => 1 E",
            "7 A, 1 E => 1 FUEL"
        };
    }
}
