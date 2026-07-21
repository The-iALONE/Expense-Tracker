using Expense_Tracker.Enums;
using Expense_Tracker.Models;
using Expense_Tracker.Services;

namespace Expense_Tracker.Ui
{
    public class ConsoleUi
    {
        TransactionService _transactionService;
        TransactionReport _transactionReport;
        public ConsoleUi(TransactionService transactionService, TransactionReport transactionReport)
        {
            _transactionService = transactionService;
            _transactionReport = transactionReport;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    ShowMainUi();
                    if (!int.TryParse(Console.ReadLine(), out int input))
                    {
                        Console.Clear();
                        throw new FormatException("Invalid input");
                    }
                    switch (input)
                    {
                        case 1:
                            ShowAllTransactions();
                            break;
                        case 2:
                            AddTransaction();
                            break;
                        case 3:
                            EditTransaction();
                            break;
                        case 4:
                            DeleteTransaction();
                            break;
                        case 5:
                            SearchTransaction();
                            break;
                        case 6:
                            FilterTransactions();
                            break;
                        case 7:
                            FinancialReport();
                            break;
                        case 8:
                            MonthlyReport();
                            break;
                        case 0:
                            Exit();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid Operation");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void FilterTransactions()
        {
            Console.Clear();
            Console.WriteLine("Filter transactions By: ");
            Console.WriteLine("├── 1. Type");
            Console.WriteLine("│   ├── 1. Income");
            Console.WriteLine("│   └── 2. Expense");
            Console.WriteLine("│");
            Console.WriteLine("├── 2. Category");
            Console.WriteLine("│");
            Console.WriteLine("├── 3. This month");
            Console.WriteLine("│");
            Console.WriteLine("├── 4. Between two month");
            Console.WriteLine("│");
            Console.WriteLine("├── 5. Expenses more than");
            Console.WriteLine("│");
            Console.WriteLine("└── 0. Exit");
            Console.WriteLine("");
            Console.Write("Select: ");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                Console.Write("Number is invalid");
                return;
            }
            Console.Clear();
            switch (input)
            {
                case 1:
                    Console.WriteLine("Select type: ");
                    Console.WriteLine("1. Income");
                    Console.WriteLine("2. Expense");
                    Console.WriteLine("");
                    Console.Write("Select: ");
                    if (!int.TryParse(Console.ReadLine(), out int inputType))
                    {
                        Console.Write("Number is invalid");
                        return;
                    }
                    if (inputType < 1 || inputType > 2) return;
                    List<Transaction> transactionsByType;
                    if (inputType == 1)
                    {
                        transactionsByType = _transactionService.GetByType((TransactionType)0);
                    }
                    else
                    {
                        transactionsByType = _transactionService.GetByType((TransactionType)1);
                    }
                    Console.Clear();
                    Console.Write("Results: \n");
                    Console.WriteLine("------------------------:");
                    foreach (Transaction t in transactionsByType)
                    {
                        Console.WriteLine($"{t.Id}. {t.Title}");
                        Console.WriteLine("------------------------:");
                    }
                    Console.WriteLine("");
                    break;
                case 2:
                    Console.WriteLine("Select category: ");
                    ShowTransactionCategories();
                    Console.WriteLine("");
                    Console.Write("Select: ");
                    if (!int.TryParse(Console.ReadLine(), out int inputCategory))
                    {
                        Console.WriteLine("Number is invalid");
                        return;
                    }
                    if(inputCategory < 0 || inputCategory > 8) return;
                    List<Transaction> transactionsByCategory = _transactionService.GetByCategorty((TransactionCategory)inputCategory);
                    Console.Clear();
                    if(transactionsByCategory.Count == 0)
                    {
                        Console.WriteLine("No transaction found with this category");
                        return;
                    }
                    Console.Write("Results: \n");
                    Console.WriteLine("------------------------:");
                    foreach (Transaction t in transactionsByCategory)
                    {
                        Console.WriteLine($"{t.Id}. {t.Title}");
                        Console.WriteLine("------------------------:");
                    }
                    break;
                case 3:
                    Console.Clear();
                    DateTime startDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
                    DateTime now = DateTime.Now;
                    List<Transaction> thisMonthTransactions = _transactionService.GetByDateRange(startDate, now);
                    Console.Write("This month transactions: \n");
                    Console.WriteLine("------------------------:");
                    foreach (Transaction t in thisMonthTransactions)
                    {
                        Console.WriteLine($"{t.Id}. {t.Title}");
                        Console.WriteLine("------------------------:");
                    }
                    break;
                case 4:
                    Console.Clear();
                    Console.Write("From date (yyyy-mm-dd): ");
                    string? inputFromDate = Console.ReadLine();
                    if (string.IsNullOrEmpty(inputFromDate))
                    {
                        Console.WriteLine("Date is Invalid.");
                        return;
                    }
                    Console.Write("To date (yyyy-mm-dd): ");
                    string? inputToDate = Console.ReadLine();
                    if (string.IsNullOrEmpty(inputToDate))
                    {
                        Console.WriteLine("Date is Invalid.");
                        return;
                    }
                    if (!DateTime.TryParseExact(inputFromDate, "yyyy-MM-dd",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime fromDateResult))
                    {
                        Console.WriteLine("Date is Invalid.");
                        return;
                    }
                    if (!DateTime.TryParseExact(inputToDate, "yyyy-MM-dd",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime toDateResult))
                    {
                        Console.WriteLine("Date is Invalid.");
                        return;
                    }
                    if (fromDateResult >= toDateResult)
                    {
                        Console.WriteLine("Start date is greater than enddate.");
                        return;
                    }
                    List<Transaction> fromDateToDateTransactions = _transactionService.GetByDateRange(fromDateResult, toDateResult);
                    Console.Clear();
                    Console.WriteLine($"transactions from {fromDateResult} to {toDateResult}: ");
                    Console.WriteLine("");
                    Console.WriteLine("------------------------:");
                    foreach (Transaction t in fromDateToDateTransactions)
                    {
                        Console.WriteLine($"{t.Id}. {t.Title}");
                        Console.WriteLine("------------------------:");
                    }
                    Console.WriteLine("");
                    break;
                case 5:
                    Console.Write("Expense transactions more than: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal inputAmount))
                    {
                        Console.Write("Amount is invalid");
                        return;
                    }
                    List<Transaction> transactionsGreaterThanInputAmount = _transactionService.GetExpensesGreaterThan(inputAmount);
                    Console.Clear();
                    Console.Write($"Expense transactions more than ${inputAmount}: ");
                    Console.WriteLine("");
                    Console.WriteLine("------------------------:");
                    foreach (Transaction t in transactionsGreaterThanInputAmount)
                    {
                        Console.WriteLine($"{t.Id}. {t.Title} - ${t.Amount}");
                        Console.WriteLine("------------------------:");
                    }
                    Console.WriteLine("");
                    break;
                case 0:
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Number is invalid");
                    break;
            }
        }

        private void SearchTransaction()
        {
            Console.Clear();
            Console.Write("Search Transaction (Title - Category): ");
            string? searchInput = Console.ReadLine();
            if(searchInput == null)
            {
                Console.WriteLine("Transaction or category doosn't exist.");
                return;
            }
            List<Transaction> searchResults = _transactionService.Search(searchInput);
            Console.WriteLine("Results: \n");
            Console.WriteLine("-----------------------------:");
            foreach (Transaction transaction in searchResults)
            {
                Console.WriteLine($"{transaction.Id}. {transaction.Title} - {transaction.Category}");
                Console.WriteLine("-----------------------------:");
            }
            Console.WriteLine("");
        }

        private void ShowMainUi()
        {
            Console.WriteLine("======= Expense Tracker =======");
            Console.WriteLine("");
            Console.WriteLine("1. Show All Transactions");
            Console.WriteLine("2. Add Transaction");
            Console.WriteLine("3. Edit Transaction");
            Console.WriteLine("4. Delete Transaction");
            Console.WriteLine("5. Search Transaction");
            Console.WriteLine("6. Filter Transaction");
            Console.WriteLine("7. Financial Report");
            Console.WriteLine("8. Monthly Report");
            Console.WriteLine("");
            Console.WriteLine("0. Exit");
            Console.WriteLine("");
            Console.Write("Select: ");

        }

        public void Exit()
        {
            System.Environment.Exit(0);
        }

        private void AddTransaction()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter Transaction Title: ");
                string? transactionTitle = Console.ReadLine();

                if (string.IsNullOrEmpty(transactionTitle))
                {
                    Console.WriteLine("Title is invalid");
                    continue;
                }

                Console.Write("Enter Transaction Amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal transactionAmount))
                {
                    Console.WriteLine("Amount is invalid");
                    continue;
                }

                ShowTransactionTypes();
                Console.Write("Select Transaction Type: ");
                if (!int.TryParse(Console.ReadLine(), out int transactionType) &&
                    !Enum.IsDefined(typeof(TransactionType), transactionType))
                {
                    Console.WriteLine("Invalid category.");
                    continue;
                }

                ShowTransactionCategories();
                Console.Write("Select Transaction Category: ");
                if (!int.TryParse(Console.ReadLine(), out int transactionCategory) &&
                    !Enum.IsDefined(typeof(TransactionCategory), transactionCategory))
                {
                    Console.WriteLine("Invalid category.");
                    continue;
                }

                Console.Write("Enter a Description: ");
                string? transactionDescription = Console.ReadLine();

                if (transactionDescription == null)
                {
                    Console.WriteLine("Description is invalid");
                    continue;
                }

                List<Transaction> transactions = _transactionService.GetAll();
                int newId = transactions.Any() ? transactions.Max(t => t.Id) + 1 : 1;

                _transactionService.AddTransaction(new Models.Transaction
                {
                    Id = newId,
                    Title = transactionTitle,
                    Amount = transactionAmount,
                    Type = (TransactionType)transactionType,
                    Category = (TransactionCategory)transactionCategory,
                    Date = DateTime.Now,
                    Description = transactionDescription
                });
                Console.Clear();
                Console.WriteLine($"Transaction \"{transactionTitle}\" Added successfuly!");
                return;
            }
        }

        public void EditTransaction()
        {
            ListTransactions();
            Console.Write("Please enter number next to task you want to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int inputId))
            {
                Console.Write("Number is invalid");
                return;
            }
            Transaction? transaction = _transactionService.GetById(inputId);
            if (transaction == null) {
                Console.Write("Item dosen't exist.");
                return;
            }

            Console.Write("Enter Transaction Title: ");
            string? inputTitle = Console.ReadLine();

            if (string.IsNullOrEmpty(inputTitle))
            {
                Console.WriteLine("Title is invalid");
                return;
            }

            Console.Write("Enter Transaction Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal inputAmount))
            {
                Console.WriteLine("Amount is invalid");
                return;
            }

            ShowTransactionTypes();
            Console.Write("Select Transaction Type: ");
            if (!int.TryParse(Console.ReadLine(), out int inputType) &&
                !Enum.IsDefined(typeof(TransactionType), inputType))
            {
                Console.WriteLine("Invalid category.");
                return;
            }

            ShowTransactionCategories();
            Console.Write("Select Transaction Category: ");
            if (!int.TryParse(Console.ReadLine(), out int inputCategory) &&
                !Enum.IsDefined(typeof(TransactionCategory), inputCategory))
            {
                Console.WriteLine("Invalid category.");
                return;
            }

            Console.WriteLine("Enter transaction date: (yyyy-MM-dd)");
            string? inputDate = Console.ReadLine();
            if (inputDate == null)
            {
                Console.WriteLine("Date is invalid");
                throw new KeyNotFoundException("Date is invalid");
            }
            if (!DateTime.TryParseExact(inputDate, "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime dateResult))
            {
                Console.WriteLine("Date is Invalid.");
                return;
            }

            Console.Write("Enter a Description: ");
            string? inputDescription = Console.ReadLine();

            if (inputDescription == null)
            {
                Console.WriteLine("Description is invalid");
                throw new KeyNotFoundException("Description is invalid");
            }
            bool isEdited = _transactionService.UpdateTransaction(inputId, inputTitle, (TransactionType)inputType, (TransactionCategory)inputCategory, dateResult, inputDescription);
            Console.Clear();
            if (isEdited)
            {
                Console.Write($"Transaction \"{transaction}\" edited successfuly.");
            }
            else
            {
                Console.WriteLine($"Failed while editing \"{transaction}\", Please try again.");
            }
        }

        public void DeleteTransaction()
        {
            ListTransactions();
            Console.Write("Please enter number next to task you want to delete: ");
            if(!int.TryParse(Console.ReadLine(), out int inputId)){
                Console.WriteLine("Number is invalid");
                return;
            }
            Transaction? transaction = _transactionService.GetById(inputId);
            if( transaction == null)
            {
                Console.WriteLine("Transaction is invalid or doesn't exist.");
                return;
            }
            Console.Clear();
            Console.WriteLine($"Are you sure you want to delete transaction \"{transaction.Title}\"? ");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            Console.WriteLine("");
            Console.Write("Select: ");

            if (!int.TryParse(Console.ReadLine(), out int inputConfirm))
            {
                Console.WriteLine("Number is invalid");
                return;
            }

            if (inputConfirm == 0 || inputConfirm >= 2) return;

            bool isDeleted = _transactionService.DeleteTransaction(inputId);
            if (isDeleted)
            {
                Console.WriteLine($"Transaction \"{transaction.Title}\" deleted successfuly.");
            } else
            {
                Console.WriteLine($"Failed while deleting \"{transaction.Title}\", Please try again.");
            }
        }

        public void ListTransactions()
        {
            Console.Clear();
            Console.WriteLine("All Transactions List:"); 
            List<Transaction> transactionsList = _transactionService.GetAll();
            foreach(Transaction t in transactionsList)
            {
                Console.WriteLine($"{t.Id}. {t.Title}");
            }
        }

        public void FinancialReport()
        {
            Console.Clear();
            List<Transaction> transactions = _transactionService.GetAll();
            decimal totalIncome = _transactionReport.GetTotalIncome(transactions);
            decimal totalExpenses = _transactionReport.GetTotalExpenses(transactions);
            decimal balance = _transactionReport.GetBalance(transactions);
            Console.WriteLine($"Total Income: ${totalIncome}");
            Console.WriteLine($"Total Expenses: ${totalExpenses}");
            Console.WriteLine($"Balance: ${balance}");
            Console.Write("\n=========================\n");
            Console.WriteLine("Total Expenses by Category: ");
            Console.WriteLine("│");
            List<KeyValuePair<TransactionCategory, decimal>> items = _transactionReport.GetExpensesByCategory(transactions).ToList();
            int index = 0;
            int itemsCount = items.Count;

            foreach (var item in items)
            {
                if (index == itemsCount - 1)
                    Console.WriteLine($"└── {item.Key} {item.Value}");
                else
                    Console.WriteLine($"├── {item.Key} {item.Value}");
                index++;
            }

            Console.WriteLine("");
        }

        private void MonthlyReport(){
            Console.Clear();
            List<Transaction> transactions = _transactionService.GetAll();
            while(true)
            {
                Console.Write("Please enter Year: ");
                if (!int.TryParse(Console.ReadLine(), out int inputYear))
                {
                    return;
                }
                if (inputYear < 1000 || inputYear > 9999)
                {
                    return;
                }
                Console.Write("Please enter Month: ");
                if (!int.TryParse(Console.ReadLine(), out int inputMonth)) return;
                List<Transaction> monthlyTransactions = _transactionReport.GetMonthlyTransaction(transactions, inputYear, inputMonth);
                if(monthlyTransactions.Count == 0)
                {
                    Console.WriteLine("\nNo Transaction found or input is invalid.\n");
                    return;
                }
                Console.Write("\n=========================\n");
                Console.WriteLine("Monthly Transactions: ");
                foreach (var item in monthlyTransactions)
                {
                    TimeSpan diff = DateTime.Now - item.Date;
                    String days = diff.Days == 0 ? "" : $"{diff.Days} days and ";
                    Console.WriteLine($"{item.Title} - ${item.Amount} - ({item.Type}) | {days}{(int)diff.Hours}:{(int)diff.Minutes}:{(int)diff.Seconds} ago");
                }
                Console.Write("\n");
                return;
            }
        }

        private void ShowAllTransactions()
        {
            Console.Clear();
            List<Transaction> transactions = _transactionService.GetAll();
            Console.WriteLine("All Transactions List:");
            Console.WriteLine("--------------------------------------------:");
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine("-------------------:");
                Console.WriteLine($"Id: {transaction.Id}");
                Console.WriteLine($"Title: {transaction.Title}");
                Console.WriteLine($"Amount: {transaction.Amount}");
                Console.WriteLine($"Type: {transaction.Type}");
                Console.WriteLine($"Category: {transaction.Category}");
                Console.WriteLine($"Date: {transaction.Date}");
                Console.WriteLine($"Description: {transaction.Description}");
                Console.WriteLine("-------------------:");
            }
            Console.WriteLine("");
        }

        private void ShowTransactionTypes()
        {
            foreach (TransactionType transactionType in Enum.GetValues<TransactionType>())
            {
                Console.WriteLine($"{(int)transactionType}. {transactionType}");
            }
        }

        private void ShowTransactionCategories()
        {
            foreach (TransactionCategory transactionCategory in Enum.GetValues<TransactionCategory>())
            {
                Console.WriteLine($"{(int)transactionCategory}. {transactionCategory}");
            }
        }
    }
}