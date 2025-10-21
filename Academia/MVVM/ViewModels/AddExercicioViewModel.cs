using Academia.MVVM.Models;
using Academia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Academia.MVVM.ViewModels;

public class AddExercicioViewModel : ObservableObject
{
    private readonly IAcademiaDbService _db;

    private string _tipo = "";
    public string Tipo
    {
        get => _tipo;
        set => SetProperty(ref _tipo, value);
    }

    private int _repeticoes = 10;
    public int Repeticoes
    {
        get => _repeticoes;
        set => SetProperty(ref _repeticoes, value);
    }

    private double _carga = 0;
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

    private string _foto = "halteres";
    public string Foto
    {
        get => _foto;
        set => SetProperty(ref _foto, value);
    }

    public List<string> OpcoesFoto { get; } = new() { "halteres", "corrida", "yoga" };
    public List<string> OpcoesTipo { get; } = new() { "Supino", "Agachamento", "Corrida", "Remada", "Bicicleta", "Flexão" };

    public IAsyncRelayCommand SalvarCommand { get; }
    public IAsyncRelayCommand CancelarCommand { get; }

    public AddExercicioViewModel(IAcademiaDbService db)
    {
        _db = db;
        SalvarCommand = new AsyncRelayCommand(SalvarAsync);
        CancelarCommand = new AsyncRelayCommand(CancelarAsync);
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
            Carga = Math.Max(0, Carga),
            Data = Data.Date,
            Foto = string.IsNullOrWhiteSpace(Foto) ? "halteres" : Foto
        };

        await _db.AddAsync(novo);
        await Shell.Current.Navigation.PopAsync();
    }

    private async Task CancelarAsync() => await Shell.Current.Navigation.PopAsync();
}
