using JaffaDiskont.Klase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for StanjeZaliha.xaml
    /// </summary>
    public partial class StanjeZaliha : Window
    {

        public static DataView CachedCSVData { get; set; }

        public StanjeZaliha()
        {

            InitializeComponent();

            if (CachedCSVData != null)
            {
                DataGridCSV.ItemsSource = CachedCSVData;
            }
        }

        private void PrijemRobe(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Csv Files|*.csv";

            if (openFile.ShowDialog() == true)
            {
                var csvData = CSVimport.getCSVData(openFile.FileName);

                StanjeZaliha.CachedCSVData = csvData; 
                DataGridCSV.ItemsSource = csvData;
            }
            else
            {
                MessageBox.Show("Nije odabrana nijedna datoteka.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void PovratakNaGlavniMeni(object sender, RoutedEventArgs e)
        {
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainMenu mainMenu)
                {
                    mainMenu.Show();  
                    break;
                }
            }

            this.Close();  
        }


    }
}
