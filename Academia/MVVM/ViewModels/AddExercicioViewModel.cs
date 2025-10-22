using Academia.MVVM.Models;
using Academia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Academia.MVVM.ViewModels;

public class AddExercicioViewModel : ObservableObject
{
    private readonly IAcademiaDbService _db;

    public List<string> OpcoesTipo { get; } = new()
    {
        "Supino", "Remada", "Agachamento", "Flexão",
        "Corrida", "Bicicleta", "Yoga"
    };

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

    private static readonly HashSet<string> TiposCardio = new(StringComparer.OrdinalIgnoreCase)
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
                AtualizarCamposPorTipo();
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

    private int _duracaoMinutos = 0;
    public int DuracaoMinutos
    {
        get => _duracaoMinutos;
        set => SetProperty(ref _duracaoMinutos, value);
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

    // Flags de UI
    private bool _cargaHabilitada = true;
    public bool CargaHabilitada
    {
        get => _cargaHabilitada;
        set => SetProperty(ref _cargaHabilitada, value);
    }

    private bool _repeticoesHabilitadas = true;
    public bool RepeticoesHabilitadas
    {
        get => _repeticoesHabilitadas;
        set => SetProperty(ref _repeticoesHabilitadas, value);
    }

    private bool _duracaoHabilitada = false;
    public bool DuracaoHabilitada
    {
        get => _duracaoHabilitada;
        set => SetProperty(ref _duracaoHabilitada, value);
    }

    public IAsyncRelayCommand SalvarCommand { get; }
    public IAsyncRelayCommand CancelarCommand { get; }

    public AddExercicioViewModel(IAcademiaDbService db)
    {
        _db = db;
        AtualizarCamposPorTipo();
        SalvarCommand = new AsyncRelayCommand(SalvarAsync);
        CancelarCommand = new AsyncRelayCommand(CancelarAsync);
    }

    private void AtualizarCamposPorTipo()
    {
        // Foto automática
        Foto = TipoParaImagem.TryGetValue(Tipo, out var img) ? img : "halteres";

        var isCardio = TiposCardio.Contains(Tipo);

        // Cardio: duração visível, repetições/carga desativadas
        DuracaoHabilitada = isCardio;
        RepeticoesHabilitadas = !isCardio;
        CargaHabilitada = !isCardio;

        if (isCardio)
        {
            Carga = 0;
            Repeticoes = 0;
            if (DuracaoMinutos <= 0) DuracaoMinutos = 10; // sugestão inicial
        }
        else
        {
            DuracaoMinutos = 0;
            if (Repeticoes <= 0) Repeticoes = 10;
        }
    }

    private async Task SalvarAsync()
    {
        if (string.IsNullOrWhiteSpace(Tipo))
        {
            await Shell.Current.DisplayAlert("Atenção", "Informe o tipo do exercício.", "OK");
            return;
        }

        // Validação simples
        var isCardio = TiposCardio.Contains(Tipo);
        if (isCardio && DuracaoMinutos <= 0)
        {
            await Shell.Current.DisplayAlert("Atenção", "Informe a duração (minutos) para atividades de cardio.", "OK");
            return;
        }
        if (!isCardio && Repeticoes <= 0)
        {
            await Shell.Current.DisplayAlert("Atenção", "Informe as repetições.", "OK");
            return;
        }

        var novo = new Exercicio
        {
            Tipo = Tipo.Trim(),
            Repeticoes = Math.Max(0, Repeticoes),
            Carga = Math.Max(0, Carga),
            DuracaoMinutos = Math.Max(0, DuracaoMinutos),
            Data = Data.Date,
            Foto = Foto
        };

        await _db.AddAsync(novo);
        await Shell.Current.Navigation.PopAsync();
    }

    private async Task CancelarAsync() => await Shell.Current.Navigation.PopAsync();
}
