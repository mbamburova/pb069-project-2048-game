using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pb069_project_2048game.Model
{
    public interface IGame
    {
        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void CreateNewGame();

        bool CheckWin();

    }
}
