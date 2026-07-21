using Expense_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal Amount { get; set; }
        public required TransactionType Type { get; set; }
        public required TransactionCategory Category { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
