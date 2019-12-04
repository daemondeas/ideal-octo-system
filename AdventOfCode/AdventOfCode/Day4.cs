using System;

namespace AdventOfCode
{
    public static class Day4
    {
        public static void SeventhPuzzle(string range)
        {
            var values = range.Split('-');
            var lower = int.Parse(values[0]);
            var upper = int.Parse(values[1]);
            var possiblePasswordsCount = 0;

            for (var i = lower; i <= upper; i++)
            {
                if (IsPossiblePassword(i.ToString())) {
                    ++possiblePasswordsCount;
                }
            }

            Console.WriteLine($"{possiblePasswordsCount} possible pws");
        }

        public static void EigthPuzzle(string range)
        {
            var values = range.Split('-');
            var lower = int.Parse(values[0]);
            var upper = int.Parse(values[1]);
            var possiblePasswordsCount = 0;

            for (var i = lower; i <= upper; i++)
            {
                if (IsPossibleExtraRestrictionPassword(i.ToString()))
                {
                    ++possiblePasswordsCount;
                }
            }

            Console.WriteLine($"{possiblePasswordsCount} possible pws");
        }

        private static bool IsPossiblePassword(string password)
        {
            if (password.Length != 6)
            {
                return false;
            }

            var passArray = password.ToCharArray();
            var foundDouble = false;

            for(var i = 0; i < 5; i++)
            {
                foundDouble |= passArray[i] == passArray[i + 1];

                if (passArray[i] > passArray[i + 1])
                {
                    return false;
                }
            }

            return foundDouble;
        }

        private static bool IsPossibleExtraRestrictionPassword(string password)
        {
            if (password.Length != 6)
            {
                return false;
            }

            var foundDouble = false;
            var passArray = password.ToCharArray();
            for (var i = 0; i < 5; i++)
            {
                if (passArray[i] > passArray[i + 1])
                {
                    return false;
                }

                if (passArray[i] == passArray[i + 1])
                {
                    if (i == 0)
                    {
                        foundDouble |= passArray[i] != passArray[i + 2];
                    }
                    else if (i == 4)
                    {
                        foundDouble |= passArray[i] != passArray[i - 1];
                    }
                    else
                    {
                        foundDouble |= passArray[i] != passArray[i - 1]
                            && passArray[i] != passArray[i + 2];
                    }
                }
            }

            return foundDouble;
        }
    }
}
