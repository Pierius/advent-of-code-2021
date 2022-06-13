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

        private void ExecutePartOne(string[] puzzleInput)
        {
            int numberOfIncreases = 0;
            int previousDepth = Int32.Parse(puzzleInput[0]);

            for(int i = 1; i < puzzleInput.Length; i++)
            {
                int depth = Int32.Parse(puzzleInput[i]);
                
                if (depth > previousDepth)
                {
                    numberOfIncreases++;
                }
                previousDepth = depth;
            }

            Answer("Part One", numberOfIncreases);
        }

        private void ExecutePartTwo(string[] puzzleInput)
        {
            int range = 3;
            int numberOfIncreases = 0;
            int? previousSum = null;

            for (int i = 0; i+range <= puzzleInput.Length; i++)
            {
                int sum = puzzleInput[i..(i+range)].Sum(Int32.Parse);                
                
                if (previousSum != null && sum > previousSum)
                {
                    numberOfIncreases++;
                }
                previousSum = sum;
            }

            Answer("Part Two", numberOfIncreases);
        }        
    }
}
