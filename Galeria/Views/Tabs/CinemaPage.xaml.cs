using System.Collections.ObjectModel;
using Galeria.Data;
using Galeria.Models;

namespace Galeria.Views.Tabs;

public partial class CinemaPage : ContentPage
{
    public ObservableCollection<Movie> Movies { get; } = new();

    public CinemaPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = this;

        foreach (var m in MallRepository.GetMovies())
            Movies.Add(m);
    }

    // Clique no card do filme -> abre a tela de detalhes
    private async void OnMovieTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Movie movie)
        {
            await Navigation.PushAsync(new Views.Cinema.MovieDetailPage(movie));
        }
    }
}
