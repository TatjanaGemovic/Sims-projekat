using SIMS_Projekat.Controller;
using SIMS_Projekat.ManagerView;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for ManagerHome.xaml
    /// </summary>
    public partial class ManagerHome : Window
    {
        public static Frame mainFrame { get; set; }
        

        public ManagerHome()
        {
            InitializeComponent();
            this.DataContext = this;
            mainFrame = MainFrame;

        }

        private void notifikacije_KeyUp(object sender, KeyEventArgs e)
        {


        }

        private void notifikacije_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void notifikacije_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void prostorije_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new RoomView();
            }
        }

        private void prostorije_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new RoomView();
        }


        private void tt(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new RoomView();
        }

        
    }
}
