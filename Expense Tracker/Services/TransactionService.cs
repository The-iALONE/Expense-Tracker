using Expense_Tracker.Enums;
using Expense_Tracker.Models;
using Expense_Tracker.Storage;

namespace Expense_Tracker.Services
{
    public class TransactionService
    {
        private List<Transaction> _transactions;
        private IStorage<Transaction> _storage;

        public TransactionService(List<Transaction> transactionList, IStorage<Transaction> storage)
        {
            _transactions = transactionList;
            _storage = storage;
        }

        public Transaction? GetById(int id)
        {
            return _transactions.FirstOrDefault(t => t.Id == id);
        }

        public bool DeleteTransaction(int id)
        {
            Transaction? transaction = GetById(id);
            _transactions.Remove(transaction!);
            _storage.Save(_transactions);
            return true;
        }

        public bool UpdateTransaction(
            int id,
            string title,
            TransactionType type,
            TransactionCategory category,
            DateTime date,
            string Description)
        {
            Transaction? transaction = GetById(id);
            transaction!.Title = title;
            transaction.Type = type;
            transaction.Category = category;
            transaction.Date = date;
            transaction.Description = Description;
            _storage.Save(_transactions);
            return true;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            _storage.Save(_transactions);
        }

        public List<Transaction> Search(string searchTerm)
        {
            bool isCategory = Enum.TryParse<TransactionCategory>(searchTerm, true, out TransactionCategory category);
            return _transactions.Where(t=>t.Title.Contains(searchTerm) || (isCategory && t.Category == category)).ToList();
        }

        public List<Transaction> GetByType(TransactionType type)
        {
            return _transactions.Where(t => t.Type == type).ToList();
        }

        public List<Transaction> GetByCategorty(TransactionCategory category)
        {
            return _transactions.Where(t=>t.Category == category).ToList();
        }

        public List<Transaction> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _transactions.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
        }

        public List<Transaction> GetExpensesGreaterThan(decimal amount)
        {
            return _transactions.Where(t=> t.Amount >= amount && t.Type == TransactionType.Expense).ToList();
        }

        public List<Transaction> GetAll()
        {
            return _transactions.ToList();
        }
    }
}
