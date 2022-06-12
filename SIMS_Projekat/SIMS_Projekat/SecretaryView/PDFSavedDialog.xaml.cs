using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Diagnostics;
using System.Windows;

namespace SIMS_Projekat.SecretaryView
{

    public partial class PDFSavedDialog : Window
    {
        public PDFSavedDialog()
        {
            InitializeComponent();
        }

        private void SaveLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(@".\..\..\..\Resources\pdf_files\Izveštaj o zauzetosti prostorija.pdf") { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show("Fajl se ne moze otvoriti");
            }
            Close();
        }

        private void CloseLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
