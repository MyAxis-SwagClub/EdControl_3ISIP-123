using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static DepositCalculator17.MainWindow;

namespace DepositCalculator17.Tests
{
    /// <summary>
    /// Автоматизированные модульные тесты (MSTest) для калькулятора вклада — Вариант #17
    /// Соответствуют ручным тестовым сценариям #1–#7
    /// </summary>
    [TestClass]
    public class DepositCalcTests
    {
        private readonly DepositCalc _calc = new DepositCalc();

        /// <summary>
        /// Тест #1 (соответствует ручному тестовому примеру #1)
        /// Проверка расчёта сложных процентов (стандартный кейс)
        /// </summary>
        [TestMethod]
        public void CompoundInterest_StandardCase_ReturnsCorrectValues()
        {
            var result = _calc.Calculate(100000, 6, 12, true);
            Assert.AreEqual(106152.02, result.total);
            Assert.AreEqual(6152.02, result.income);
        }

        /// <summary>
        /// Тест #2 (соответствует ручному тестовому примеру #2)
        /// Проверка расчёта простых процентов (стандартный кейс)
        /// </summary>
        [TestMethod]
        public void SimpleInterest_StandardCase_ReturnsCorrectValues()
        {
            var result = _calc.Calculate(100000, 12, 12, false);
            Assert.AreEqual(112000.00, result.total);
            Assert.AreEqual(12000.00, result.income);
        }

        /// <summary>
        /// Тест #3 (соответствует ручному тестовому примеру #3)
        /// Граничный тест: минимальный срок 1 месяц + сложные проценты
        /// </summary>
        [TestMethod]
        public void OneMonth_CompoundInterest_ReturnsCorrectValue()
        {
            var result = _calc.Calculate(50000, 1, 18, true);
            Assert.AreEqual(50750.00, result.total);
            Assert.AreEqual(750.00, result.income);
        }

        /// <summary>
        /// Тест #4 (соответствует ручному тестовому примеру #6)
        /// Проверка простых процентов при минимальном сроке 1 месяц
        /// </summary>
        [TestMethod]
        public void OneMonth_SimpleInterest_ReturnsCorrectValue()
        {
            var result = _calc.Calculate(100000, 1, 12, false);
            Assert.AreEqual(101000.00, result.total);
            Assert.AreEqual(1000.00, result.income);
        }

        /// <summary>
        /// Тест #5 (соответствует ручному тестовому примеру #5)
        /// Проверка долгосрочного вклада (5 лет, сложные проценты)
        /// </summary>
        [TestMethod]
        public void FiveYears_CompoundInterest_ReturnsCorrectValue()
        {
            var result = _calc.Calculate(200000, 60, 10, true);
            Assert.AreEqual(329061.79, result.total);
            Assert.AreEqual(129061.79, result.income);
        }

        /// <summary>
        /// Тест #6 (соответствует ручному тестовому примеру #4)
        /// Проверка валидации: отрицательная сумма вклада
        /// </summary>
        [TestMethod]
        public void NegativePrincipal_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _calc.Calculate(-10000, 6, 12, true));
        }

        /// <summary>
        /// Тест #7 
        /// Проверка валидации: нулевой срок вклада
        /// </summary>

        [TestMethod]
        public void ZeroMonths_ThrowsArgumentException()
        {   
            Assert.ThrowsException<ArgumentException>(() => _calc.Calculate(100000, 0, 12, true));
        }

    }
}
