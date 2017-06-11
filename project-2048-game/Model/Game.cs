using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pb069_project_2048game.Model
{
    public abstract class Game
    {
        public abstract void MoveLeft();

        public abstract void MoveRight();

        public abstract void MoveUp();

        public abstract void MoveDown();

        public abstract void CreateNewGame();

        public abstract bool CheckWin();

    }
}
