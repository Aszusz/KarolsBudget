using System;

namespace KarolsBudget
{
    public class Expense
    {
        public Expense(string label, DateTime date, double amount)
        {
            Amount = amount;
            Date = date;
            Label = label;
        }

        public double Amount { get; }

        public DateTime Date { get; }

        public string Label { get; }
    }
}