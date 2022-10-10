using SIMS_Projekat.Controller;
using SIMS_Projekat.ManagerView;
using SIMS_Projekat.Model;
using SIMS_Projekat.Properties;
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

            th2 = new Thread(new ThreadStart(App.renovationRequestController.RunRenovation));
            th2.IsBackground = true;
            th2.Start();

        }



        private void notifikacije_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new NotificationView();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.CurrentLanguage == "sr-LATN")
            {
                if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                    "Odjavljivanje",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }

            }
            else
            {
                if (MessageBox.Show("Are you sure you want to log off?",
                        "Logging off",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }

            }
            
        }

        private void lekovi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new MedicineView();
        }

        private void settingsUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new SettingsView();
        }



        private void NotifikacijeEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new NotificationView();
            }
        }

        private void LekoviEnkter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new MedicineView();
            }
        }

        private void OpremaEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new EquipmentView();
            }
        }

        private void AnketeEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new PollsView();
            }
        }

        private void AnketeMouseUp(object sender, MouseButtonEventArgs e)
        {
          
                MainFrame.Content = new PollsView();
            
        }

        private void PodesavanjaEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new SettingsView();
            }
        }

        private void HelpEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new HelpView();
            }
        }

        private void HelpMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new HelpView();
        }

        private void ProfilMouseUP(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new ProfileView();
        }

        private void ProfilEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainFrame.Content = new ProfileView();
            }
        }

        private void ProfilMouseUP1(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new ProfileView();
        }

        private void HelpUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new HelpView();
        }

        private void LogOFfUp(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Enter)
            {
                if (Settings.Default.CurrentLanguage == "sr-LATN")
                {
                    if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                        "Odjavljivanje",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }

                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to log off?",
                            "Logging off",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }

                }

            }


        }
    }
}
