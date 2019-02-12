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

        public int GetNumberOfPastDays(Budget budget)
        {
            return _dateProvider.Today() > budget.Start
                ? (int) (_dateProvider.Today() - budget.Start).TotalDays
                : 0;
        }

        public int GetNumberOfRemainingDays(Budget budget)
        {
            return _dateProvider.Today() < budget.End
                ? (int) (budget.End - _dateProvider.Today()).TotalDays
                : 0;
        }

        public int GetNumberOfTotalDays(Budget budget)
        {
            return (int) (budget.End - budget.Start).TotalDays + 1;
        }

        public bool IsTodayInBudgetScope(Budget budget)
        {
            return _dateProvider.Today() >= budget.Start &&
                   _dateProvider.Today() <= budget.End;
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

        public double CalculateSavings(Budget budget)
        {
            var howMuchIWasAllowedToSpend = GetNumberOfPastDays(budget) * budget.Amount / GetNumberOfTotalDays(budget);
            var howMuchIDidActuallySpend = SumPastExpenses(budget);
            var balance = howMuchIWasAllowedToSpend - howMuchIDidActuallySpend;
            return balance > 0 ? balance : 0;
        }

        public double CalculateCurrentBudget(Budget budget)
        {
            return budget.Amount - CalculateSavings(budget) - SumPastExpenses(budget);
        }

        public double CalculateTodaysAllowance(Budget budget)
        {
            if (!IsTodayInBudgetScope(budget)) return 0.0;

            return CalculateCurrentBudget(budget)/(GetNumberOfRemainingDays(budget) + 1);
        }

        public double CalculateRemainingBudget(Budget budget)
        {
            return CalculateCurrentBudget(budget) - CalculateTodaysAllowance(budget);
        }
    }
}