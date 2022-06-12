using AdventOfCode2021.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public class Menu
    {
        private const int MINIMUM_DAY = 1;
        private const int MAXIMUM_DAY = 25;

        public void OpenMenu()
        {
            string errorMessage = string.Empty;

            while (true)
            {
                Console.Clear();

                if (errorMessage != string.Empty)
                {
                    Console.WriteLine($"Error: {errorMessage}\n");
                    errorMessage = string.Empty;
                }

                Console.WriteLine("Welcome to Advent of Code!");
                Console.Write($"Choose your day ({MINIMUM_DAY}-{MAXIMUM_DAY}):");

                string? userInput = Console.ReadLine();

                if (userInput != null && 
                    (userInput.ToLower() == "q" || userInput.ToLower() == "quit"))
                {
                    break; // Quit program
                }

                Console.WriteLine(); // Line break                
                
                Day day = ConvertToDay(userInput);
                
                if (day == Day.Invalid)
                {
                    errorMessage = $"Only days between {MINIMUM_DAY} and {MAXIMUM_DAY} are valid!";
                    continue;
                }

                Console.WriteLine($"Executing Day: {day}");
                new Puzzle().Execute(day);
                
                Console.ReadKey();
            }
        }

        private Day ConvertToDay(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Int32.TryParse(value, out int day))
            {
                return Day.Invalid;
            }

            if (day < MINIMUM_DAY || day > MAXIMUM_DAY)
            {
                return Day.Invalid;
            }

            return (Day)day;
        }
    }
}
