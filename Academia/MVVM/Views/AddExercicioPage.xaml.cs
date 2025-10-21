using Academia.MVVM.ViewModels;

namespace Academia.MVVM.Views;

public partial class AddExercicioPage : ContentPage
{
    public AddExercicioPage(AddExercicioViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
