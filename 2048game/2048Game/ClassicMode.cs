using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048game._2048Game
{
    public class ClassicMode : ObservableObject, IGame
    {
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        private int[][] _board;
        public int[][] Board
        {
            get { return _board ?? (_board = Initialize()); }
            set
            {
                _board = value;
                Refresh();
            }
        }
        
        private string[][] _boardValues;
        public string[][] BoardValues
        {
            get
            {
                if (_boardValues == null) InitializeValues();
                return _boardValues;
            }
            set
            {
                _boardValues = value;
            }
        }

        private readonly int _rowLength;

        public ClassicMode()
        {
            _rowLength = 4;
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

        public void CreateNewGame()
        {
            AddRandomTile();
            AddRandomTile();
            Score = 0;
        }

        public bool CheckWin()
        {
            for (var row = 0; row < _rowLength; row++)
            {
                for (var col = 0; col < _rowLength; col++)
                {
                    if (Board[row][col] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckLoose()
        {
            for (var row = 0; row < _rowLength; row++)
            {
                for (var col = 0; col < _rowLength; col++)
                {
                    if (Board[row][col] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region Helper methods 

        private int[][] Initialize()
        {
            var board = new int[_rowLength][];
            for (var i = 0; i < _rowLength; i++)
            {
                board[i] = new[] { 0, 0, 0, 0 };
            }
            return board;
        }

        private void AddRandomTile()
        {
            var rand = new Random();
            int randRow, randCol;

            do
            {
                randRow = rand.Next(0, 4);
                randCol = rand.Next(0, 4);
            } while (Board[randRow][randCol] != 0);
            Board[randRow][randCol] = 4 / rand.Next(1, 3);
            Refresh();
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

        public void InitializeValues()
        {
            Score = 0;
            BoardValues = new string[_rowLength][];
            for (var i = 0; i < _rowLength; i++)
            {
                BoardValues[i] = new[] { "0", "0", "0", "0" };
            }
        }

        public void PrintClassicBoard()
        {
            for (var i = 0; i <= Board.GetUpperBound(0); i++)
            {
                for (var j = 0; j < Board.Length; j++)
                {
                    BoardValues[i][j] = "";
                    if (Board[i][j] != 0)
                    {
                        BoardValues[i][j] = $"{Board[i][j]}";
                    }
                }
            }
        }

        private void Refresh()
        {
            PrintClassicBoard();
            OnPropertyChanged("BoardValues");
        }
        #endregion
    }
}
