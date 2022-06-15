using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DayThree : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_three_puzzle_input.txt");

            ExecutePartOne(puzzleInput);
            ExecutePartTwo(puzzleInput);
        }

        private void ExecutePartOne(string[] puzzleInput)
        {
            int[] zeroCounter = new int[12];
            int[] oneCounter = new int[12];

            for (int i = 0; i < puzzleInput.Length; i++)
            {
                char[] charArray = puzzleInput[i].ToArray();
                for (int x = 0; x < charArray.Length; x++)
                {
                    zeroCounter[x] += charArray[x].Equals('0') ? 1 : 0;
                    oneCounter[x] += charArray[x].Equals('1') ? 1 : 0;
                }
            }

            StringBuilder binaryGammaRate = new StringBuilder();
            StringBuilder binaryEpsilonRate = new StringBuilder();

            for (int i = 0; i < zeroCounter.Length; i++)
            {
                bool zeroIsMostCommon = zeroCounter[i] > oneCounter[i];

                binaryGammaRate.Append(zeroIsMostCommon ? 0 : 1);
                binaryEpsilonRate.Append(zeroIsMostCommon ? 1 : 0);
            }

            int gammaRate = Convert.ToInt32(binaryGammaRate.ToString(), 2);
            int epsilonRate = Convert.ToInt32(binaryEpsilonRate.ToString(), 2);

            Answer("Part One", CalculatePowerRate(gammaRate, epsilonRate));
        }

        private int CalculatePowerRate(int gammaRate, int epsilonRate)
        {
            return gammaRate * epsilonRate;
        }

        private enum Rating 
        { 
            Co2Scrubber = 0,
            Oxygen = 1
        }


        private void ExecutePartTwo(string[] puzzleInput)
        {
            string oxygenNumber = FindRating(puzzleInput, Rating.Oxygen);
            string co2ScrubberNumber = FindRating(puzzleInput, Rating.Co2Scrubber);

            int oxygenRating = Convert.ToInt32(oxygenNumber, 2);
            int co2ScrubberRating = Convert.ToInt32(co2ScrubberNumber, 2);

            Answer("Part Two", CalculateLifeSupportRating(oxygenRating, co2ScrubberRating));
        }

        private string FindRating(string[] numbers, Rating rating, int position = 0)
        {
            if (numbers.Length == 1 || position >= numbers[0].Length)
            {
                return numbers[0];
            }

            (int zero, int one) counter = (0, 0);

            foreach (string number in numbers)
            {
                if (number[position].Equals('1'))
                {
                    counter.one++;
                }
                else
                {
                    counter.zero++;
                }
            }

            char bit = '1';
            switch (rating)
            {
                case Rating.Oxygen:
                    bit = counter.one >= counter.zero ? '1' : '0';
                    break;
                case Rating.Co2Scrubber:
                    bit = counter.one >= counter.zero ? '0' : '1';
                    break;
            }

            return FindRating(
                numbers: numbers.Where(n => n[position].Equals(bit)).ToArray(), 
                rating: rating,
                position: position + 1
            );
        }

        private int CalculateLifeSupportRating(int oxygenRating, int co2ScrubberRating)
        {
            return oxygenRating * co2ScrubberRating;
        }
    }
}
