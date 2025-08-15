using System.Collections.ObjectModel;

namespace VirtualAssistant.Views;

public partial class ChatPage : ContentPage
{
    public ObservableCollection<Message> Messages { get; set; } = new();

    public ChatPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OnSendClicked(object sender, EventArgs e)
    {
        var text = MessageEntry.Text?.Trim();
        if (!string.IsNullOrEmpty(text))
        {
            Messages.Add(new Message { Text = $"Tu: {text}" });
            Messages.Add(new Message { Text = $"Assistente: Hai detto '{text}'" });
            MessageEntry.Text = string.Empty;
        }
    }
}

public class Message
{
    public string Text { get; set; }
}