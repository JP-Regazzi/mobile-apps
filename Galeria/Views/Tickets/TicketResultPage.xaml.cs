using Galeria.Models;

namespace Galeria.Views.Tickets;

public partial class TicketResultPage : ContentPage
{
    public TicketResultPage(ParkingTicket ticket)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = ticket;
    }
}
