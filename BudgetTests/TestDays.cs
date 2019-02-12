using System;
using KarolsBudget;
using NUnit.Framework;

namespace BudgetTests
{
    [TestFixture]
    public class TestDays
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
        }

        [Test]
        public void TestDaysPre()
        {
            _dateProvider.SetToday(new DateTime(2019, 01, 31));

            Assert.That(_budgetService.GetNumberOfPastDays(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.GetNumberOfRemainingDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.GetNumberOfTotalDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.IsTodayInBudgetScope(_budget), Is.EqualTo(false));
        }

        [Test]
        public void TestDaysStart()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 01));

            Assert.That(_budgetService.GetNumberOfPastDays(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.GetNumberOfRemainingDays(_budget), Is.EqualTo(2));
            Assert.That(_budgetService.GetNumberOfTotalDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.IsTodayInBudgetScope(_budget), Is.EqualTo(true));
        }

        [Test]
        public void TestDaysMiddle()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 02));

            Assert.That(_budgetService.GetNumberOfPastDays(_budget), Is.EqualTo(1));
            Assert.That(_budgetService.GetNumberOfRemainingDays(_budget), Is.EqualTo(1));
            Assert.That(_budgetService.GetNumberOfTotalDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.IsTodayInBudgetScope(_budget), Is.EqualTo(true));
        }

        [Test]
        public void TestDaysEnd()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 03));

            Assert.That(_budgetService.GetNumberOfPastDays(_budget), Is.EqualTo(2));
            Assert.That(_budgetService.GetNumberOfRemainingDays(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.GetNumberOfTotalDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.IsTodayInBudgetScope(_budget), Is.EqualTo(true));
        }

        [Test]
        public void TestDaysPost()
        {
            _dateProvider.SetToday(new DateTime(2019, 02, 04));

            Assert.That(_budgetService.GetNumberOfPastDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.GetNumberOfRemainingDays(_budget), Is.EqualTo(0));
            Assert.That(_budgetService.GetNumberOfTotalDays(_budget), Is.EqualTo(3));
            Assert.That(_budgetService.IsTodayInBudgetScope(_budget), Is.EqualTo(false));
        }
    }
}
