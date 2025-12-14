using Microsoft.Maui.Controls;

namespace AppSorteio;

public partial class ResultPage : ContentPage
{
    public ResultPage(string winnerName)
    {
        InitializeComponent();
        WinnerLabel.Text = winnerName;
    }
}
