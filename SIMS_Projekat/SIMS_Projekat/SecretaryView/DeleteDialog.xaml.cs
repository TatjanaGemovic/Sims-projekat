using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace SIMS_Projekat.SecretaryView
{

    public partial class DeleteDialog : Window
    {
        ObservableCollection<Account> accounts;
        Account accountToRemove;
        public DeleteDialog(ObservableCollection<Account> accounts, Account account)
        {
            InitializeComponent();
            this.accounts = accounts;
            accountToRemove = account;
        }


        private void CloseLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }

        private void YesLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            accounts.Remove(accountToRemove);
            Close();
        }
    }
}
