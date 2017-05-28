using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace pb069_project_2048game.Model
{
    public class QuantumMode : IGame
    {
        public int Score { get; set; }

        public QuantumTile[][] Board { get; set; }

        public QuantumMode()
        {
            Initialize();
        }
        
        public void MoveLeft()
        {
            for (var row = 0; row < 4; row++)
            {
                Board[row] = MoveRow(Board[row]);
            }
            AddRandomTile();
        }

        public void MoveRight()
        {
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < 4; row++)
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
            for (var row = 0; row < 4; row++)
            {
                Board[row] = MoveRow(Board[row]);
            }
            TransposeAndRotateBoard(Board);
            AddRandomTile();
        }

        public void MoveDown()
        {
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < 4; row++)
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
            Initialize();
            Score = 0;
        }

        public bool CheckWin()
        {
            return Score == 2048;
        }

        #region Helper methods

        private void Initialize()
        {
            Board = new QuantumTile[4][];

            for (var i = 0; i < 4; i++)
            {
                Board[i] = new[] { new QuantumTile(), new QuantumTile(), new QuantumTile(), new QuantumTile() };
            }

            AddRandomTile();
            AddRandomTile();
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
        }


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

        private QuantumTile[] MergeRowResults(QuantumTile[][] subResults)
        {
            var mergedResults = new[] { new QuantumTile(), new QuantumTile(), new QuantumTile(), new QuantumTile() };

            for (var i = 0; i < subResults.GetLength(0); i++) 
            {
                foreach (var t in subResults) {
                    mergedResults[i].TileSet.UnionWith(t[i].TileSet);
                }
            }
            return mergedResults;
        }

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

        private void CopyToBoard(QuantumTile[][] retVal)
        {
            Board = retVal.Select(x => x.ToArray()).ToArray();
        }

        #endregion

    }
}
