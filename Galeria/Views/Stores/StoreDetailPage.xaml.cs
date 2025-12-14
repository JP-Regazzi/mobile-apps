using Galeria.Models;

namespace Galeria.Views.Stores;

public partial class StoreDetailPage : ContentPage
{
    public StoreDetailPage(Store store)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = store;
    }

    private async void OnBackTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
