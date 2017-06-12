using System.Windows;

namespace _2048game
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(string message)
        {
            InitializeComponent();
            myLabel.Content = message;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
