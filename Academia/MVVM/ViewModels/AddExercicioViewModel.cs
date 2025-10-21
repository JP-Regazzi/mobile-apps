using Academia.MVVM.Models;
using Academia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Academia.MVVM.ViewModels;

public class AddExercicioViewModel : ObservableObject
{
    private readonly IAcademiaDbService _db;

    // Tipos disponíveis
    public List<string> OpcoesTipo { get; } = new()
    {
        "Supino", "Remada", "Agachamento", "Flexão",
        "Corrida", "Bicicleta", "Yoga"
    };

    // Mapeia o Tipo (como aparece no Picker) para o nome da imagem (arquivo em Resources/Images)
    private static readonly Dictionary<string, string> TipoParaImagem = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Supino"] = "supino",
        ["Remada"] = "remada",
        ["Agachamento"] = "agachamento",
        ["Flexão"] = "flexao",
        ["Corrida"] = "corrida",
        ["Bicicleta"] = "bicicleta",
        ["Yoga"] = "yoga",
    };

    // Quais tipos NÃO têm carga
    private static readonly HashSet<string> TiposSemCarga = new(StringComparer.OrdinalIgnoreCase)
    {
        "Corrida", "Bicicleta", "Yoga"
    };

    private string _tipo = "Supino";
    public string Tipo
    {
        get => _tipo;
        set
        {
            if (SetProperty(ref _tipo, value))
            {
                AtualizarFotoECarga();
            }
        }
    }

    private int _repeticoes = 10;
    public int Repeticoes
    {
        get => _repeticoes;
        set => SetProperty(ref _repeticoes, value);
    }

    private double _carga;
    public double Carga
    {
        get => _carga;
        set => SetProperty(ref _carga, value);
    }

    private DateTime _data = DateTime.Today;
    public DateTime Data
    {
        get => _data;
        set => SetProperty(ref _data, value);
    }

    private string _foto = "supino";
    public string Foto
    {
        get => _foto;
        set => SetProperty(ref _foto, value);
    }

    // controla a UI (se mostra/edita Carga)
    private bool _cargaHabilitada = true;
    public bool CargaHabilitada
    {
        get => _cargaHabilitada;
        set => SetProperty(ref _cargaHabilitada, value);
    }

    public IAsyncRelayCommand SalvarCommand { get; }
    public IAsyncRelayCommand CancelarCommand { get; }

    public AddExercicioViewModel(IAcademiaDbService db)
    {
        _db = db;
        AtualizarFotoECarga(); // inicializa Foto/Carga conforme tipo padrão

        SalvarCommand = new AsyncRelayCommand(SalvarAsync);
        CancelarCommand = new AsyncRelayCommand(CancelarAsync);
    }

    private void AtualizarFotoECarga()
    {
        // Foto automática baseada no Tipo
        if (!TipoParaImagem.TryGetValue(Tipo, out var img))
            img = "halteres"; // fallback caso falte imagem
        Foto = img;

        // Carga habilitada só para exercícios de força
        var semCarga = TiposSemCarga.Contains(Tipo);
        CargaHabilitada = !semCarga;
        if (semCarga)
            Carga = 0; // força zero para cardio
    }

    private async Task SalvarAsync()
    {
        if (string.IsNullOrWhiteSpace(Tipo))
        {
            await Shell.Current.DisplayAlert("Atenção", "Informe o tipo do exercício.", "OK");
            return;
        }

        var novo = new Exercicio
        {
            Tipo = Tipo.Trim(),
            Repeticoes = Math.Max(0, Repeticoes),
            Carga = Math.Max(0, Carga), // para cardio já estará 0
            Data = Data.Date,
            Foto = Foto // já vem do mapeamento acima
        };

        await _db.AddAsync(novo);
        await Shell.Current.Navigation.PopAsync();
    }

    private async Task CancelarAsync() => await Shell.Current.Navigation.PopAsync();
}
