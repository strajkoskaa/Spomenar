using Spomenar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spomenar.Data {
    public class AppDatabase {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath) {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AnswerModel>().Wait();
        }

        public async Task<AnswerModel> SaveAnswerAsync(AnswerModel answer) {
            await _database.InsertAsync(answer);
            Debug.WriteLine($"Saved answer ID: {answer.Id}");
            return answer; 
        }

        public async Task<List<AnswerModel>> GetAnswersAsync() {
            var list = await _database.Table<AnswerModel>().ToListAsync();
            foreach (var a in list)
                System.Diagnostics.Debug.WriteLine($"Answer: {a.Id} {a.Question}");
            return list;
        }

        public Task<List<AnswerModel>> GetAnswersByDateAsync(string date) {
            return _database.Table<AnswerModel>().Where(a => a.Date == date).ToListAsync();
        }

        public Task<List<AnswerModel>> GetAnswersByQuestionAsync(string question) {
            return _database.Table<AnswerModel>().Where(a => a.Question == question).OrderBy(a => a.Id).Take(10).ToListAsync();
        }

        public async Task DeleteAnswerAsync(AnswerModel answer) {
            if (answer == null || answer.Id == 0) {
                System.Diagnostics.Debug.WriteLine($"Delete failed: answer null or Id=0. Answer: {answer?.Text}");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"Deleting answer with ID: {answer.Id}");
            await _database.DeleteAsync(answer);
        }


        public async Task DeleteAllAnswersAsync() {
            await _database.DeleteAllAsync<AnswerModel>();
        }
    }
}
