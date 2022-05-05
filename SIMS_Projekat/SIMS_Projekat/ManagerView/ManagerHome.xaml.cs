using SIMS_Projekat.Controller;
using SIMS_Projekat.ManagerView;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        public Thread th1;
        public Thread th2;

        public ManagerHome()
        {
            InitializeComponent();
            this.DataContext = this;
            mainFrame = MainFrame;

            th1 = new Thread(new ThreadStart(App.exchangeEquipmentRequestController.ThreadFunction));
            th1.IsBackground = true;
            th1.Start();

            th2 = new Thread(new ThreadStart(App.renovationRequestController.ThreadFunction));
            th2.IsBackground = true;
            th2.Start();

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

        private void oprema_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new EquipmentView();
        }

        private void oprema_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new EquipmentView();
        }
    }
}
