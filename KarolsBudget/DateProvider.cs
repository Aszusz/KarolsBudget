using System;

namespace KarolsBudget
{
    public class DateProvider : IDateProvider
    {
        public DateTime Today()
        {
            return DateTime.Now.Date;
        }

        public DateTime Yesterday()
        {
            return DateTime.Now.Date - TimeSpan.FromDays(1);
        }
    }
}