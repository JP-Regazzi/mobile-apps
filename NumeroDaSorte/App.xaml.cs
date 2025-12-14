using Microsoft.Maui.Controls;

namespace NumeroDaSorte;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Tela principal: nossa MainPage simples
        MainPage = new MainPage();
    }
}
