using System;
using System.Windows;
using banking.Business;

namespace banking.Presentation
{
    public partial class DepositWindow : Window
    {
        private readonly MainWindow mainWindow;
        private readonly AccountService accountService = new AccountService();
        private readonly int accountNumber;

        public DepositWindow(MainWindow mainWindow, int accountNumber)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.accountNumber = accountNumber;
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                accountService.Deposit(accountNumber, amount);
                MessageBox.Show("Deposit successful.");
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