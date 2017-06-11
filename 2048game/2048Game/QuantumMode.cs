using System;
using System.Linq;

namespace _2048game._2048Game
{
    public class QuantumMode : ObservableObject, IGame
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

        private QuantumTile[][] _board;
        public QuantumTile[][] Board
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

        public QuantumMode()
        {
            _rowLength = 4;
        }

        public void MoveLeft()
        {
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = MoveRow(Board[row]);
            }
            AddRandomTile();
        }

        public void MoveRight()
        {
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = MoveRow(Board[row]);
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
                Board[row] = MoveRow(Board[row]);
            }
            TransposeAndRotateBoard(Board);
            AddRandomTile();
        }

        public void MoveDown()
        {
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < _rowLength; row++)
            {
                Board[row] = MoveRow(Board[row]);
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
            return Score == 2048;
        }

        #region Helper methods

        private QuantumTile[][] Initialize()
        {
            var board = new QuantumTile[_rowLength][];

            for (var i = 0; i < _rowLength; i++)
            {
                board[i] = new[] { new QuantumTile(), new QuantumTile(), new QuantumTile(), new QuantumTile() };
            }

            //AddRandomTile();
            //AddRandomTile();
            return board;
        }

        private void AddRandomTile()
        {
            var rand = new Random();
            int randomRow;
            int randomCol;
            do
            {
                randomRow = rand.Next(0, 4);
                randomCol = rand.Next(0, 4);
            } while (!Board[randomRow][randomCol].TileSet.Count.Equals(0));

            var randomCount = new Random().Next(1, 8);
            for (var i = 0; i < randomCount; i++)
            {
                var randVal = 8 / rand.Next(1, 4);
                Board[randomRow][randomCol].TileSet.Add(randVal);
            }
            Refresh();
        }

        // the method moves selected row to the letf and returns resultant merged row 
        private QuantumTile[] MoveRow(QuantumTile[] row)
        {
            ShiftTiles(row);

            var subResults = new QuantumTile[3][];
            var matched = false;
            var countOfSubResults = 0;

            for (var i = 0; i < 3; i++)
            {
                subResults[i] = new[] { new QuantumTile(), new QuantumTile(), new QuantumTile() };
            }

            var temp = GetMatchOfTiles(row[0], row[1]);
            if (temp.TileSet.Count != 0)
            {
                matched = true;
                subResults[countOfSubResults][0] = temp;
                subResults[countOfSubResults][1] = row[2];
                subResults[countOfSubResults][2] = row[3];
            }

            temp = GetMatchOfTiles(row[1], row[2]);
            if (temp.TileSet.Count != 0)
            {
                matched = true;
                countOfSubResults++;
                subResults[countOfSubResults][0] = row[0];
                subResults[countOfSubResults][1] = temp;
                subResults[countOfSubResults][2] = row[3];
            }

            temp = GetMatchOfTiles(row[2], row[3]);
            if (temp.TileSet.Count != 0)
            {
                matched = true;
                countOfSubResults++;
                subResults[countOfSubResults][0] = row[0];
                subResults[countOfSubResults][1] = row[1];
                subResults[countOfSubResults][2] = temp;
            }
            return matched == false ? row : MergeRowResults(subResults);
        }

        // the method removes tiles with zero value and shift remainig in the selected row to the left
        private void ShiftTiles(QuantumTile[] row)
        {
            var temp = 0;
            while (temp != row.Length)
            {
                if (row[temp].TileSet.Count == 0)
                {
                    for (var j = temp; j < row.Length; j++)
                    {
                        if (row[j].TileSet.Count != 0)
                        {
                            row[temp] = row[j];
                            row[j] = new QuantumTile();
                            break;
                        }
                    }
                    temp++;
                }
                else
                {
                    temp++;
                }
            }
        }

        // unites row subresults and return new united row
        private QuantumTile[] MergeRowResults(QuantumTile[][] subResults)
        {
            var mergedResults = new[] { new QuantumTile(), new QuantumTile(), new QuantumTile(), new QuantumTile() };

            for (var i = 0; i < subResults.GetLength(0); i++)
            {
                foreach (var t in subResults)
                {
                    mergedResults[i].TileSet.UnionWith(t[i].TileSet);
                }
            }
            return mergedResults;
        }

        // return tile as a result of two adjacent tiles
        private QuantumTile GetMatchOfTiles(QuantumTile tile1, QuantumTile tile2)
        {
            var matchedTile = new QuantumTile();

            var tileArray = tile1.TileSet.ToArray();
            foreach (var t in tileArray)
            {
                if (tile2.TileSet.Contains(t))
                {
                    matchedTile.TileSet.Add(2 * t);
                    Score += 2 * t;
                }
            }
            return matchedTile;
        }

        // transpose board and then rotate it 90 degrees counter clock-wise
        private void TransposeAndRotateBoard(QuantumTile[][] board)
        {
            var length = board[0].Length;
            var retVal = new QuantumTile[length][];

            for (var x = 0; x < length; x++)
            {
                retVal[x] = board.Select(p => p[x]).ToArray();
                Array.Reverse(retVal[x]);
            }
            CopyToBoard(retVal);
        }

        // deep copy of matrix
        private void CopyToBoard(QuantumTile[][] retVal)
        {
            Board = retVal.Select(x => x.ToArray()).ToArray();
        }

        public void InitializeValues()
        {
            Score = 8;
            BoardValues = new string[_rowLength][];
            for (var i = 0; i < _rowLength; i++)
            {
                BoardValues[i] = new[] { "0", "0", "0", "0" };
            }
        }

        public void PrintQuantumBoard()
        {
            for (var i = 0; i <= Board.GetUpperBound(0); i++)
            {
                for (var j = 0; j < Board.Length; j++)
                {
                    BoardValues[i][j] = "";
                    foreach (var value in Board[i][j].TileSet)
                    {
                        BoardValues[i][j] += $"{value}  ";
                    }
                }

            }
        }

        private void Refresh()
        {
            PrintQuantumBoard();
            OnPropertyChanged("BoardValues");
        }
        #endregion
    }
}
