using TaskCRUD.ViewModels;

namespace TaskCRUD
{
    public partial class TaskFormPage : ContentPage
    {
        private readonly TaskViewModel _viewModel;

        public TaskFormPage(TaskViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.ResetNewTask();
        }

        private async void OnCloseTapped(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnSaveTapped(object? sender, EventArgs e)
        {
            if (_viewModel.SaveTaskCommand.CanExecute(null))
            {
                _viewModel.SaveTaskCommand.Execute(null);
                await Navigation.PopAsync();
            }
        }

        private void OnDateSelected(object? sender, DateChangedEventArgs e)
        {
            // Quando o usuário escolhe a data no DatePicker,
            // atualizamos o texto da data no ViewModel.
            _viewModel.NewDateText = e.NewDate.ToString("dd/MM/yyyy");
        }
    }
}
