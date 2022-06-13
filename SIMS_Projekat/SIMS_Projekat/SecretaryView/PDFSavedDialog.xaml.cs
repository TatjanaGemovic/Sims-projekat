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


        private void CloseLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
