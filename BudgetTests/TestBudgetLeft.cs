using System;
using KarolsBudget;
using NUnit.Framework;

namespace BudgetTests
{
    [TestFixture]
    public class TestBudgetLeft
    {
        private TestDateProvider _dateProvider;
        private Budget _budget;
        private BudgetService _budgetService;

        [SetUp]
        public void SetUp()
        {
            _dateProvider = new TestDateProvider();
            _budgetService = new BudgetService(_dateProvider);
            _budget = new Budget(
                new DateTime(2019, 02, 1), 
                new DateTime(2019, 02, 3), 
                300.00);

            _budget.AddExpense("Wydatek 1", new DateTime(2019, 02, 1), 50);
            _budget.AddExpense("Wydatek 2", new DateTime(2019, 02, 2), 200);
            _budget.AddExpense("Wydatek 3", new DateTime(2019, 02, 3), 10);
        }

        [Test]
        public void TestBudgetPre()
        {
            _dateProvider.SetToday(new DateTime(2019, 01, 31));

            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(300));
        }

        [Test]
        public void TestBudgetStart()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 01));

            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(200));
        }

        [Test]
        public void TestBudgetMiddle()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 02));

            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(100));
        }

        [Test]
        public void TestBudgetEnd()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 03));

            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(0));
        }

        [Test]
        public void TestBudgetPost()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 04));

            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(0));
        }
    }
}
