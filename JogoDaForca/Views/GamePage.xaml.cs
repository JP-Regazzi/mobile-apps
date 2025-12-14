using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;

namespace JogoDaForca.Views;

public partial class GamePage : ContentPage
{
    private readonly Dictionary<string, string[]> _palavrasPorCategoria = new()
    {
        ["Animal"] = new[] { "ELEFANTE", "GATO", "TARTARUGA" },
        ["Objeto"] = new[] { "CADEIRA", "RELOGIO", "GARRAFA" },
        ["Vegetal"] = new[] { "CENOURA", "ALFACE", "BATATA" },
        ["Fruta"] = new[] { "BANANA", "ABACAXI", "MANGA" },
        ["Verbo"] = new[] { "CORRER", "PULAR", "ESCREVER" },
    };

    private string? _categoriaAtual;
    private string? _palavraSecreta;
    private readonly HashSet<char> _acertos = new();
    private readonly HashSet<char> _erros = new();
    private int _vidasRestantes = 6;

    public GamePage()
    {
        InitializeComponent();
        NovaPartida();
    }

    private IEnumerable<Button> KeyboardButtons =>
        Row1.Children.OfType<Button>()
        .Concat(Row2.Children.OfType<Button>())
        .Concat(Row3.Children.OfType<Button>());

    private void NovaPartida()
    {
        _acertos.Clear();
        _erros.Clear();
        _vidasRestantes = 6;

        var rnd = new Random();
        var categorias = _palavrasPorCategoria.Keys.ToList();
        _categoriaAtual = categorias[rnd.Next(categorias.Count)];
        var lista = _palavrasPorCategoria[_categoriaAtual];
        _palavraSecreta = lista[rnd.Next(lista.Length)];

        HangImage.Source = "forca1.png";
        LblCategoria.Text = _categoriaAtual!;
        AtualizarPalavra();
        HabilitarTeclado(true);
        ResetarCoresTeclas();
    }

    private void AtualizarPalavra()
    {
        if (_palavraSecreta is null) return;

        var sb = new StringBuilder();
        foreach (char ch in _palavraSecreta)
        {
            if (char.IsLetter(ch))
                sb.Append(_acertos.Contains(ch) ? ch : '_').Append(' ');
            else
                sb.Append(ch).Append(' ');
        }
        LblPalavra.Text = sb.ToString().Trim();

        bool venceu = _palavraSecreta.All(c => !char.IsLetter(c) || _acertos.Contains(c));
        if (venceu)
            FimDeJogo(true);
    }

    private void OnKeyClicked(object sender, EventArgs e)
    {
        if (_palavraSecreta is null) return;
        if (sender is not Button btn) return;

        char letra = char.ToUpperInvariant(btn.Text?.FirstOrDefault() ?? '\0');
        if (!char.IsLetter(letra) || btn.IsEnabled == false) return;

        if (_palavraSecreta.Contains(letra))
        {
            _acertos.Add(letra);
            btn.BackgroundColor = Colors.LightSeaGreen;   // Verde = acerto
            btn.TextColor = Colors.White;
        }
        else
        {
            if (_erros.Add(letra))
            {
                _vidasRestantes--;
                AtualizarForca();
            }
            btn.BackgroundColor = Colors.IndianRed;    // vermelho = erro
            btn.TextColor = Colors.White;
        }

        btn.IsEnabled = false;
        AtualizarPalavra();

        if (_vidasRestantes <= 0)
            FimDeJogo(false);
    }

    private void AtualizarForca()
    {
        // 6 vidas -> forca1; ... ; 0 vidas -> forca7
        int indice = 7 - _vidasRestantes;
        if (indice < 1) indice = 1;
        if (indice > 7) indice = 7;
        HangImage.Source = $"forca{indice}.png";
    }

    private async void FimDeJogo(bool venceu)
    {
        HabilitarTeclado(false);
        string titulo = venceu ? "Parabéns!" : "Você perdeu!";
        string msg = $"A palavra era: {_palavraSecreta}";
        bool jogarNovamente = await DisplayAlert(titulo, msg, "Jogar novamente", "Menu");

        if (jogarNovamente) NovaPartida();
        else await Navigation.PopAsync();
    }

    private void HabilitarTeclado(bool habilitar)
    {
        foreach (var b in KeyboardButtons)
            b.IsEnabled = habilitar;
    }

    private void ResetarCoresTeclas()
    {
        foreach (var b in KeyboardButtons)
        {
            b.ClearValue(Button.BackgroundColorProperty);
            b.ClearValue(Button.TextColorProperty);
        }
    }

    private void OnRestartClicked(object sender, EventArgs e) => NovaPartida();
    private async void OnExitClicked(object sender, EventArgs e) => await Navigation.PopAsync();
}
