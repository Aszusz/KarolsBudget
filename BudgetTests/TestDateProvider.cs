using System;
using KarolsBudget;

namespace BudgetTests
{
    public class TestDateProvider : IDateProvider
    {
        private DateTime _today;

        public void SetToday(DateTime date)
        {
            _today = date;
        }

        public DateTime Today()
        {
            return _today.Date;
        }

        public DateTime Yesterday()
        {
            return _today.Date - TimeSpan.FromDays(1);
        }
    }
}