using Spomenar.Models;

namespace Spomenar.Pages;

public partial class TodayPage : ContentPage {
    public TodayPage() {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e) {
        var today = DateTime.Today.ToString("yyyy-MM-dd");

        if (!string.IsNullOrWhiteSpace(Answer1.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "Прво, како се чувствуваш денес?",
                Text = Answer1.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        if (!string.IsNullOrWhiteSpace(Answer2.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "Потоа, размисли за миг, кој ти беше најубав момент денес?",
                Text = Answer2.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        if (!string.IsNullOrWhiteSpace(Answer3.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "А кој најмалку убав?",
                Text = Answer3.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        if (!string.IsNullOrWhiteSpace(Answer4.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "Издвој ми неколку нешта за што си најблагодарен/на денес?",
                Text = Answer4.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        if (!string.IsNullOrWhiteSpace(Answer5.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "Што те научи денешниот ден?",
                Text = Answer5.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        if (!string.IsNullOrWhiteSpace(Answer6.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "Запозна ли нова личност денес?",
                Text = Answer6.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        if (!string.IsNullOrWhiteSpace(Answer7.Text)) {
            var saved = await App.Database.SaveAnswerAsync(new AnswerModel {
                Question = "Дали го промени мислењето за нешто што ти е омилено? Ако да, што е тоа и што влијаеше за тоа да се смени?",
                Text = Answer7.Text,
                Date = today
            });
            System.Diagnostics.Debug.WriteLine($"Saved ID: {saved.Id} | {saved.Question}");
        }

        // Исчисти ги полињата
        Answer1.Text = Answer2.Text = Answer3.Text =
        Answer4.Text = Answer5.Text = Answer6.Text = Answer7.Text = string.Empty;

        await DisplayAlert("Успешно", "Одговорите се сочувани!", "OK");
    }
}
