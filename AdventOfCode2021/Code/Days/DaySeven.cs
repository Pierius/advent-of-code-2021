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

            ExecutePartOneAndTwo(puzzleInput);            
        }

        public void ExecutePartOneAndTwo(string[] puzzleInput)
        {
            List<int> positions = puzzleInput[0].Split(',').Select(Int32.Parse).ToList();

            int highest = positions.Max();
            int lowest = positions.Min();

            List<(int position, (int one, int two) cost)> positionCosts = new List<(int position, (int one, int two) cost)>();

            for (; lowest <= highest; lowest++)
            {
                int costOne = 0;
                int costTwo = 0;

                for (int i = 0; i < positions.Count; i++)
                {
                    int steps = Math.Abs(positions[i] - lowest);
                    int sum = 0;

                    for (int j = 1; j <= steps; j++)
                    {
                        sum += j;
                    }

                    costOne += steps;
                    costTwo += sum;
                }

                positionCosts.Add((lowest, (costOne, costTwo)));
            }
                       
            var resultOne = positionCosts.MinBy(x => x.cost.one);
            var resultTwo = positionCosts.MinBy(x => x.cost.two);

            Answer("Part One", $"position:{resultOne.position}, cost:{resultOne.cost.one}");
            Answer("Part Two", $"position:{resultTwo.position}, cost:{resultTwo.cost.two}");
        }
    }
}
