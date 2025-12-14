using Galeria.Data;
using Galeria.Models;

namespace Galeria.Views.Tabs;

public partial class TicketScanPage : ContentPage
{
    public TicketScanPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnTicketCodeCompleted(object sender, EventArgs e)
    {
        var code = TicketCodeEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(code))
            return;

        var ticket = TicketRepository.CreateFromCode(code);
        await Navigation.PushAsync(new Views.Tickets.TicketPayPage(ticket));
    }

    private async void OnCameraClicked(object sender, EventArgs e)
    {
        // Mock: se o usuário não digitou, usamos um código padrão
        var code = string.IsNullOrWhiteSpace(TicketCodeEntry.Text)
            ? "135781354"
            : TicketCodeEntry.Text.Trim();

        var ticket = TicketRepository.CreateFromCode(code);
        await Navigation.PushAsync(new Views.Tickets.TicketPayPage(ticket));
    }

    private async void OnPaidTicketsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.Tickets.TicketListPage());
    }
}
