using System;
using System.Windows;
using banking.Business;
using banking.Models;

namespace banking.Presentation
{
    public partial class EditAccountWindow : Window
    {
        private readonly MainWindow mainWindow;
        private readonly AccountService accountService = new AccountService();
        private readonly int accountNumber;

        public EditAccountWindow(MainWindow mainWindow, int accountNumber)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.accountNumber = accountNumber;
            LoadAccount();
        }

        private void LoadAccount()
        {
            Account account = accountService.GetAccountByNumber(accountNumber);

            txtFirstName.Text = account.FirstName;
            txtSurname.Text = account.Surname;
            txtEmail.Text = account.Email;
            txtPhone.Text = account.Phone;
            txtAddress1.Text = account.AddressLine1;
            txtAddress2.Text = account.AddressLine2;
            txtCity.Text = account.City;
            txtCounty.Text = account.County;
            txtAccountType.Text = account.AccountType;
            txtAccountNumber.Text = account.AccountNumber.ToString();
            txtSortCode.Text = account.SortCode.ToString();
            txtBalance.Text = account.Balance.ToString("0.00");
            txtOverdraft.Text = account.OverdraftLimit.ToString("0.00");
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account account = new Account
                {
                    AccountNumber = int.Parse(txtAccountNumber.Text),
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    AddressLine1 = txtAddress1.Text,
                    AddressLine2 = txtAddress2.Text,
                    City = txtCity.Text,
                    County = txtCounty.Text,
                    OverdraftLimit = decimal.Parse(txtOverdraft.Text)
                };

                accountService.UpdateAccount(account);
                MessageBox.Show("Account updated successfully.");
                mainWindow.LoadAccounts();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}