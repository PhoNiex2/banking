using System;
using System.Windows;
using banking.Business;

namespace banking.Presentation
{
    public partial class TransferWindow : Window
    {
        private readonly MainWindow mainWindow;
        private readonly AccountService accountService = new AccountService();
        private readonly int sourceAccountNumber;

        public TransferWindow(MainWindow mainWindow, int sourceAccountNumber)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.sourceAccountNumber = sourceAccountNumber;
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isExternal = chkExternal.IsChecked == true;
                int destinationAccount = 0;

                if (!isExternal)
                {
                    destinationAccount = int.Parse(txtDestinationAccount.Text);
                }

                decimal amount = decimal.Parse(txtAmount.Text);

                accountService.Transfer(sourceAccountNumber, destinationAccount, amount, isExternal);

                MessageBox.Show("Transfer successful.");
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

        private void chkExternal_Checked(object sender, RoutedEventArgs e)
        {
            bool isExternal = chkExternal.IsChecked == true;
            txtDestinationAccount.IsEnabled = !isExternal;

            if (isExternal)
            {
                txtDestinationAccount.Text = "";
            }
        }
    }
}