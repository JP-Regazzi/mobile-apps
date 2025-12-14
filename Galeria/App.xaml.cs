using Galeria.Views;

namespace Galeria;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // NavigationPage = vamos usar para poder abrir as telas de detalhe com PushAsync
        var tabbed = new MainTabbedPage();
        var nav = new NavigationPage(tabbed);

        // Esconde a navigation bar padrão para a página de abas
        NavigationPage.SetHasNavigationBar(tabbed, false);

        MainPage = nav;
    }
}
