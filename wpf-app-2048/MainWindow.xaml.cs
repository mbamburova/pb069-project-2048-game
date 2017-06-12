using System.Windows;
using System.Windows.Input;
using _2048game._2048Game;

namespace _2048game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGame _game;

        public MainWindow()
        {
            InitializeComponent();
            QuantumMode.FocusVisualStyle = null;
            ClassicMode.FocusVisualStyle = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _game = new ClassicMode();
            _game.CreateNewGame();
            DataContext = _game;
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            _game = new QuantumMode();
            _game.CreateNewGame();
            DataContext = _game;
        }

        private void keyUpEventHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
                case Key.Up:
                    _game.MoveUp();
                    break;
                case Key.Down:
                    _game.MoveDown();
                    break;
                case Key.Left:
                    _game.MoveLeft();
                    break;
                case Key.Right:
                    _game.MoveRight();
                    break;
            }
            CheckWin();
        }

        private void CheckWin()
        {
            if (!_game.CheckWin()) return;
            var form = new Window1("You win! :)");
            form.Show();
        }
    }
}
