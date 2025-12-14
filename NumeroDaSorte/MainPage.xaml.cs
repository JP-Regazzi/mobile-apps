using System.Collections.Generic;

namespace NumeroDaSorte;

public partial class MainPage : ContentPage
{
    private readonly Random _random = new();

    public MainPage()
    {
        InitializeComponent();
    }

    // Evento do botão
    private void OnGerarNumeroClicked(object sender, EventArgs e)
    {
        // Gera 5 números distintos entre 1 e 60
        var numeros = GerarNumeros(5, 1, 60);

        var lista = numeros.ToList();

        Num1Label.Text = lista[0].ToString("D2");
        Num2Label.Text = lista[1].ToString("D2");
        Num3Label.Text = lista[2].ToString("D2");
        Num4Label.Text = lista[3].ToString("D2");
        Num5Label.Text = lista[4].ToString("D2");

        // Exibe o bloco de resultado
        ResultadoLayout.IsVisible = true;
    }

    // Usa SortedSet para ordenar e não repetir
    private SortedSet<int> GerarNumeros(int quantidade, int minInclusive, int maxInclusive)
    {
        var set = new SortedSet<int>();

        while (set.Count < quantidade)
        {
            int n = _random.Next(minInclusive, maxInclusive + 1);
            set.Add(n);
        }

        return set;
    }
}
