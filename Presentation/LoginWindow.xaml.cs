using System.Windows;
using banking.Business;

namespace banking.Presentation
{
    public partial class LoginWindow : Window
    {
        private readonly UserService userService = new UserService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            bool success = userService.Login(txtUsername.Text, txtPassword.Password);

            if (success)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                txtError.Text = "Invalid username or password.";
                txtError.Visibility = Visibility.Visible;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}