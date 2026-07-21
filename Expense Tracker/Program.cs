using Expense_Tracker.Enums;
using Expense_Tracker.Services;
using Expense_Tracker.Storage;
using Expense_Tracker.Ui;
using Expense_Tracker.Models;

IStorage<Transaction> storage = new JsonStorage<Transaction>();
List<Transaction> transactions = storage.Load();
TransactionService service = new TransactionService(transactions, storage);
TransactionReport reportService = new TransactionReport();
ConsoleUi app = new ConsoleUi(service, reportService);

app.Run();