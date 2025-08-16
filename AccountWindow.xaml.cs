using Microsoft.Win32;
using System.Windows;

namespace VirtualAssistant
{
    public partial class AccountWindow : Window
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string UserAvatar { get; set; }

        public AccountWindow(string username, string email, string avatarPath)
        {
            InitializeComponent();
            Username = username;
            Email = email;
            UserAvatar = avatarPath;
            Bio = "Tell us something about yourself...";
            DataContext = this;
        }

        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg"
            };
            if (dialog.ShowDialog() == true)
            {
                UserAvatar = dialog.FileName;
                DataContext = null;
                DataContext = this;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Changes saved!", "Account", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}