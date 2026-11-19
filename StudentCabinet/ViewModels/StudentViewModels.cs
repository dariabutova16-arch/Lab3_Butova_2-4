using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StudentCabinet.Models;
using StudentCabinet.Services;

namespace StudentCabinet.ViewModels;

public class StudentViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private Student _student = new Student();
    private readonly IAppStateService _state;
    private readonly IStudentRepository _repository;
    private bool _loaded;

    public ObservableCollection<Student> Students { get; } = new();
    public ICommand AddStudentCommand { get; }
    public ICommand OpenDetailsCommand { get; }
    public ICommand DeleteStudentCommand { get; }
    public ICommand LoadCommand { get; }

    public StudentViewModel(IAppStateService state, IStudentRepository repository)
    {
        _state = state;
        _repository = repository;
        AddStudentCommand = new Command(async () => await AddStudentAsync(), CanAddStudent);
        OpenDetailsCommand = new Command<Student>(async (student) => await OpenDetailsAsync(student));
        DeleteStudentCommand = new Command<Student>(async (student) => await DeleteStudentAsync(student));
        LoadCommand = new Command(async () => await LoadAsync());
    }

    public string FullName
    {
        get => _student.FullName;
        set
        {
            if (_student.FullName != value)
            {
                _student.FullName = value;
                _state.DraftFullName = value;
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(Greeting));
                ((Command)AddStudentCommand).ChangeCanExecute();
            }
        }
    }

    public string Group
    {
        get => _student.Group;
        set
        {
            if (_student.Group != value)
            {
                _student.Group = value;
                _state.DraftGroup = value;
                OnPropertyChanged(nameof(Group));
                OnPropertyChanged(nameof(Greeting));
            }
        }
    }

    public double AverageScore
    {
        get => _student.AverageScore;
        set
        {
            if (_student.AverageScore != value)
            {
                _student.AverageScore = value;
                _state.DraftScore = value;
                OnPropertyChanged(nameof(AverageScore));
                OnPropertyChanged(nameof(IsGoodScore));
            }
        }
    }

    public string Greeting => "Студент: " + FullName + ", група " + Group;

    public bool IsGoodScore => AverageScore >= 4.0;

    private async Task LoadAsync()
    {
        FullName = _state.DraftFullName;
        Group = _state.DraftGroup;
        AverageScore = _state.DraftScore;
        System.Diagnostics.Debug.WriteLine("[VM] Стан форми відновлено");

        if (_loaded) return;
        _loaded = true;

        var items = await _repository.GetAllAsync();
        Students.Clear();
        foreach (var item in items)
            Students.Add(item);
        System.Diagnostics.Debug.WriteLine("[DB] Завантажено студентів: " + items.Count);
    }

    private async Task AddStudentAsync()
    {
        var student = new Student
        {
            FullName = FullName,
            Group = Group,
            AverageScore = AverageScore
        };

        await _repository.SaveAsync(student);
        Students.Add(student);
        System.Diagnostics.Debug.WriteLine("[DB] Додано: " + student.FullName + " (Id " + student.Id + ")");

        FullName = string.Empty;
        Group = string.Empty;
    }

    private bool CanAddStudent()
    {
        return !string.IsNullOrWhiteSpace(FullName);
    }

    private async Task DeleteStudentAsync(Student student)
    {
        if (student is null) return;

        await _repository.DeleteAsync(student);
        Students.Remove(student);
        System.Diagnostics.Debug.WriteLine("[DB] Видалено: " + student.FullName);
    }

    private async Task OpenDetailsAsync(Student student)
    {
        if (student is null) return;

        var parameters = new Dictionary<string, object>
        {
            { "SelectedStudent", student }
        };
        await Shell.Current.GoToAsync("studentdetail", parameters);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("UpdatedStudent", out var updatedObj) &&
            query.TryGetValue("OriginalStudent", out var originalObj) &&
            updatedObj is Student updated &&
            originalObj is Student original)
        {
            _ = ReplaceStudentAsync(original, updated);
        }
    }

    private async Task ReplaceStudentAsync(Student original, Student updated)
    {
        updated.Id = original.Id;
        await _repository.SaveAsync(updated);

        var index = Students.IndexOf(original);
        if (index >= 0)
            Students[index] = updated;
        System.Diagnostics.Debug.WriteLine("[DB] Оновлено: " + updated.FullName);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}