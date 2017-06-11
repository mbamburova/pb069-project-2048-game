using System;
using System.Collections.Generic;
using System.Linq;

namespace pb069_project_2048game._2048Game
{
    public class ClassicMode : IGame
    {
        public int[][] Board { get; set; }

        public int Score { get; set; }

        private readonly int _rowLength;
    
        public ClassicMode()
        {
            _rowLength = 4;
           Initialize();
        }
       
        public void MoveLeft()
        {
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = Move(Board[row]);
            }
            AddRandomTile();
        }

        public void MoveRight()
        {

            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = Move(Board[row]);
            }
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);

            AddRandomTile();
        }

        public void MoveUp()
        {
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = Move(Board[row]);
            }
            TransposeAndRotateBoard(Board);

            AddRandomTile();
        }

        public void MoveDown()
        {
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = Move(Board[row]);
            }
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);

            AddRandomTile();
        }

        public bool CheckWin()
        {
            return Score == 2048;
        }

        public void CreateNewGame()
        {
            Initialize();
            Score = 0;
        }

        #region Helper methods 

        private void Initialize()
        {
            Board = new int[_rowLength][];
            for (var i = 0; i < _rowLength; i++)
            {
                Board[i] = new [] { 0, 0, 0, 0 };
            }

            AddRandomTile();
            AddRandomTile();
        }

        private void AddRandomTile()
        {
            var rand = new Random();
            int randRow, randCol;

            do
            {
                randRow = rand.Next(0, 3);
                randCol = rand.Next(0, 3);
            } while (Board[randRow][randCol] != 0);
            Board[randRow][randCol] = 4 / rand.Next(1, 3);
        }

        // moves selected row to the left
        private int[] Move(IEnumerable<int> row)
        {
            var newRow = new int[_rowLength];
            var j = 0;
            int? previous = null;

            foreach (var t in row)
            {
                if (t == 0) continue;
                if (previous == null)
                {
                    previous = t;
                }
                else
                {
                    if (previous == t)
                    {
                        var newValue = 2 * t;
                        Score += newValue;
                        newRow[j] = newValue;
                        j++;
                        previous = null;
                    }
                    else
                    {
                        newRow[j] = (int)previous;
                        j++;
                        previous = t;
                    }
                }
            }
            if (previous != null)
            {
                newRow[j] = (int)previous;
            }
            return newRow;
        }

        // transpose the matrix and rotate it by 90 degrees counter clock-wise
        private void TransposeAndRotateBoard(int[][] board)
        {
            var length = board[0].Length;
            var retVal = new int[length][];
            for (var x = 0; x < length; x++)
            {
                retVal[x] = board.Select(p => p[x]).ToArray();
                Array.Reverse(retVal[x]);
            }
            CopyToBoard(retVal);
        }

        // deep copy of matrix
        private void CopyToBoard(int[][] retVal)
        {
            Board = retVal.Select(x => x.ToArray()).ToArray();
        }

        #endregion
    }
}
