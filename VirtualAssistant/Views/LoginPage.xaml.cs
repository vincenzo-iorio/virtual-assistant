namespace VirtualAssistant.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Login", "Login simulato completato!", "OK");
        await Navigation.PushAsync(new ChatPage());
    }
}