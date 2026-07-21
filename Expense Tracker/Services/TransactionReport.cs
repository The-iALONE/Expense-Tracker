using System;
using System.Collections.Generic;
using System.Text;
using Expense_Tracker.Models;
using Expense_Tracker.Enums;

namespace Expense_Tracker.Services
{
    public class TransactionReport
    {
        public decimal GetTotalIncome(IEnumerable<Transaction> transactions)
        {
            return transactions.Where(t=>t.Type == TransactionType.Income).Sum(t=> t.Amount);
        }
        public decimal GetTotalExpenses(IEnumerable<Transaction> transactions)
        {
            return transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
        }

        public decimal GetBalance(IEnumerable<Transaction> transactions)
        {
            return GetTotalIncome(transactions) - GetTotalExpenses(transactions); 
        }

        public Dictionary<TransactionCategory, decimal> GetExpensesByCategory(IEnumerable<Transaction> transactions)
        {
            return transactions.Where(t => t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(t => t.Amount)
                );
        }

        public List<Transaction> GetMonthlyTransaction(IEnumerable<Transaction> transactions, int year, int month)
        {
            return transactions.Where(t=>t.Date.Year == year && t.Date.Month == month).OrderByDescending(t=>t.Date).ToList();   
        }
    }
}
 