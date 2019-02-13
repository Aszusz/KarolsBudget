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

            _budget.AddExpense("Wydatek-1A", new DateTime(2019, 02, 1), 30);
            _budget.AddExpense("Wydatek-1B", new DateTime(2019, 02, 1), 40);
            _budget.AddExpense("Wydatek-2A", today, 145);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(30));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(100));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(300));
        }

        [Test]
        public void TestDay3()
        {
            var today = new DateTime(2019, 02, 3);
            _dateProvider.SetToday(today);

            _budget.AddExpense("Wydatek-1A", new DateTime(2019, 02, 1), 30);
            _budget.AddExpense("Wydatek-1B", new DateTime(2019, 02, 1), 40);
            _budget.AddExpense("Wydatek-2A", new DateTime(2019, 02, 2), 145);
            _budget.AddExpense("Wydatek-3A", today, 30);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(95));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(190));
        }

        [Test]
        public void TestDay4()
        {
            var today = new DateTime(2019, 02, 4);
            _dateProvider.SetToday(today);

            _budget.AddExpense("Wydatek-1A", new DateTime(2019, 02, 1), 30);
            _budget.AddExpense("Wydatek-1B", new DateTime(2019, 02, 1), 40);
            _budget.AddExpense("Wydatek-2A", new DateTime(2019, 02, 2), 145);
            _budget.AddExpense("Wydatek-3A", new DateTime(2019, 02, 2), 30);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(55));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(100));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(100));
        }

        [Test]
        public void TestDay5()
        {
            var today = new DateTime(2019, 02, 5);
            _dateProvider.SetToday(today);

            _budget.AddExpense("Wydatek-1A", new DateTime(2019, 02, 1), 30);
            _budget.AddExpense("Wydatek-1B", new DateTime(2019, 02, 1), 40);
            _budget.AddExpense("Wydatek-2A", new DateTime(2019, 02, 2), 145);
            _budget.AddExpense("Wydatek-3A", new DateTime(2019, 02, 2), 30);
            _budget.AddExpense("Wydatek-5A", today, 27);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(155));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(100));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(0));
        }

        [Test]
        public void TestPost()
        {
            var today = new DateTime(2019, 02, 6);
            _dateProvider.SetToday(today);

            _budget.AddExpense("Wydatek-1A", new DateTime(2019, 02, 1), 30);
            _budget.AddExpense("Wydatek-1B", new DateTime(2019, 02, 1), 40);
            _budget.AddExpense("Wydatek-2A", new DateTime(2019, 02, 2), 145);
            _budget.AddExpense("Wydatek-3A", new DateTime(2019, 02, 2), 30);
            _budget.AddExpense("Wydatek-5A", new DateTime(2019, 02, 5), 27);

            Assert.That(_budgetService.CalculateSavings(_budget), Is.EqualTo(228));
            Assert.That(_budgetService.CalculateTodaysAllowance(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.CalculateRemainingBudget(_budget), Is.EqualTo(0));
        }
    }
}
