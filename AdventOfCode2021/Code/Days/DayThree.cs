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
    }
}
