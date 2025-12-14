namespace Galeria.Models;

public enum StoreType
{
    Store,
    Restaurant
}

public class Store
{
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string FullDescription { get; set; }
    public string Logo { get; set; }
    public string Banner { get; set; }
    public string Location { get; set; }
    public string Phone { get; set; }
    public StoreType Type { get; set; }
}
