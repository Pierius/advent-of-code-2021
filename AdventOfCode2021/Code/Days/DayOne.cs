using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2021.Code.Days
{
    internal class DayOne
    {
        private string _path = null;

        public void Execute()
        {
            string filename = "day_one_puzzle_input.txt";
            _path = Path.Combine(Environment.CurrentDirectory, @"Resources\", filename);

            if (!File.Exists(_path))
            {
                Console.WriteLine($"Resource file is missing: {_path}");
                return;
            }

            ExecutePartOne();
            ExecutePartTwo();
        }

        private void ExecutePartOne()
        {
            int? previousDepth = null;
            int numberOfIncreases = 0;

            foreach (string line in File.ReadLines(_path))
            {
                if (!Int32.TryParse(line, out int depth))
                {
                    continue;
                }

                if (previousDepth != null && depth > previousDepth)
                {
                    numberOfIncreases++;
                }

                previousDepth = depth;
            }

            Answer("Part One", numberOfIncreases);
        }

        private void ExecutePartTwo()
        {
            int? sumOfThree = null;
            int? previousSumOfThree = null;
            int numberOfIncreases = 0;
            int[] slotTimes = new int[3] { 0, -1, -2 };
            int[] slotSums = new int[3] { 0, 0, 0 };

            foreach (string line in File.ReadLines(_path))
            {
                if (!Int32.TryParse(line, out int depth))
                {
                    continue;
                }

                for(int i = 0; i < 3; i++)
                {
                    if (slotTimes[i] >= 0)
                    {
                        slotSums[i] += depth;
                    }
                    slotTimes[i]++;

                    if (slotTimes[i] == 3)
                    {
                        sumOfThree = slotSums[i];
                        slotSums[i] = 0;
                        slotTimes[i] = 0;
                    }
                }

                if (sumOfThree != null && previousSumOfThree != null && sumOfThree > previousSumOfThree)
                {
                    numberOfIncreases++;
                }

                previousSumOfThree = sumOfThree;
            }

            Answer("Part Two", numberOfIncreases);
        }

        private void Answer(string part, int value)
        {
            Console.WriteLine($"The answer of {part} is {value}");
        }
    }
}
