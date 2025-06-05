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
using System.Data.SQLite;
using System.Data;


namespace JaffaDiskont.Windows
{
    /// <summary>
    /// Interaction logic for LoginUC.xaml
    /// </summary>
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
        }

        private void ClearTextOnFirstFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == KorisnickoIme && tb.Text == "Korisničko ime")
                tb.Text = "";
            

            tb.Foreground = Brushes.Black;
        }

        private void RestoreHintText(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == KorisnickoIme && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Korisničko ime";
                tb.Foreground = Brushes.Gray;
            }
           
        }

        private void ClearPasswordPlaceholder(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void RestorePasswordPlaceholder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Lozinka.Password))
                PasswordPlaceholder.Visibility = Visibility.Visible;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(Lozinka.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void UlogujSe(object sender, RoutedEventArgs e)
        {

            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=korisnici.db;Version=3;");

            if(sQLiteConnection.State == ConnectionState.Closed)
            {
                sQLiteConnection.Open();
            }

            try
            {

                String query = "SELECT count(1) FROM korisnici WHERE KorisnickoIme =@KorisnickoIme and Lozinka =@Lozinka";
                SQLiteCommand cmd= new SQLiteCommand(query, sQLiteConnection);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@KorisnickoIme", KorisnickoIme.Text);
                cmd.Parameters.AddWithValue("@Lozinka", Lozinka.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());


                if(count == 1)
                {
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();

                    var myWindow = Window.GetWindow(this);
                    myWindow.Close();
                }else
                {
                    MessageBox.Show("Korisničko ime ili lozinka nisu ispravni. Molimo pokušajte ponovo.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom povezivanja sa bazom podataka: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }finally
            {
                sQLiteConnection.Close();
            }

        }

        private bool isUserExist(string KorisnickoIme, string password)
        {
           foreach(User user in Global.Users)
            {
                if (user.Username == KorisnickoIme && user.Password == password)
                {
                    return true;
                }else
                    return false;
            }
            return false;
        }
    }
}

