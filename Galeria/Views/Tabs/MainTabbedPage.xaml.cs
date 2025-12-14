using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace Galeria.Views;

public partial class MainTabbedPage : Microsoft.Maui.Controls.TabbedPage
{
    public MainTabbedPage()
    {
        InitializeComponent();
        Microsoft.Maui.Controls.NavigationPage.SetHasNavigationBar(this, false);

#if ANDROID
        // No Android, força a barra de abas na parte inferior da tela
        this.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>()
            .SetToolbarPlacement(
                Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
#endif
    }
}
