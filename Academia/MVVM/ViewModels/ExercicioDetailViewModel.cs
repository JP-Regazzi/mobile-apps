using Academia.MVVM.Models;
using Academia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Academia.MVVM.ViewModels;

public class ExercicioDetailViewModel : ObservableObject
{
    private readonly IAcademiaDbService _db;

    private Exercicio? _item;
    public Exercicio? Item
    {
        get => _item;
        private set => SetProperty(ref _item, value);
    }

    public IAsyncRelayCommand ExcluirCommand { get; }
    public IAsyncRelayCommand VoltarCommand { get; }

    public ExercicioDetailViewModel(IAcademiaDbService db)
    {
        _db = db;
        ExcluirCommand = new AsyncRelayCommand(ExcluirAsync);
        VoltarCommand = new AsyncRelayCommand(VoltarAsync);
    }

    public void SetExercicio(Exercicio exercicio)
    {
        Item = exercicio;
    }

    private async Task ExcluirAsync()
    {
        if (Item is null) return;

        var confirma = await Shell.Current.DisplayAlert(
            "Excluir",
            $"Excluir o exercício \"{Item.Tipo}\" de {Item.Data:dd/MM}?",
            "Sim", "Não");

        if (!confirma) return;

        await _db.DeleteAsync(Item);
        await Shell.Current.DisplayAlert("Pronto", "Exercício excluído.", "OK");

        // Volta para a lista; a MainPage vai recarregar ao aparecer
        await Shell.Current.Navigation.PopAsync();
    }

    private async Task VoltarAsync()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
