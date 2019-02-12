using System.Collections.Generic;
using System.Linq;

namespace KarolsBudget
{
    public class BudgetService
    {
        private readonly IDateProvider _dateProvider;

        public BudgetService(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public int GetPastDays(Budget budget)
        {
            return _dateProvider.Today() > budget.Start
                ? (int) (_dateProvider.Today() - budget.Start).TotalDays
                : 0;
        }

        public int GetPresentDays(Budget budget)
        {
            return _dateProvider.Today() >= budget.Start && _dateProvider.Today() <= budget.End ? 1 : 0;
        }

        public int GetFutureDays(Budget budget)
        {
            return _dateProvider.Today() < budget.End
                ? (int) (budget.End - _dateProvider.Today()).TotalDays
                : 0;
        }

        public int GetTotalDays(Budget budget)
        {
            return (int) (budget.End - budget.Start).TotalDays + 1;
        }

        public IEnumerable<Expense> GetPastExpenses(Budget budget)
        {
            return budget.Expenses.Where(expense => expense.Date < _dateProvider.Today());
        }

        public IEnumerable<Expense> GetTodaysExpenses(Budget budget)
        {
            return budget.Expenses.Where(expense => expense.Date == _dateProvider.Today());
        }

        public double SumPastExpenses(Budget budget)
        {
            return GetPastExpenses(budget).Sum(expense => expense.Amount);
        }

        public double SumTodaysExpenses(Budget budget)
        {
            return GetTodaysExpenses(budget).Sum(expense => expense.Amount);
        }

        private double CalculateBalance(Budget budget)
        {
            var howMuchIWasAllowedSpend = GetPastDays(budget) * budget.Amount / GetTotalDays(budget);
            var howMuchIDidActuallySpend = SumPastExpenses(budget);
            return howMuchIWasAllowedSpend - howMuchIDidActuallySpend;
        }

        public double CalculateSavings(Budget budget)
        {
            var balance = CalculateBalance(budget);
            return balance > 0 ? balance : 0;
        }

        public double CalculateTodaysAllowance(Budget budget)
        {
            if (GetPresentDays(budget) == 0) return 0.0;

            var howMuchMoneyShouldBeLeftToday =
                (GetFutureDays(budget) + GetPresentDays(budget))
                *budget.Amount/GetTotalDays(budget);
            var balance = CalculateBalance(budget);
            var howMuchMoneyIsActuallyLeftToday = balance < 0
                ? howMuchMoneyShouldBeLeftToday + balance
                : howMuchMoneyShouldBeLeftToday;

            return howMuchMoneyIsActuallyLeftToday/(GetPresentDays(budget) + GetFutureDays(budget));
        }

        public double CalculateRemainingBudget(Budget budget)
        {
            return budget.Amount 
                - SumPastExpenses(budget) 
                - CalculateSavings(budget) 
                - CalculateTodaysAllowance(budget);
        }
    }
}