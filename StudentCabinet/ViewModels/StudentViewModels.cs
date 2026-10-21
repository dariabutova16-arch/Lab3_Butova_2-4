using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StudentCabinet.Models;

namespace StudentCabinet.ViewModels;

public class StudentViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private Student _student = new Student();

    public ObservableCollection<Student> Students { get; } = new();
    public ICommand AddStudentCommand { get; }
    public ICommand OpenDetailsCommand { get; }

    public StudentViewModel()
    {
        AddStudentCommand = new Command(AddStudent, CanAddStudent);
        OpenDetailsCommand = new Command<Student>(async (student) => await OpenDetailsAsync(student));
    }

    public string FullName
    {
        get => _student.FullName;
        set
        {
            if (_student.FullName != value)
            {
                _student.FullName = value;
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
                OnPropertyChanged(nameof(AverageScore));
                OnPropertyChanged(nameof(IsGoodScore));
            }
        }
    }

    public string Greeting => $"Студент: