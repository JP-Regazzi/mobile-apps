using Microsoft.Maui;
using Android.App;
using Android.Content.PM;

namespace JogoDaForca;

[Activity(Theme = "@style/Maui.SplashTheme",
          MainLauncher = true,
          ConfigurationChanges =
              ConfigChanges.ScreenSize |
              ConfigChanges.Orientation |
              ConfigChanges.UiMode |
              ConfigChanges.ScreenLayout |
              ConfigChanges.SmallestScreenSize |
              ConfigChanges.Density)]
public class MainActivity : Microsoft.Maui.MauiAppCompatActivity
{
}
