using Spomenar.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Spomenar.Pages;

public partial class HistoryPage : ContentPage
{
    private static readonly string[] QuestionOrder = new[]
    {
        "Прво, како се чувствуваш денес?",
        "Потоа, размисли за миг, кој ти беше најубав момент денес?",
        "А кој најмалку убав?",
        "Издвој ми неколку нешта за што си најблагодарен/на денес?",
        "Што те научи денешниот ден?",
        "Запозна ли нова личност денес?",
        "Дали го промени мислењето за нешто што ти е омилено? Ако да, што е тоа и што влијаеше за тоа да се смени?"
    };

    public ObservableCollection<QuestionGroup> QuestionGroups { get; set; }
    public ICommand ToggleGroupCommand { get; }
    public ICommand DeleteAnswerCommand { get; }

    public HistoryPage()
    {
        InitializeComponent();
        ToggleGroupCommand = new Command<string>(ToggleGroup);
        DeleteAnswerCommand = new Command<AnswerModel>(async (answer) => await DeleteAnswerAsync(answer));
        LoadData();
        BindingContext = this;
    }

    private async void LoadData()
    {
        var answers = await App.Database.GetAnswersAsync();
        var groups = answers
            .GroupBy(a => a.Question)
            .OrderBy(g => Array.IndexOf(QuestionOrder, g.Key))
            .Select(g => new QuestionGroup(g.Key, g.OrderBy(a => a.Date)))
            .ToList();
        QuestionGroups = new ObservableCollection<QuestionGroup>(groups);
        QuestionsCollectionView.ItemsSource = QuestionGroups;
    }

    private async Task DeleteAnswerAsync(AnswerModel answer)
    {
        if (answer == null) return;
        bool confirm = await DisplayAlert("Бришење", "Дали сте сигурни дека сакате да го избришете одговорот?", "Да", "Не");
        if (!confirm) return;

        await App.Database.DeleteAnswerAsync(answer);
        LoadData();
    }

    private void ToggleGroup(string question)
    {
        var group = QuestionGroups.FirstOrDefault(g => g.Question == question);
        if (group != null)
            group.IsExpanded = !group.IsExpanded;
    }
}

public class QuestionGroup : ObservableCollection<AnswerModel>
{
    public string Question { get; }
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            _isExpanded = value;
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(IsExpanded)));
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(VisibleItems)));
        }
    }
    private bool _isExpanded = false;

    public bool ShowAll
    {
        get => _showAll;
        set
        {
            _showAll = value;
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(ShowAll)));
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(VisibleItems)));
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(ShowAllButtonText)));
        }
    }
    private bool _showAll = false;

    public IEnumerable<AnswerModel> VisibleItems =>
        ShowAll ? this.OrderBy(a => a.Date) : this.OrderBy(a => a.Date).Take(5);

    public string ShowAllButtonText => ShowAll ? "Скриј" : "Види ги сите";
    public bool ShowAllButtonVisible => this.Count > 5;

    public Command ToggleShowAllCommand { get; }
    
    public QuestionGroup(string question, IEnumerable<AnswerModel> answers) : base(answers)
    {
        Question = question;
        ToggleShowAllCommand = new Command(() =>
        {
            ShowAll = !ShowAll;
        });
    }
}