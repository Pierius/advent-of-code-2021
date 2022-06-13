using AdventOfCode2021.Code.Days;
using AdventOfCode2021.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal class Puzzle
    {
        public void Execute(Day day)
        {
            switch (day)
            {
                case Day.One:
                    new DayOne().Execute();
                    break;
                case Day.Two:
                    new DayTwo().Execute();
                    break;
            }
        }
    }
}
