using System;

namespace KarolsBudget
{
    public interface IDateProvider
    {
        DateTime Today();
        DateTime Yesterday();
    }
}