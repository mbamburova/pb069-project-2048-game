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

        public bool IsWon { get; set; } = false;


        public ClassicMode()
        {
           Initialize(Board);
        }

        public void Initialize(int[][] board)
        {
            Board = new int[4][];
            for (var i = 0; i < 4; i++)  
            {
                Board[i] = new int[4] {0, 0, 0, 0};
            }

            AddRandomTile();
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            int randRow, randCol;

            do
            {
                randRow = new Random().Next(0, 3);
                randCol = new Random().Next(0, 3);
            } while (Board[randRow][randCol] != 0);
            Board[randRow][randCol] = 4 / new Random().Next(1, 3);
        }

        public int[] Move(IEnumerable<int> row) // move row to left
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

        public void TransposeAndRotateBoard(int[][] board)
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

        public void CopyToBoard(int[][] retVal)
        {
            // really deep copy?
            Board = retVal.Select(x => x.ToArray()).ToArray();
        }
        
        public void MoveLeft()
        {
            for (int row = 0; row < 4; row++)
            {
                Board[row] = Move(Board[row]);
            }
            AddRandomTile();
        }

        public void MoveRight()
        {

            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            for (int row = 0; row < 4; row++)
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
            for (int row = 0; row < 4; row++)
            {
                Board[row] = Move(Board[row]);
            }
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);
            TransposeAndRotateBoard(Board);

            AddRandomTile();
        }

        public bool CheckIfIsWon(int[][] board)
        {
            return !Board.Any(x => x.Any(y => y > 2048)) && Board.Any(x => x.Any(y => y == 2048));
        }

        public void CreateNewGame()
        {
            Initialize(Board);
            Score = 0;
        }
    }
}
