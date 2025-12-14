using Galeria.Models;

namespace Galeria.Data;

public static class TicketRepository
{
    private static readonly List<ParkingTicket> Tickets = new()
    {
        new ParkingTicket
        {
            Code = "135781354",
            Entry = new DateTime(2021, 1, 1, 20, 0, 0),
            Exit  = new DateTime(2021, 1, 2, 1, 0, 0),
            Price = 6m,
            Paid  = true
        },
        new ParkingTicket
        {
            Code = "135781355",
            Entry = new DateTime(2021, 1, 1, 12, 0, 0),
            Exit  = new DateTime(2021, 1, 1, 15, 0, 0),
            Price = 6m,
            Paid  = true
        }
    };

    public static ParkingTicket CreateFromCode(string code)
    {
        // Simulação: cria um ticket a partir do código digitado / escaneado
        return new ParkingTicket
        {
            Code = code,
            Entry = DateTime.Now.AddHours(-3),
            Exit = DateTime.Now,
            Price = 6m,
            Paid = false
        };
    }

    public static IEnumerable<ParkingTicket> GetPaidTickets() =>
        Tickets.Where(t => t.Paid);

    public static void AddOrUpdate(ParkingTicket ticket)
    {
        var existing = Tickets.FirstOrDefault(t => t.Code == ticket.Code);
        if (existing == null)
        {
            Tickets.Add(ticket);
        }
        else
        {
            existing.Entry = ticket.Entry;
            existing.Exit = ticket.Exit;
            existing.Price = ticket.Price;
            existing.Paid = ticket.Paid;
        }
    }
}
