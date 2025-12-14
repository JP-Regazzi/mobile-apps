using System.ComponentModel;
using TaskCRUD.ViewModels;

namespace TaskCRUD
{
    public partial class MainPage : ContentPage
    {
        public TaskViewModel ViewModel => (TaskViewModel)BindingContext;

        public MainPage()
        {
            InitializeComponent();

            if (BindingContext is TaskViewModel vm)
            {
                vm.PropertyChanged += OnViewModelPropertyChanged;
                Title = vm.HasTasks ? "Lista" : "Início";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is not TaskViewModel vm) return;

            if (e.PropertyName == nameof(TaskViewModel.HasTasks) ||
                e.PropertyName == nameof(TaskViewModel.HasNoTasks))
            {
                Title = vm.HasTasks ? "Lista" : "Início";
            }
        }

        private async void OnAddTaskTapped(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskFormPage(ViewModel));
        }
    }
}
