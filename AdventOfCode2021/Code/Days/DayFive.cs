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
        private enum Direction 
        {
            Horizontal,
            Vertical,
            Diagonal
        }

        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_five_puzzle_input.txt");

            ExecutePartOne(puzzleInput);
            ExecutePartTwo(puzzleInput);
        }

        private void ExecutePartOne(string[] lines)
        {
            int[,] field = new int[1000, 1000];
            var paths = GetPaths(lines);

            DrawPaths(field, paths, new List<Direction>() { Direction.Horizontal, Direction.Vertical });
            int countOverlap = GetOverlapCount(field, 2);

            Answer("Part One", countOverlap);
        }

        private void ExecutePartTwo(string[] lines)
        {
            int[,] field = new int[1000, 1000];
            var paths = GetPaths(lines);

            DrawPaths(field, paths, new List<Direction>() { Direction.Horizontal, Direction.Vertical, Direction.Diagonal });
            int countOverlap = GetOverlapCount(field, 2);

            Answer("Part Two", countOverlap);
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

        private void DrawPaths(int[,] field, List<Path> paths, List<Direction> allowedDirections)
        {
            foreach (var path in paths)
            {
                List<Coordinate> coordinates = path.GetPathCoordinates(allowedDirections);

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
            public List<Coordinate> GetPathCoordinates(List<Direction> directions)
            {
                List<Coordinate> coordinates = new List<Coordinate>()
                {                    
                    new Coordinate(EndPoint.X, EndPoint.Y)
                };

                if (StartPoint.X == EndPoint.X && directions.Contains(Direction.Vertical))
                {
                    for (int vert = StartPoint.Y; vert != EndPoint.Y;)
                    {
                        coordinates.Add(new Coordinate(StartPoint.X, vert));

                        vert = EndPoint.Y > StartPoint.Y ? vert + 1 : vert - 1;
                    }
                }
                else if (StartPoint.Y == EndPoint.Y && directions.Contains(Direction.Horizontal))
                {
                    for (int hor = StartPoint.X; hor != EndPoint.X;)
                    {
                        coordinates.Add(new Coordinate(hor, StartPoint.Y));
                        
                        hor = EndPoint.X > StartPoint.X ? hor + 1 : hor - 1;
                    }
                }
                else if (StartPoint.X != EndPoint.X && StartPoint.Y != EndPoint.Y && directions.Contains(Direction.Diagonal))
                {
                    for (int hor = StartPoint.X, vert = StartPoint.Y; hor != EndPoint.X && vert != EndPoint.Y;)
                    {
                        coordinates.Add(new Coordinate(hor, vert));
                        
                        hor = EndPoint.X > StartPoint.X ? hor + 1 : hor - 1;
                        vert = EndPoint.Y > StartPoint.Y ? vert + 1 : vert - 1;
                    }
                }
                else
                {
                    // Remove endpoint
                    coordinates.Clear();
                }

                return coordinates;
            }
        }
    }
}
