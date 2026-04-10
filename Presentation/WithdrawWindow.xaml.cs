using System;
using System.Windows;
using banking.Business;

namespace banking.Presentation
{
    public partial class WithdrawWindow : Window
    {
        private readonly MainWindow mainWindow;
        private readonly AccountService accountService = new AccountService();
        private readonly int accountNumber;

        public WithdrawWindow(MainWindow mainWindow, int accountNumber)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.accountNumber = accountNumber;
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                accountService.Withdraw(accountNumber, amount);
                MessageBox.Show("Withdrawal successful.");
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