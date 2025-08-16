using System.Windows;

namespace VirtualAssistantMock
{
    public partial class LoginWindow : Window
    {
        public string Username { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Simulated auth check
            if (UsernameBox.Text == "admin" && PasswordBox.Password == "1234")
            {
                Username = UsernameBox.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}