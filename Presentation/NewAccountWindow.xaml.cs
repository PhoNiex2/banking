using System;
using System.Collections.Generic;
using System.Windows;
using banking.Business;
using banking.Models;

namespace banking.Presentation
{
    public partial class NewAccountWindow : Window
    {
        private readonly MainWindow mainWindow;
        private readonly AccountService accountService = new AccountService();

        public NewAccountWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            txtSortCode.Text = "101010";
            txtAccountNumber.Text = accountService.GenerateAccountNumber().ToString();

            cmbCounty.ItemsSource = new List<string>
            {
                "Antrim","Armagh","Carlow","Cavan","Clare","Cork","Derry","Donegal","Down",
                "Dublin","Fermanagh","Galway","Kerry","Kildare","Kilkenny","Laois","Leitrim",
                "Limerick","Longford","Louth","Mayo","Meath","Monaghan","Offaly","Roscommon",
                "Sligo","Tipperary","Tyrone","Waterford","Westmeath","Wexford","Wicklow"
            };
        }

        private void AccountType_Checked(object sender, RoutedEventArgs e)
        {
            if (rbSavings.IsChecked == true)
            {
                txtOverdraft.Text = "0";
                txtOverdraft.IsEnabled = false;
            }
            else
            {
                txtOverdraft.IsEnabled = true;
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateNewAccountForm())
                    return;

                string accountType = rbCurrent.IsChecked == true ? "Current" : "Savings";

                Account account = new Account
                {
                    FirstName = txtFirstName.Text.Trim(),
                    Surname = txtSurname.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    AddressLine1 = txtAddress1.Text.Trim(),
                    AddressLine2 = txtAddress2.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    County = cmbCounty.Text.Trim(),
                    AccountType = accountType,
                    AccountNumber = int.Parse(txtAccountNumber.Text),
                    SortCode = int.Parse(txtSortCode.Text),
                    Balance = decimal.Parse(txtBalance.Text),
                    OverdraftLimit = decimal.Parse(txtOverdraft.Text)
                };

                accountService.AddAccount(account);

                MessageBox.Show("Account created successfully.");
                mainWindow.LoadAccounts();
                Close();
            }
            catch (Exception ex)
            {
                ShowValidationError(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private bool ValidateNewAccountForm()
        {
            txtValidationError.Visibility = Visibility.Collapsed;
            txtValidationError.Text = "";

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                ShowValidationError("First Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                ShowValidationError("Surname is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowValidationError("Email is required.");
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                ShowValidationError("Enter a valid email address.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                ShowValidationError("Phone is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress1.Text))
            {
                ShowValidationError("Address Line 1 is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                ShowValidationError("City is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbCounty.Text))
            {
                ShowValidationError("Please select a county.");
                return false;
            }

            if (rbCurrent.IsChecked != true && rbSavings.IsChecked != true)
            {
                ShowValidationError("Please select an account type.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                ShowValidationError("Account Number could not be generated.");
                return false;
            }

            if (!decimal.TryParse(txtBalance.Text, out decimal balance))
            {
                ShowValidationError("Initial Balance must be a valid number.");
                return false;
            }

            if (balance < 0)
            {
                ShowValidationError("Initial Balance cannot be negative.");
                return false;
            }

            if (!decimal.TryParse(txtOverdraft.Text, out decimal overdraft))
            {
                ShowValidationError("Overdraft Limit must be a valid number.");
                return false;
            }

            if (overdraft < 0)
            {
                ShowValidationError("Overdraft Limit cannot be negative.");
                return false;
            }

            if (rbSavings.IsChecked == true && overdraft != 0)
            {
                ShowValidationError("Savings accounts must have an overdraft limit of 0.");
                return false;
            }

            return true;
        }

        private void ShowValidationError(string message)
        {
            txtValidationError.Text = message;
            txtValidationError.Visibility = Visibility.Visible;
        }

        
    }
}