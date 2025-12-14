using Microsoft.Maui.Controls;

namespace AppSorteio;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
    }

    private async void OnSplashTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ParticipantsPage());
    }
}
