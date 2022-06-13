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

        public void Answer(string part, int value)
        {
            Console.WriteLine($"The answer of {part} is {value}");
        }
    }
}
