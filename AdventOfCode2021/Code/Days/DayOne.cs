using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2021.Code.Days
{
    internal class DayOne : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_one_puzzle_input.txt");

            ExecutePartOne(puzzleInput);
            ExecutePartTwo(puzzleInput);
        }

        private void ExecutePartOne(string[] input)
        {
            int numberOfIncreases = 0;
            int previousDepth = Int32.Parse(input[0]);

            for(int i = 1; i < input.Length; i++)
            {
                int depth = Int32.Parse(input[i]);
                
                if (depth > previousDepth)
                {
                    numberOfIncreases++;
                }
                previousDepth = depth;
            }

            Answer("Part One", numberOfIncreases);
        }

        private void ExecutePartTwo(string[] input)
        {
            int range = 3;
            int numberOfIncreases = 0;
            int? previousSumOfThree = null;

            for (int i = 0; i+range <= input.Length; i++)
            {
                int sumOfThree = input[i..(i+range)].Sum(Int32.Parse);                
                
                if (previousSumOfThree != null && sumOfThree > previousSumOfThree)
                {
                    numberOfIncreases++;
                }
                previousSumOfThree = sumOfThree;
            }

            Answer("Part Two", numberOfIncreases);
        }        
    }
}
