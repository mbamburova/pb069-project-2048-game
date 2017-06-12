namespace _2048game._2048Game
{
    public interface IGame
    {
        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void CreateNewGame();

        bool CheckWin();

        bool CheckLoose();

    }
}
