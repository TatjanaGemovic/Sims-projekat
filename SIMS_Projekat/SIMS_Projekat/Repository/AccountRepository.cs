using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class AccountRepository
    {
        public Account CreatePatientAccount(Model.Patient patient, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Account DeletePatientAccount(Model.Patient patient)
        {
            throw new NotImplementedException();
        }

        public Account EditPatientAccount(Model.Patient patient)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllPatientAccounts()
        {
            throw new NotImplementedException();
        }

        public Account GetPatientAccountByID(string patientID)
        {
            throw new NotImplementedException();
        }

        public Model.UrgentPatient CreateUrgentPatientAccount(Model.UrgentPatient urgentPatient)
        {
            throw new NotImplementedException();
        }

        public Model.UrgentPatient DeleteUrgentPatientAccount(Model.UrgentPatient urgentPatient)
        {
            throw new NotImplementedException();
        }

        public Model.UrgentPatient GetUrgentPatientAccountByID(string urgentPatientID)
        {
            throw new NotImplementedException();
        }

        public List<UrgentPatient> GetAllUrgentPatients()
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Account> account;



        public System.Collections.Generic.List<Account> Account
        {
            get
            {
                if (account == null)
                    account = new System.Collections.Generic.List<Account>();
                return account;
            }
            set
            {
                RemoveAllAccount();
                if (value != null)
                {
                    foreach (Account oAccount in value)
                        AddAccount(oAccount);
                }
            }
        }


        public void AddAccount(Account newAccount)
        {
            if (newAccount == null)
                return;
            if (this.account == null)
                this.account = new System.Collections.Generic.List<Account>();
            if (!this.account.Contains(newAccount))
                this.account.Add(newAccount);
        }


        public void RemoveAccount(Account oldAccount)
        {
            if (oldAccount == null)
                return;
            if (this.account != null)
                if (this.account.Contains(oldAccount))
                    this.account.Remove(oldAccount);
        }


        public void RemoveAllAccount()
        {
            if (account != null)
                account.Clear();
        }
        public System.Collections.Generic.List<UrgentPatient> urgentPatient;



        public System.Collections.Generic.List<UrgentPatient> UrgentPatient
        {
            get
            {
                if (urgentPatient == null)
                    urgentPatient = new System.Collections.Generic.List<UrgentPatient>();
                return urgentPatient;
            }
            set
            {
                RemoveAllUrgentPatient();
                if (value != null)
                {
                    foreach (Model.UrgentPatient oUrgentPatient in value)
                        AddUrgentPatient(oUrgentPatient);
                }
            }
        }


        public void AddUrgentPatient(Model.UrgentPatient newUrgentPatient)
        {
            if (newUrgentPatient == null)
                return;
            if (this.urgentPatient == null)
                this.urgentPatient = new System.Collections.Generic.List<UrgentPatient>();
            if (!this.urgentPatient.Contains(newUrgentPatient))
                this.urgentPatient.Add(newUrgentPatient);
        }


        public void RemoveUrgentPatient(Model.UrgentPatient oldUrgentPatient)
        {
            if (oldUrgentPatient == null)
                return;
            if (this.urgentPatient != null)
                if (this.urgentPatient.Contains(oldUrgentPatient))
                    this.urgentPatient.Remove(oldUrgentPatient);
        }


        public void RemoveAllUrgentPatient()
        {
            if (urgentPatient != null)
                urgentPatient.Clear();
        }

    }
}