using System;
using System.Windows;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerCreateReport.xaml
    /// </summary>
    public partial class ManagerCreateReport : Page
    {
        public ManagerCreateReport()
        {
            InitializeComponent();
            this.DataContext = this;
            odTxt.SelectedDate = DateTime.Now;
            doTxt.SelectedDate = DateTime.Now;
        }

        private void Zavrsi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
