using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _game = new ClassicMode();
            _game.CreateNewGame();
            this.DataContext = _game;
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            _game = new QuantumMode();
            _game.CreateNewGame();
            this.DataContext = _game;
        }

        private void keyUpEventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                _game.MoveUp();
            }
            if (e.Key == Key.Down)
            {
                _game.MoveDown();
            }
            if (e.Key == Key.Left)
            {
                _game.MoveLeft();
            }
            if (e.Key == Key.Right)
            {
                _game.MoveRight();
            }
        }
    }
}
