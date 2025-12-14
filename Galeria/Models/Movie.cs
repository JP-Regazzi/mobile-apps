namespace Galeria.Models;

public class Movie
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Poster { get; set; }
    public string Banner { get; set; }
    public TimeSpan Duration { get; set; }
    public string Synopsis { get; set; }

    public string DurationText => $"{(int)Duration.TotalHours}h {Duration.Minutes}m";
}
