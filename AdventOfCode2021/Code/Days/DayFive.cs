using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DayFive : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_five_puzzle_input.txt");

            ExecutePartOne(puzzleInput);
        }

        private void ExecutePartOne(string[] lines)
        {
            int[,] field = new int[999, 999];
            var paths = GetPaths(lines);

            DrawPaths(field, paths);
            int countOverlap = GetOverlapCount(field, 2);

            Answer("Part One", countOverlap);
        }

        private int GetOverlapCount(int[,] field, int minimumTimes)
        {
            int counter = 0;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] >= minimumTimes)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        private void DrawPaths(int[,] field, List<Path> paths)
        {
            foreach (var path in paths)
            {
                List<Coordinate> coordinates = path.GetPathCoordinates();

                foreach(var coordinate in coordinates)
                {
                    field[coordinate.X, coordinate.Y] += 1;                    
                }
            }
        }

        private List<Path> GetPaths(string[] lines)
        {
            var paths = new List<Path>();

            foreach(string line in lines)
            {
                string regex = @"[->,\s]+";
                string[] points = Regex.Split(line, regex);
                paths.Add(new Path(
                    startX: Int32.Parse(points[0]),
                    startY: Int32.Parse(points[1]),
                    endX: Int32.Parse(points[2]),
                    endY: Int32.Parse(points[3])
                ));
            }

            return paths;
        }

        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private class Path
        {
            public Coordinate StartPoint { get; private set; }
            public Coordinate EndPoint { get; private set; }
            public Path(int startX, int startY, int endX, int endY)
            {
                StartPoint = new Coordinate(startX, startY);
                EndPoint = new Coordinate(endX, endY);
            }
            public List<Coordinate> GetPathCoordinates()
            {
                List<Coordinate> coordinates = new List<Coordinate>();

                if (StartPoint.X == EndPoint.X)
                {
                    // vertical line
                    int y = EndPoint.Y - StartPoint.Y;
                    for (int i = (y > 0 ? StartPoint.Y : EndPoint.Y); i <= (y > 0 ? EndPoint.Y : StartPoint.Y); i++)
                    {
                        coordinates.Add(new Coordinate(StartPoint.X, i));
                    }
                }
                else if (StartPoint.Y == EndPoint.Y)
                {
                    // horizontal line
                    int x = EndPoint.X - StartPoint.X;
                    for (int i = (x > 0 ? StartPoint.X : EndPoint.X); i <= (x > 0 ? EndPoint.X : StartPoint.X); i++)
                    {
                        coordinates.Add(new Coordinate(i, StartPoint.Y));
                    }
                }

                return coordinates;
            }
        }
    }
}
