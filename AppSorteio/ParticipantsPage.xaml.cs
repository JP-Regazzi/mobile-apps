using Microsoft.Maui.Controls;

namespace AppSorteio;

public partial class ParticipantsPage : ContentPage
{
    private static readonly Random _random = new();

    public ParticipantsPage()
    {
        InitializeComponent();
    }

    private async void OnSortearClicked(object sender, EventArgs e)
    {
        var texto = NamesEditor.Text;

        if (string.IsNullOrWhiteSpace(texto))
        {
            await DisplayAlert("Atenção", "Digite ao menos um nome.", "OK");
            return;
        }

        // Separa por linhas, tirando vazios
        var separadores = new[] { '\r', '\n' };
        var nomes = texto
            .Split(separadores, StringSplitOptions.RemoveEmptyEntries)
            .Select(n => n.Trim())
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .ToArray();

        if (nomes.Length == 0)
        {
            await DisplayAlert("Atenção", "Digite ao menos um nome válido.", "OK");
            return;
        }

        var indice = _random.Next(nomes.Length);
        var sorteado = nomes[indice];

        // Navega para a tela de resultado, passando o nome sorteado
        await Navigation.PushAsync(new ResultPage(sorteado));
    }
}
