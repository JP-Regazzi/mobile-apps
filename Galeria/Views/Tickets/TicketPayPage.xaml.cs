using Galeria.Data;
using Galeria.Models;

namespace Galeria.Views.Tickets;

public partial class TicketPayPage : ContentPage
{
    private readonly ParkingTicket _ticket;

    public TicketPayPage(ParkingTicket ticket)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _ticket = ticket;
        BindingContext = _ticket;
    }

    private async void OnCopyAndPayClicked(object sender, EventArgs e)
    {
        // Mock: marca como pago e grava no "repositório"
        _ticket.Paid = true;
        TicketRepository.AddOrUpdate(_ticket);

        await Navigation.PushAsync(new TicketResultPage(_ticket));
    }
}
