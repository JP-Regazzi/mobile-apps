namespace Galeria.Models
{
    public class ParkingTicket
    {
        public string Code { get; set; }
        public DateTime Entry { get; set; }
        public DateTime Exit { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }

        public string DurationText => (Exit - Entry).ToString(@"h\h\ mm\m");
        public string EntryText => Entry.ToString("dd/MM/yyyy HH:mm'h'");
        public string ExitText => Exit.ToString("dd/MM/yyyy HH:mm'h'");
        public string PriceText => $"R$ {Price:0.00}";
    }
}
