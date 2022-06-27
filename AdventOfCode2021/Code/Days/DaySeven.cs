using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DaySeven : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_seven_puzzle_input.txt");

            string[] sampleInput = new string[]
            {
                "16,1,2,0,4,2,7,1,2,14"
            };

            ExecutePartOne(puzzleInput);
            ExecutePartTwo(sampleInput);
        }

        public void ExecutePartOne(string[] puzzleInput)
        {
            List<int> positions = puzzleInput[0].Split(',').Select(Int32.Parse).ToList();

            int highest = positions.Max();
            int lowest = positions.Min();

            List<(int position, int cost)> positionCosts = new List<(int position, int cost)>();

            for (; lowest <= highest; lowest++)
            {
                int cost = 0;

                for (int i = 0; i < positions.Count; i++)
                {
                    cost += Math.Abs(positions[i] - lowest);
                }

                positionCosts.Add((lowest, cost));
            }
                       
            var result = positionCosts.MinBy(x => x.cost);

            Answer("Part One", $"position:{result.position}, cost:{result.cost}");
        }

        public void ExecutePartTwo(string[] puzzleInput)
        {
            
        }
    }
}
