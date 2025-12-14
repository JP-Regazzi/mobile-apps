namespace TaskCRUD;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var navPage = new NavigationPage(new MainPage())
        {
            // barra superior cinza claro
            BarBackgroundColor = Color.FromArgb("#F4F4F4"),
            BarTextColor = Colors.Black
        };

        MainPage = navPage;
    }
}
