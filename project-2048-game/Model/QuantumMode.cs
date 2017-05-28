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
            Initialize(Board);
        }

        public void Initialize(QuantumTile[][] board)
        {
            Board = new QuantumTile[4][];

            for (var i = 0; i < 4; i++)
            {
                Board[i] = new QuantumTile[4];
            }

            AddRandomTile();
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            int randomRow;
            int randomCol;
            do
            {
                randomRow = new Random().Next(0, 3);
                randomCol = new Random().Next(0, 3);
            } while (Board[randomRow][randomCol].TileSet.Count.Equals(0));

            var randomCount = new Random().Next(1, 3);
            for (var i = 0; i < randomCount; i++)
            {
                Board[randomRow][randomCol].TileSet.Add(4 / new Random().Next(1, 2));
            }
        }

        // vráti riadok po pohybe
        public QuantumTile[] MoveRow(QuantumTile[] row) // to the left
        {
            QuantumTile[][] subResults = new QuantumTile[3][];
            var matched = false;
            var countOfSubResults = 0;

            for (var i = 0; i < 3; i++)
            {
                subResults[i] = new QuantumTile[3];
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
            if (temp.TileSet.Count != 0 )
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

        public QuantumTile[] MergeRowResults(QuantumTile[][] subResults)
        {
           var mergedResults = new QuantumTile[4];
            
            for (var i = 0; i < subResults.GetLength(0); i++) // cez pocet subresults
            {
                for (var j = 0; j < subResults.GetUpperBound(1); j++) //3
                {
                    mergedResults[i].TileSet.UnionWith(subResults[j][i].TileSet);
                }
            }
            return mergedResults;
        }

        public QuantumTile GetMatchOfTiles(QuantumTile tile1, QuantumTile tile2)
        {
            var matchedTile = new QuantumTile();
           
            var tileArray = tile1.TileSet.ToArray();
            foreach (var t in tileArray) {
                if (tile2.TileSet.Contains(t))
                {
                    matchedTile.TileSet.Add(2*t);
                }
            }
            return matchedTile;
        }

        public void TransposeAndRotateBoard(QuantumTile[][] board)
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

        public void CopyToBoard(QuantumTile[][] retVal)
        {
            // really deep copy?
            Board = retVal.Select(x => x.ToArray()).ToArray();
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

    }
}
