using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for UrgentPatientView.xaml
    /// </summary>
    public partial class UrgentPatientView : Window
    {
        public AccountRepository AccountRepository { get; set; }
        public AccountController AccountController { get; set; }

        private static DataGrid dataGrid;

        public static ObservableCollection<UrgentPatient> UrgentPatients
        {
            get;
            set;
        }

        public UrgentPatientView(AccountRepository accountRepository, AccountController accountController)
        {
            InitializeComponent();
            this.DataContext = this;
            AccountRepository = accountRepository;
            AccountController = accountController;
            dataGrid = dataGridUrgentPatients;
            UrgentPatients = new ObservableCollection<UrgentPatient>();
            foreach (UrgentPatient urgentPatient in AccountController.GetAllUrgentPatients())
            {
                UrgentPatients.Add(urgentPatient);
            }

        }

        public static void AddUrgentPatient(UrgentPatient newUrgentPatient)
        {
            UrgentPatients.Add(newUrgentPatient);
        }

        public static void DeleteUrgentPatient(UrgentPatient newUrgentPatient)
        {
            UrgentPatients.Remove(newUrgentPatient);
        }

        public static void Refresh()
        {
            dataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUrgentPatient addUrgentPatient = new(AccountController);
            addUrgentPatient.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UrgentPatient urgentPatient = (UrgentPatient)dataGridUrgentPatients.SelectedItem;
            DeleteUrgentPatient(urgentPatient);
            AccountController.DeleteUrgentPatientAccount(urgentPatient);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UrgentPatient urgentPatient = (UrgentPatient)dataGridUrgentPatients.SelectedItem;
            EditUrgentPatient editUrgentPatient = new(AccountController, urgentPatient);
            editUrgentPatient.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UrgentPatient urgentPatient = (UrgentPatient)dataGridUrgentPatients.SelectedItem;
            ViewUrgentPatient viewUrgentPatient = new(AccountController, urgentPatient);
            viewUrgentPatient.Show();
        }
    }
}
