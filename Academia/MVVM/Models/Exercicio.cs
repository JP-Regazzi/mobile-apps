using SQLite;

namespace Academia.MVVM.Models;

[Table("exercicios")]
public class Exercicio
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Data { get; set; }

    [MaxLength(60)]
    public string Tipo { get; set; } = string.Empty;

    // Para exercícios de força
    public int Repeticoes { get; set; }

    // Para exercícios de força (kg). Em cardio fica 0.
    public double Carga { get; set; }

    // Para exercícios de cardio (minutos). Em força fica 0.
    public int DuracaoMinutos { get; set; }

    [MaxLength(120)]
    public string Foto { get; set; } = "halteres";
}
