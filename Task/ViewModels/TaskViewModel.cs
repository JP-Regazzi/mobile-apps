using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TaskCRUD.Models;

namespace TaskCRUD.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<TaskItem> Tasks { get; } = new();
        public ObservableCollection<TaskItem> FilteredTasks { get; } = new();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    ApplyFilter();
                }
            }
        }

        public bool HasTasks => FilteredTasks.Any();
        public bool HasNoTasks => !HasTasks;

        // Campos da nova tarefa
        private string _newTitle = string.Empty;
        public string NewTitle
        {
            get => _newTitle;
            set
            {
                if (_newTitle != value)
                {
                    _newTitle = value;
                    OnPropertyChanged(nameof(NewTitle));
                    UpdateCanSave();
                }
            }
        }

        private string _newDescription = string.Empty;
        public string NewDescription
        {
            get => _newDescription;
            set
            {
                if (_newDescription != value)
                {
                    _newDescription = value;
                    OnPropertyChanged(nameof(NewDescription));
                }
            }
        }

        // data em texto (digitada ou escolhida)
        private string _newDateText = string.Empty;
        public string NewDateText
        {
            get => _newDateText;
            set
            {
                if (_newDateText != value)
                {
                    _newDateText = value;
                    OnPropertyChanged(nameof(NewDateText));
                }
            }
        }

        public ObservableCollection<SubtaskItem> NewSubtasks { get; } = new();

        private int _nextSubtaskOrder = 1;

        public ICommand SaveTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ToggleTaskCompletedCommand { get; }
        public ICommand AddSubtaskCommand { get; }
        public ICommand DeleteSubtaskCommand { get; }
        public ICommand ToggleSubtaskCompletedCommand { get; }

        private bool _canSaveTask;
        public bool CanSaveTask
        {
            get => _canSaveTask;
            private set
            {
                if (_canSaveTask != value)
                {
                    _canSaveTask = value;
                    OnPropertyChanged(nameof(CanSaveTask));
                }
            }
        }

        public TaskViewModel()
        {
            SaveTaskCommand = new Command(SaveTask, () => CanSaveTask);
            DeleteTaskCommand = new Command<TaskItem>(DeleteTask);
            ToggleTaskCompletedCommand = new Command<TaskItem>(ToggleCompleted);
            AddSubtaskCommand = new Command(AddSubtask);
            DeleteSubtaskCommand = new Command<SubtaskItem>(DeleteSubtask);
            ToggleSubtaskCompletedCommand = new Command<SubtaskItem>(ToggleSubtaskCompleted);

            ResetNewTask();
            ApplyFilter();
        }

        public void ResetNewTask()
        {
            NewTitle = string.Empty;
            NewDescription = string.Empty;

            // data padrão: hoje
            NewDateText = DateTime.Today.ToString("dd/MM/yyyy",
                CultureInfo.GetCultureInfo("pt-BR"));

            NewSubtasks.Clear();
            _nextSubtaskOrder = 1;
            AddSubtask(); // começa com "Etapa 1"

            UpdateCanSave();
        }

        private void UpdateCanSave()
        {
            CanSaveTask = !string.IsNullOrWhiteSpace(NewTitle);
            (SaveTaskCommand as Command)?.ChangeCanExecute();
        }

        private void SaveTask()
        {
            // Converte o texto da data para DateTime; se der erro, usa hoje.
            var culture = CultureInfo.GetCultureInfo("pt-BR");
            DateTime date;

            if (!DateTime.TryParseExact(
                    NewDateText,
                    new[] { "dd/MM/yyyy", "d/M/yyyy", "ddMMyyyy" },
                    culture,
                    DateTimeStyles.None,
                    out date))
            {
                date = DateTime.Today;
            }

            var item = new TaskItem
            {
                Title = NewTitle.Trim(),
                Description = NewDescription?.Trim() ?? string.Empty,
                Date = date,
                IsCompleted = false,
                Subtasks = new ObservableCollection<SubtaskItem>(
                    NewSubtasks.Select(s => new SubtaskItem
                    {
                        Order = s.Order,
                        Title = s.Title,
                        IsCompleted = s.IsCompleted
                    }))
            };

            Tasks.Add(item);
            ApplyFilter();
            ResetNewTask();
        }

        private void DeleteTask(TaskItem? task)
        {
            if (task == null) return;
            Tasks.Remove(task);
            ApplyFilter();
        }

        private void ToggleCompleted(TaskItem? task)
        {
            if (task == null) return;
            task.IsCompleted = !task.IsCompleted;
        }

        private void AddSubtask()
        {
            NewSubtasks.Add(new SubtaskItem
            {
                Order = _nextSubtaskOrder,
                Title = $"Etapa {_nextSubtaskOrder}",
                IsCompleted = false
            });

            _nextSubtaskOrder++;
        }

        private void DeleteSubtask(SubtaskItem? subtask)
        {
            if (subtask == null) return;
            if (NewSubtasks.Contains(subtask))
                NewSubtasks.Remove(subtask);
        }

        private void ToggleSubtaskCompleted(SubtaskItem? subtask)
        {
            if (subtask == null) return;
            subtask.IsCompleted = !subtask.IsCompleted;
        }

        private void ApplyFilter()
        {
            FilteredTasks.Clear();

            IEnumerable<TaskItem> source = Tasks;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                source = source.Where(t =>
                    t.Title != null &&
                    t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            foreach (var task in source)
                FilteredTasks.Add(task);

            OnPropertyChanged(nameof(HasTasks));
            OnPropertyChanged(nameof(HasNoTasks));
        }
    }
}
