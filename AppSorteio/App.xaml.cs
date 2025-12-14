using Microsoft.Maui.Controls;

namespace AppSorteio;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Primeira tela é a Splash dentro de um NavigationPage
        MainPage = new NavigationPage(new SplashPage())
        {
            BarBackgroundColor = (Color)Current.Resources["PrimaryColor"],
            BarTextColor = Colors.White
        };
    }
}
