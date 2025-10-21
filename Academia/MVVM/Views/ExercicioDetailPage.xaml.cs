using Academia.MVVM.ViewModels;

namespace Academia.MVVM.Views;

public partial class ExercicioDetailPage : ContentPage
{
    public ExercicioDetailPage(ExercicioDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
