using SQLite;
namespace Academia.MVVM.Models;

[Table("exercicios")]
public class Exercicio
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Data do exercício (salvamos apenas a parte da data)
    public DateTime Data { get; set; }

    // Tipo: ex. "Supino", "Corrida", "Agachamento"
    [MaxLength(60)]
    public string Tipo { get; set; } = string.Empty;

    // Repetições (ou duração, se for cardio)
    public int Repeticoes { get; set; }

    // Carga (kg) — pode ser 0 para cardio
    public double Carga { get; set; }

    // Nome/Path da imagem (ex.: "halteres", "corrida", "yoga")
    [MaxLength(120)]
    public string Foto { get; set; } = "halteres";
}
