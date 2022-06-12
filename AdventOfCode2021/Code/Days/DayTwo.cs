using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DayTwo : AbstractDay
    {
        private string _path = null;

        public override void Execute()
        {
            string filename = "day_two_puzzle_input.txt";
            _path = Path.Combine(Environment.CurrentDirectory, @"Resources\", filename);

            if (!File.Exists(_path))
            {
                Console.WriteLine($"Resource file is missing: {_path}");
                return;
            }

            ExecutePartOne();
        }

        private void ExecutePartOne()
        {
            int distance = 0;
            int dept = 0;

            foreach(string line in File.ReadAllLines(_path))
            {
                KeyValuePair<string, int> movement = GetMovement(line);

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

        private KeyValuePair<string, int> GetMovement(string line)
        {
            string[] movement = line.Split(" ");

            string direction = movement[0];
            int value = Convert.ToInt32(movement[1]);

            return new KeyValuePair<string, int>(direction, value);
        }

    }
}
