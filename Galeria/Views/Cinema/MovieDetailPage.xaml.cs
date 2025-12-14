using Galeria.Models;

namespace Galeria.Views.Cinema;

public partial class MovieDetailPage : ContentPage
{
    public MovieDetailPage(Movie movie)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = movie;
    }

    private async void OnCloseTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnPlayTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Trailer", "Iniciando trailer (mock).", "OK");
    }

    private async void OnPauseTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Trailer", "Pausando trailer (mock).", "OK");
    }
}
