using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pb069_project_2048game.Model
{
    public class ClassicMode : IGame
    {
        public int[][] Board { get; set; }

        public int Score { get; set; }
        
    
        public ClassicMode()
        {
           Initialize();
        }
       
        public void MoveLeft()
        {
            for (var row = 0; row < 4; row++)
            {
                Board[row] = Move(Board[row]);
            }
            AddRandomTile();
        }

        public void MoveRight()
        {

            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < 4; row++)
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
            for (var row = 0; row < 4; row++)
            {
                Board[row] = Move(Board[row]);
            }
            TransposeAndRotateBoard(Board);

            AddRandomTile();
        }

        public void MoveDown()
        {
            TransposeAndRotateBoard(Board);
            for (var row = 0; row < 4; row++)
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
            Board = new int[4][];
            for (var i = 0; i < 4; i++)
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

        private int[] Move(IEnumerable<int> row)
        {
            var newRow = new int[4];
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

        private void CopyToBoard(int[][] retVal)
        {
            Board = retVal.Select(x => x.ToArray()).ToArray();
        }

        #endregion
    }
}
