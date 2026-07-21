using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Text.Json;

namespace Expense_Tracker.Storage
{
    public class JsonStorage<T> : IStorage<T>
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "jsonstorage.json");
        private List<T>? _transactionsList;
        public List<T> Load()
        {
            if (File.Exists(filePath)) { 
                string json = File.ReadAllText(filePath);

                _transactionsList = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                return _transactionsList;
            } else
            {
                _transactionsList = new List<T>();
                return _transactionsList;
            }
        }

        public void Save(List<T> items)
        {
            string json = JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                WriteIndented = true,
            });

            File.WriteAllText(filePath, json);
        }
    }
}
