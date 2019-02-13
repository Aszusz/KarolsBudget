using System;
using KarolsBudget;
using NUnit.Framework;

namespace BudgetTests
{
    [TestFixture]
    public class TestBudgetService
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
                new DateTime(2019, 02, 5),
                500.00);
        }

        [Test]
        public void TestPre()
        {
            _dateProvider.SetToday(new DateTime(2019, 01, 31));

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(500));
        }

        [Test]
        public void TestDay1()
        {
            var today = new DateTime(2019, 02, 1);
            _dateProvider.SetToday(today);

            _budget.AddExpense("Wydatek-1A", today, 30);
            _budget.AddExpense("Wydatek-1B", today, 40);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(100));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(400));
        }

        [Test]
        public void TestDay2()
        {
            var today = new DateTime(2019, 02, 2);
            _dateProvider.SetToday(today);

            _budget.AddExpense("Wydatek-2A", today, 50);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(30));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(100));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(300));
        }
    }
}
