using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using banking.Business;
using banking.Models;

namespace banking.Presentation
{
    public partial class TransactionsWindow : Window
    {
        private readonly TransactionService transactionService = new TransactionService();
        private readonly int accountNumber;
        private List<TransactionRecord> allTransactions = new List<TransactionRecord>();

        public TransactionsWindow(int accountNumber)
        {
            InitializeComponent();
            this.accountNumber = accountNumber;
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            allTransactions = transactionService.GetTransactionsByAccount(accountNumber);
            dgTransactions.ItemsSource = null;
            dgTransactions.ItemsSource = allTransactions;

            txtTransactionCount.Text = allTransactions.Count.ToString();
            txtTransactionTotal.Text = allTransactions.Sum(t => t.Amount).ToString("C");
        }

        private void txtSearchTransaction_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchTransaction.Text.ToLower();

            var filteredTransactions = allTransactions.Where(t =>
                t.TransactionType.ToLower().Contains(searchText) ||
                t.ReferenceNumber.ToLower().Contains(searchText) ||
                t.SourceAccountNumber.ToString().Contains(searchText) ||
                (t.DestinationAccountNumber.HasValue &&
                 t.DestinationAccountNumber.Value.ToString().Contains(searchText))
            ).ToList();

            dgTransactions.ItemsSource = null;
            dgTransactions.ItemsSource = filteredTransactions;

            txtTransactionCount.Text = filteredTransactions.Count.ToString();
            txtTransactionTotal.Text = filteredTransactions.Sum(t => t.Amount).ToString("C");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}