using Academia.MVVM.ViewModels;

namespace Academia.MVVM.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Loaded += async (_, __) =>
        {
            if (BindingContext is MainViewModel mvm && mvm.CarregarCommand.CanExecute(null))
                await mvm.CarregarCommand.ExecuteAsync(null);
        };
    }

    // Recarrega quando a página volta a aparecer (ex.: após excluir nos detalhes)
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel mvm && mvm.CarregarCommand.CanExecute(null))
            _ = mvm.CarregarCommand.ExecuteAsync(null);
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (BindingContext is MainViewModel mvm && mvm.TrocarDataCommand.CanExecute(e.NewDate))
            await mvm.TrocarDataCommand.ExecuteAsync(e.NewDate);
    }
}
