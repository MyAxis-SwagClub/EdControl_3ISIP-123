using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DepositCalculator17.Tests
{
    /// <summary>
    /// Автоматизированные (модульные) тесты для калькулятора вклада — Вариант №17
    /// </summary>
    public class DepositCalcTests
    {
        private readonly DepositCalc _calc = new DepositCalc();

        [Fact]
        public void CompoundInterest_StandardCase_ReturnsCorrectValues()
        {
            var result = _calc.Calculate(100000, 6, 12, true);
            Assert.Equal(106152.02, result.total);
            Assert.Equal(6152.02, result.income);
        }

        [Fact]
        public void SimpleInterest_StandardCase_ReturnsCorrectValues()
        {
            var result = _calc.Calculate(100000, 12, 12, false);
            Assert.Equal(112000.00, result.total);
            Assert.Equal(12000.00, result.income);
        }

        [Fact]
        public void OneMonth_CompoundInterest_ReturnsCorrectValue()
        {
            var result = _calc.Calculate(50000, 1, 18, true);
            Assert.Equal(50750.00, result.total);
            Assert.Equal(750.00, result.income);
        }

        [Fact]
        public void OneMonth_SimpleInterest_ReturnsCorrectValue()
        {
            var result = _calc.Calculate(100000, 1, 12, false);
            Assert.Equal(101000.00, result.total);
            Assert.Equal(1000.00, result.income);
        }

        [Fact]
        public void FiveYears_CompoundInterest_ReturnsCorrectValue()
        {
            var result = _calc.Calculate(200000, 60, 10, true);
            Assert.Equal(329061.79, result.total);
            Assert.Equal(129061.79, result.income);
        }

        [Theory]
        [InlineData(100000, 24, 8, true)]
        [InlineData(50000, 36, 15, false)]
        public void DifferentInputs_ReturnsCorrectResults(double principal, int months, double rate, bool isCompound)
        {
            var result = _calc.Calculate(principal, months, rate, isCompound);
            Assert.True(result.total > principal, "Итоговая сумма должна быть больше начальной");
            Assert.True(result.income >= 0, "Доход не может быть отрицательным");
        }

        [Fact]
        public void NegativePrincipal_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _calc.Calculate(-10000, 6, 12, true));
        }

        [Fact]
        public void ZeroMonths_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _calc.Calculate(100000, 0, 12, true));
        }

        [Fact]
        public void NegativeRate_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _calc.Calculate(100000, 6, -5, true));
        }

        [Fact]
        public void ZeroPrincipal_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _calc.Calculate(0, 12, 10, true));
        }
    }
}