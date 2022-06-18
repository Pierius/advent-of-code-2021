using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code.Days
{
    internal class DayFour : AbstractDay
    {
        public override void Execute()
        {
            string[] puzzleInput = GetPuzzleInput("day_four_puzzle_input.txt");

            ExecutePartOne(puzzleInput);
            ExecutePartTwo(puzzleInput);
        }

        private void ExecutePartOne(string[] puzzleInput)
        {
            List<int> drawnNumbers = GetDrawnNumbers(puzzleInput[0]);
            List<BingoBoard> bingoBoards = GetBingoBoards(puzzleInput);
            List<BingoBoard> winners = new List<BingoBoard>();

            foreach(int number in drawnNumbers)
            {
                foreach(BingoBoard board in bingoBoards)
                {
                    board.MarkNumber(number);
                    if (board.IsWinner())
                    {
                        winners.Add(board);
                    }
                }
                if (winners.Count > 0)
                {
                    break;
                }
            }
            
            DrawWinningBingoBoards(winners);
        }

        private void ExecutePartTwo(string[] puzzleInput)
        {
            List<int> drawnNumbers = GetDrawnNumbers(puzzleInput[0]);
            List<BingoBoard> bingoBoards = GetBingoBoards(puzzleInput);
            BingoBoard lastWinner = null;

            foreach (int number in drawnNumbers)
            {
                foreach (BingoBoard board in bingoBoards)
                {
                    if (board.IsWinner())
                    {
                        continue;
                    }
                        
                    board.MarkNumber(number);
                    if (board.IsWinner())
                    {
                        lastWinner = board;
                    }
                }                
            }

            lastWinner.DrawBoard();
        }

        private void DrawWinningBingoBoards(List<BingoBoard> winners)
        {
            foreach(BingoBoard board in winners)
            {                
                board.DrawBoard();
            }
        }

        private List<int> GetDrawnNumbers(string numbers)
        {
            List<int> drawnNumbers = new List<int>();

            foreach(string number in numbers.Split(','))
            {
                drawnNumbers.Add(Int32.Parse(number));
            }

            return drawnNumbers;
        }

        private List<BingoBoard> GetBingoBoards(string[] boardNumbers)
        {
            List<BingoBoard> boards = new List<BingoBoard>();
            List<string> horizontalLines = new List<string>();
            int boardId = 0;

            for (int i = 1; i < boardNumbers.Length; i++)
            {
                if (horizontalLines.Count == 5)
                {
                    boards.Add(new BingoBoard(boardId, horizontalLines));                    
                }

                if (boardNumbers[i].Length == 0)
                {                    
                    horizontalLines.Clear();
                    boardId = i;
                    continue;
                }

                horizontalLines.Add(boardNumbers[i]);
            }

            return boards;
        }

        private class BingoBoard
        {
            public int Id { get; private set; }
            public int LastNumber { get; private set; }
            
            private List<List<int>> _lines = new List<List<int>>();
            private List<string> _originLines = new List<string>();
            private int[,] _board2d = new int[5, 5];

            public BingoBoard(int id, List<string> horizontalLines)
            {
                Id = id;
                Build(horizontalLines);
            }

            private void Build(List<string> horizontalLines)
            {
                if (horizontalLines.Count != 5)
                {
                    Console.WriteLine("Error: a Bingo board needs to exists out of 5 horizontal lines!");
                    return;
                }

                for(int i = 0; i < horizontalLines.Count; i++)
                {
                    List<int> numbers = horizontalLines[i]
                        .Split(separator: ' ', options: StringSplitOptions.RemoveEmptyEntries)
                        .Select(Int32.Parse)
                        .ToList();

                    for (int v = 0; v < numbers.Count; v++)
                    {
                        _board2d[i, v] = numbers[v];
                    }

                    _lines.Add(numbers);
                    _originLines.Add(horizontalLines[i]);
                }

                BuildVerticalLines();
            }

            private void BuildVerticalLines()
            {
                for (int v = 0; v < _board2d.GetLength(1); v++)
                {
                    List<int> verticalLine = new List<int>();

                    for (int h = 0; h < _board2d.GetLength(0); h++)
                    {
                        verticalLine.Add(_board2d[h, v]);
                    }

                    _lines.Add(verticalLine);
                }
            }            

            public void MarkNumber(int number)
            {
                LastNumber = number;

                foreach(List<int> line in _lines)
                {
                    if (line.Contains(number))
                    {
                        line.Remove(number);
                    }
                }
            }

            public bool IsWinner()
            {
                return _lines.Where(o => o.Count == 0).Any();
            }

            public int CalculateScore()
            {
                if (!IsWinner())
                {
                    return 0;
                }

                List<int> distinctNumbers = new List<int>();
                foreach(var line in _lines)
                {
                    foreach(var number in line)
                    {
                        if (!distinctNumbers.Contains(number))
                        {
                            distinctNumbers.Add(number);
                        }
                    }
                }

                return distinctNumbers.Sum() * this.LastNumber;
            }

            public void DrawBoard()
            {
                Console.WriteLine($"Bingo board #{this.Id} has a score of {CalculateScore()}");
                foreach(string line in _originLines)
                {
                    Console.WriteLine(line);
                }
            }
        }        
    }
}
