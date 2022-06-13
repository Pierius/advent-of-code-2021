using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DayTwo : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_two_puzzle_input.txt");
            
            ExecutePartOne(puzzleInput);
            ExecutePartTwo(puzzleInput);
        }

        private void ExecutePartOne(string[] input)
        {
            int distance = 0;
            int dept = 0;

            for (int i = 0; i < input.Length; i++)
            {
                KeyValuePair<string, int> movement = GetMovement(input[i]);

                switch (movement.Key)
                {
                    case "forward":
                        distance += movement.Value;
                        break;
                    case "up":
                        dept -= movement.Value;
                        break;
                    case "down":
                        dept += movement.Value;
                        break;
                }
            }

            Answer("Part One", distance * dept);
        }

        private void ExecutePartTwo(string[] input)
        {
            int distance = 0;
            int dept = 0;
            int aim = 0;

            for (int i = 0; i < input.Length; i++)
            {
                KeyValuePair<string, int> movement = GetMovement(input[i]);

                switch (movement.Key)
                {
                    case "forward":
                        distance += movement.Value;
                        dept += movement.Value * aim;
                        break;
                    case "up":
                        aim -= movement.Value;
                        break;
                    case "down":
                        aim += movement.Value;
                        break;
                }                
            }

            Answer("Part Two", distance * dept);
        }

        private KeyValuePair<string, int> GetMovement(string line)
        {
            string[] movement = line.Split(" ");

            string direction = movement[0];
            int value = Convert.ToInt32(movement[1]);

            return new KeyValuePair<string, int>(direction, value);
        }
    }
}
