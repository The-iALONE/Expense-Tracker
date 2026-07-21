using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Storage
{
    public interface IStorage<T>
    {
        List<T> Load();
        void Save(List<T> items);
    }
}
