using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace JaffaDiskont.Windows
{
    /// <summary>
    /// Interaction logic for RegisterUC.xaml
    /// </summary>
    public partial class RegisterUC : UserControl
    {
        public RegisterUC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void BtnRegistrujSe(object sender, RoutedEventArgs e)
        {
            bool isKorisnickoImeValid = !string.IsNullOrWhiteSpace(KorisnickoIme.Text) && KorisnickoIme.Text != "Korisničko ime";
            bool isLozinkaValid = !string.IsNullOrWhiteSpace(Lozinka.Password);
            bool isIDzaposlenogValid = !string.IsNullOrWhiteSpace(IDzaposlenog.Text) && IDzaposlenog.Text != "ID zaposlenog";

            if (!isKorisnickoImeValid || !isLozinkaValid || !isIDzaposlenogValid)
            {
                MessageBox.Show("Molimo popunite sva polja pravilno.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    // Pravi User objekat i čuva ga u listu Global.Users (tvoja logika)
                    User user = new User(KorisnickoIme.Text, Lozinka.Password);
                    Global.Users.Add(user);

                    // Kreira konekciju prema bazi (putanja do baze neka bude ista kao u loginu)
                    string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "korisnici.db");
                    using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                    {
                        connection.Open();

                        // SQL komanda za upis korisnika u bazu
                        string query = "INSERT INTO korisnici (KorisnickoIme, Lozinka) VALUES (@KorisnickoIme, @Lozinka)";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@KorisnickoIme", KorisnickoIme.Text);
                            cmd.Parameters.AddWithValue("@Lozinka", Lozinka.Password);
                            cmd.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    MessageBox.Show("Registracija uspješna!", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Po želji resetuj polja
                    KorisnickoIme.Text = "Korisničko ime";
                    Lozinka.Password = "";
                    IDzaposlenog.Text = "ID zaposlenog";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška prilikom upisa u bazu: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ClearTextOnFirstFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == KorisnickoIme && tb.Text == "Korisničko ime")
                tb.Text = "";
            else if (tb == IDzaposlenog && tb.Text == "ID zaposlenog")
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
            else if (tb == IDzaposlenog && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "ID zaposlenog";
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

    }
}