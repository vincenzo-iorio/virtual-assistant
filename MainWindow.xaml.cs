using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using VirtualAssistant;

namespace VirtualAssistant
{
    public partial class MainWindow : Window
    {
        private Border _typingBubble;

        public MainWindow()
        {
            InitializeComponent();

            // Seed welcome message
            AddMessage("Hi Vincenzo! I’m your local assistant. Ask me anything and I’ll show a placeholder answer.", fromUser: false);
        }

        private async void AskButton_Click(object sender, RoutedEventArgs e)
        {
            string question = QuestionBox.Text.Trim();
            if (string.IsNullOrEmpty(question)) return;

            AddMessage(question, fromUser: true);
            QuestionBox.Clear();

            ShowTypingIndicator();
            await Task.Delay(600);
            HideTypingIndicator();

            AddMessage($"(Mock) Here's a placeholder answer to: \"{question}\".\nYou can wire this to your backend later.", fromUser: false);
            ScrollToEnd();
        }

        private void QuestionBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                e.Handled = true;
                AskButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void ShowTypingIndicator()
        {
            if (_typingBubble != null) return;

            var dots = new TextBlock
            {
                Text = "Typing…",
                Foreground = (Brush)new BrushConverter().ConvertFromString("#666666"),
                FontSize = 12
            };

            _typingBubble = MakeBubble(dots, fromUser: false);
            ChatPanel.Children.Add(_typingBubble);
            ScrollToEnd();
        }

        private void HideTypingIndicator()
        {
            if (_typingBubble == null) return;
            ChatPanel.Children.Remove(_typingBubble);
            _typingBubble = null;
        }

        private void AddMessage(string text, bool fromUser)
        {
            var tb = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Foreground = fromUser ? Brushes.White : (Brush)new BrushConverter().ConvertFromString("#2B2B2B"),
                FontSize = 14
            };

            var bubble = MakeBubble(tb, fromUser);
            ChatPanel.Children.Add(bubble);
            ScrollToEnd();
        }

        private Border MakeBubble(UIElement content, bool fromUser)
        {
            var accent = (Color)ColorConverter.ConvertFromString("#0078D4");
            var botBg = (Color)ColorConverter.ConvertFromString("#F3F6FB");
            var botBorder = (Color)ColorConverter.ConvertFromString("#E1E6F0");

            return new Border
            {
                Background = new SolidColorBrush(fromUser ? accent : botBg),
                BorderBrush = fromUser ? null : new SolidColorBrush(botBorder),
                BorderThickness = fromUser ? new Thickness(0) : new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(fromUser ? 48 : 0, 6, fromUser ? 0 : 48, 6),
                HorizontalAlignment = fromUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                Effect = new DropShadowEffect
                {
                    Color = (Color)ColorConverter.ConvertFromString("#22000000"),
                    BlurRadius = 8,
                    ShadowDepth = 2,
                    Opacity = 0.6
                },
                Child = content
            };
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow
            {
                Owner = this
            };

            if (loginWindow.ShowDialog() == true)
            {
                MessageBox.Show($"Welcome, {loginWindow.Username}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                // You can now store loginWindow.Username or update UI
            }
        }


        private void ScrollToEnd()
        {
            // Ensures the newest bubble is visible
            if (ChatPanel.Parent is ScrollViewer sc)
            {
                sc.ScrollToEnd();
            }
        }
    }
}