using System;
using System.Windows;
using System.Windows.Input;

namespace JaffaDiskont.Windows
{
    public partial class LoginWindow : Window
    {
        private bool isFullscreen = true; // Starts in fullscreen

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Minimize_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Fullscreen_Click(object sender, MouseButtonEventArgs e)
        {
            if (isFullscreen)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowState = WindowState.Normal;
                isFullscreen = false;
            }
            else
            {
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
                isFullscreen = true;
            }
        }

        private void ShowLogin(object sender, RoutedEventArgs e)
        {
            TabContentPresenter.Content = new LoginUC();
        }

        private void ShowRegister(object sender, RoutedEventArgs e)
        {
            TabContentPresenter.Content = new RegisterUC();
        }



        private void BtnOtkazi(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Unesite svoje korisničko ime, lozinku i ID zaposlenog.\nAko nemate nalog, registrujte se koristeći drugi tab.",
                            "Pomoć", MessageBoxButton.OK, MessageBoxImage.Information);
        }



    }
}
