using System.Collections.ObjectModel;
using Galeria.Data;
using Galeria.Models;

namespace Galeria.Views.Tabs;

public partial class StoresPage : ContentPage
{
    private readonly List<Store> _allStores;
    public ObservableCollection<Store> Stores { get; } = new();

    public StoresPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _allStores = MallRepository.GetStoresByType(StoreType.Store).ToList();

        BindingContext = this;
        LoadStores(_allStores);
    }

    private void LoadStores(IEnumerable<Store> stores)
    {
        Stores.Clear();
        foreach (var s in stores)
            Stores.Add(s);
    }

    private void OnSearchBarTextChanged(object sender, EventArgs e)
    {
        var text = StoreSearchBar.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(text))
        {
            LoadStores(_allStores);
        }
        else
        {
            var filtered = _allStores
                .Where(s => s.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
            LoadStores(filtered);
        }
    }

    // Novo: clique no card abre a StoreDetailPage
    private async void OnStoreTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Store store)
        {
            await Navigation.PushAsync(new Views.Stores.StoreDetailPage(store));
        }
    }
}
