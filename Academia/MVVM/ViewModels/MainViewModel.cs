using System.Collections.ObjectModel;
using Academia.MVVM.Models;
using Academia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Academia.MVVM.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly IAcademiaDbService _db;
    private readonly IServiceProvider _services;

    public ObservableCollection<Exercicio> Itens { get; } = new();

    private DateTime _dataSelecionada = DateTime.Today;
    public DateTime DataSelecionada
    {
        get => _dataSelecionada;
        set => SetProperty(ref _dataSelecionada, value);
    }

    private int _totalDoDia;
    public int TotalDoDia
    {
        get => _totalDoDia;
        set => SetProperty(ref _totalDoDia, value);
    }

    private List<DateTime> _diasAnteriores = new();
    public List<DateTime> DiasAnteriores
    {
        get => _diasAnteriores;
        set => SetProperty(ref _diasAnteriores, value);
    }

    public IAsyncRelayCommand CarregarCommand { get; }
    public IAsyncRelayCommand<DateTime> TrocarDataCommand { get; }
    public IAsyncRelayCommand HojeCommand { get; }
    public IAsyncRelayCommand AdicionarCommand { get; }
    public IAsyncRelayCommand<Exercicio> ExcluirCommand { get; }

    // NOVO: abrir detalhes ao tocar no item
    public IAsyncRelayCommand<Exercicio> AbrirDetalheCommand { get; }

    public MainViewModel(IAcademiaDbService db, IServiceProvider services)
    {
        _db = db;
        _services = services;

        CarregarCommand = new AsyncRelayCommand(CarregarAsync);
        TrocarDataCommand = new AsyncRelayCommand<DateTime>(TrocarDataAsync);
        HojeCommand = new AsyncRelayCommand(HojeAsync);
        AdicionarCommand = new AsyncRelayCommand(AdicionarAsync);
        ExcluirCommand = new AsyncRelayCommand<Exercicio>(ExcluirAsync);

        AbrirDetalheCommand = new AsyncRelayCommand<Exercicio>(AbrirDetalheAsync);
    }

    private async Task CarregarAsync()
    {
        await _db.InitAsync();
        await AtualizarListaAsync();
        DiasAnteriores = await _db.GetDatasComRegistroAsync();
    }

    private async Task TrocarDataAsync(DateTime novaData)
    {
        DataSelecionada = novaData.Date;
        await AtualizarListaAsync();
    }

    private async Task HojeAsync()
    {
        DataSelecionada = DateTime.Today;
        await AtualizarListaAsync();
    }

    private async Task AdicionarAsync()
    {
        var page = _services.GetService(typeof(Academia.MVVM.Views.AddExercicioPage)) as Page;
        if (page is not null)
            await Shell.Current.Navigation.PushAsync(page);
    }

    private async Task ExcluirAsync(Exercicio item)
    {
        if (item is null) return;
        await _db.DeleteAsync(item);
        await AtualizarListaAsync();
        DiasAnteriores = await _db.GetDatasComRegistroAsync();
    }

    private async Task AbrirDetalheAsync(Exercicio item)
    {
        if (item is null) return;

        var page = _services.GetService(typeof(Academia.MVVM.Views.ExercicioDetailPage)) as Page;
        if (page is null) return;

        if (page.BindingContext is Academia.MVVM.ViewModels.ExercicioDetailViewModel vm)
        {
            vm.SetExercicio(item);
        }

        await Shell.Current.Navigation.PushAsync(page);
    }

    private async Task AtualizarListaAsync()
    {
        Itens.Clear();
        var lista = await _db.GetByDateAsync(DataSelecionada);
        foreach (var e in lista) Itens.Add(e);
        TotalDoDia = Itens.Count;
    }
}
