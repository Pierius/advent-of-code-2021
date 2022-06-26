using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DaySix : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_six_puzzle_input.txt");

            string[] sampleInput = new string[]
            {
                "3,4,3,1,2"
            };
        
            ExecutePartOne(puzzleInput, 80);
            ExecutePartTwo(puzzleInput, 256);
        }
               
        private async void ExecutePartOne(string[] puzzleInput, int numberOfDays)
        {
            List<int> fishAges = puzzleInput[0].Split(',').Select(Int32.Parse).ToList();
            Dictionary<int, long> ageCache = new Dictionary<int, long>();

            foreach (int fishAge in fishAges)
            {
                if (!ageCache.ContainsKey(fishAge))
                {
                    ageCache.Add(fishAge, 0);
                }
            }

            List<Task<(int age, long population)>> tasks = new List<Task<(int age, long population)>>();

            foreach (int fishAge in ageCache.Keys)
            {
                tasks.Add(Task.Run(() => CalculateFishPopulationAsync(fishAge, numberOfDays)));
            }
            var results = await Task.WhenAll(tasks);

            foreach (var result in results)
            {
                if (ageCache.ContainsKey(result.age))
                {
                    ageCache[result.age] = result.population;
                }                
            }

            long sum = 0;

            foreach (int fishAge in fishAges)
            {
                if (ageCache.ContainsKey(fishAge))
                {
                    sum += ageCache[fishAge];
                }
            }

            Answer("Part One", sum);
        }

        private void ExecutePartTwo(string[] puzzleInput, int numberOfDays)
        {
            List<int> fishAges = puzzleInput[0].Split(',').Select(Int32.Parse).ToList();
            Dictionary<int, long> ageCache = new Dictionary<int, long>();
            long population = 0;

            foreach (int fishAge in fishAges)
            { 
                if (!ageCache.ContainsKey(fishAge))
                {
                    ageCache.Add(fishAge, 0);
                }
            }

            foreach (int fishAge in ageCache.Keys)
            {
                ageCache[fishAge] = CalculatePopulation(numberOfDays, fishAge) + 1;
            }

            foreach (int fishAge in fishAges)
            {
                if (ageCache.ContainsKey(fishAge))
                {
                    population += ageCache[fishAge];
                }
            }

            Answer("Part Two", population);
        }

        public long CalculatePopulation(int reproductionDays, int fishAge)
        {
            int ageNewFish = 8;

            int remainingDays = reproductionDays - (fishAge + 1);

            if (remainingDays < 0)
            {
                return 0;
            }

            long numberOfChildren = (remainingDays / 7) + 1;
            long childrensChildren = 0;

            for(int i = 0; i < numberOfChildren; i++)
            {
                childrensChildren += CalculatePopulation(remainingDays - (7 * i), ageNewFish);
            }

            return numberOfChildren + childrensChildren;            
        }

        public async Task<(int age, long population)> CalculateFishPopulationAsync(int age, int days)
        {
            List<int> fishes = new List<int>() { age };

            for (int day = 1; day <= days; day++)
            {
                List<int> newFish = new List<int>();

                for (int i = 0; i < fishes.Count; i++)
                {
                    if (fishes[i] == 0)
                    {
                        newFish.Add(8);
                        fishes[i] = 6;
                        continue;
                    }

                    --fishes[i];
                }

                fishes.AddRange(newFish);
            }

            return (age, fishes.Count);
        }        
    }
}
