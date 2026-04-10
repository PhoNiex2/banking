using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using banking.Business;
using banking.Models;
using Microsoft.Win32;

namespace banking.Presentation
{
    public partial class MainWindow : Window
    {
        private readonly AccountService accountService = new AccountService();
        private List<Account> allAccounts = new List<Account>();

        public MainWindow()
        {
            InitializeComponent();
            LoadAccounts();
        }

        public void LoadAccounts()
        {
            allAccounts = accountService.GetAllAccounts();
            dgAccounts.ItemsSource = null;
            dgAccounts.ItemsSource = allAccounts;

            txtTotalAccounts.Text = allAccounts.Count.ToString();
            txtTotalBalance.Text = allAccounts.Sum(a => a.Balance).ToString("C");
        }

        private Account GetSelectedAccount()
        {
            return dgAccounts.SelectedItem as Account;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAccounts();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            var filteredAccounts = allAccounts.Where(a =>
                a.FirstName.ToLower().Contains(searchText) ||
                a.Surname.ToLower().Contains(searchText) ||
                a.AccountNumber.ToString().Contains(searchText) ||
                a.AccountType.ToLower().Contains(searchText)).ToList();

            dgAccounts.ItemsSource = null;
            dgAccounts.ItemsSource = filteredAccounts;

            txtTotalAccounts.Text = filteredAccounts.Count.ToString();
            txtTotalBalance.Text = filteredAccounts.Sum(a => a.Balance).ToString("C");
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }

        private void DeactivateAccount_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();

            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to deactivate this account?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                accountService.DeactivateAccount(selected.AccountNumber);
                MessageBox.Show("Account deactivated.");
                LoadAccounts();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccountWindow window = new NewAccountWindow(this);
            window.ShowDialog();
        }

        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();
            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            EditAccountWindow window = new EditAccountWindow(this, selected.AccountNumber);
            window.ShowDialog();
        }

        private void DepositFunds_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();
            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            DepositWindow window = new DepositWindow(this, selected.AccountNumber);
            window.ShowDialog();
        }

        private void WithdrawFunds_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();
            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            WithdrawWindow window = new WithdrawWindow(this, selected.AccountNumber);
            window.ShowDialog();
        }

        private void TransferFunds_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();
            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            TransferWindow window = new TransferWindow(this, selected.AccountNumber);
            window.ShowDialog();
        }

        private void ViewTransactions_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();
            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            TransactionsWindow window = new TransactionsWindow(selected.AccountNumber);
            window.ShowDialog();
        }

        private void ExportToXml_Click(object sender, RoutedEventArgs e)
        {
            Account selected = GetSelectedAccount();
            if (selected == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml";

            if (dialog.ShowDialog() == true)
            {
                accountService.ExportAccountToXml(selected, dialog.FileName);
                MessageBox.Show("Account exported to XML successfully.");
            }
        }
    }
}