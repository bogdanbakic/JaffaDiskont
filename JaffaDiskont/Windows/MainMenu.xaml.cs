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
using System.Windows.Shapes;

namespace JaffaDiskont.Windows
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Ugasi(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void StanjeZaliha(object sender, RoutedEventArgs e)
        {
            StanjeZaliha stanjeZaliha = new StanjeZaliha();
            stanjeZaliha.Show();

            MainMenu mainMenu = new MainMenu();
            mainMenu.Hide();

        }
    }
}
