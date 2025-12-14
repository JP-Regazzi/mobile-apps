using System;
using Microsoft.Maui.Controls;

namespace JogoDaForca;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }
}
