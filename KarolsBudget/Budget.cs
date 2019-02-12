using System;
using System.Collections.Generic;

namespace KarolsBudget
{
    public class Budget
    {
        public Budget(DateTime start, DateTime end, double amount)
        {
            Start = start.Date;
            End = end.Date;
            Amount = amount;
            Expenses = new List<Expense>();
        }

        public DateTime Start { get; }

        public DateTime End { get; }

        public double Amount { get; }

        public List<Expense> Expenses { get; }

        public void AddExpense(string label, DateTime date, double amount)
        {
            Expenses.Add(new Expense(label, date, amount));
        }
    }
}