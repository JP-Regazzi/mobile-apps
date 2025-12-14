using System.Collections.ObjectModel;
using Galeria.Data;
using Galeria.Models;

namespace Galeria.Views.Tickets;

public partial class TicketListPage : ContentPage
{
    public ObservableCollection<ParkingTicket> Tickets { get; } = new();

    public TicketListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = this;
    }

    private void LoadTickets()
    {
        Tickets.Clear();
        foreach (var t in TicketRepository.GetPaidTickets())
            Tickets.Add(t);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadTickets();
    }
}
