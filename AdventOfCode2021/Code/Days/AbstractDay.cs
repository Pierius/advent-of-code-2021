using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal abstract class AbstractDay
    {
        public abstract void Execute();

        public string[] GetPuzzleInput(string filename)
        {
            string path;

            path = Path.Combine(Environment.CurrentDirectory, @"Resources\", filename);

            if (!File.Exists(path))
            {
                Console.WriteLine($"Resource file is missing: {path}");
                return Array.Empty<string>();
            }

            return File.ReadAllLines(path);
        }

        public void Answer(string part, int value)
        {
            Console.WriteLine($"The answer of {part} is {value}");
        }

        public void Answer(string part, long value)
        {
            Console.WriteLine($"The answer of {part} is {value}");
        }
    }
}
