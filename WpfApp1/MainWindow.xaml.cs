using System;
using System.Windows;

namespace DepositCalculator17
{
    /// <summary>
    /// Главное окно приложения - Калькулятор вклада (вариант №17)
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Класс для расчёта дохода по вкладу (вариант №17)
        /// Выделен отдельно для удобства модульного тестирования
        /// </summary>
        public class DepositCalc
        {
            /// <summary>
            /// Основной метод расчёта вклада
            /// </summary>
            /// <param name="principal">Начальная сумма</param>
            /// <param name="months">Срок в месяцах</param>
            /// <param name="annualRate">Годовая ставка (%)</param>
            /// <param name="isCompound">true = сложные проценты, false = простые</param>
            /// <returns>(итоговая сумма, доход)</returns>
            public (double total, double income) Calculate(double principal, int months, double annualRate, bool isCompound)
            {
                if (principal <= 0 || months < 1 || annualRate < 0)
                    throw new ArgumentException("Все значения должны быть положительными. Срок — минимум 1 месяц.");

                double total, income;

                if (isCompound)
                {
                    // Сложные проценты с ежемесячной капитализацией
                    total = principal * Math.Pow(1 + annualRate / 100 / 12, months);
                    income = total - principal;
                }
                else
                {
                    // Простые проценты
                    income = principal * (annualRate / 100) * (months / 12.0);
                    total = principal + income;
                }

                return (Math.Round(total, 2), Math.Round(income, 2));
            }
        }
    



    /// <summary>
    /// Обработчик нажатия кнопки "РАССЧИТАТЬ"
    /// Выполняет валидацию и расчёт вклада по выбранной схеме
    /// </summary>
    private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double sum = ParseDouble(txtSum.Text);
                int months = int.Parse(txtMonths.Text);
                double rate = ParseDouble(txtRate.Text);

                // Валидация входных данных
                if (sum <= 0 || months < 1 || rate < 0)
                {
                    WarningMessage_valid("Обратите внимание на вводимые данные!");
                    return;
                }

                double total, income;

                if (rbCompound.IsChecked == true)
                {
                    // Сложные проценты с ежемесячной капитализацией
                    total = sum * Math.Pow(1 + rate / 100 / 12, months);
                    income = total - sum;
                }
                else
                {
                    // Простые проценты (без капитализации)
                    income = sum * (rate / 100) * (months / 12.0);
                    total = sum + income;
                }

                txtResult.Text = ("Итоговая сумма: " + total.ToString("F2") + " руб.\n" +
                                 "Начисленные проценты: " + income.ToString("F2") + " руб.\n\n");
            }
            catch
            {
                WarningMessage_valid("Обратите внимание на вводимые данные!");
            }
        }

        /// <summary>
        /// Функция для отображанеия окна отображения ошибка
        /// Параметр message несет в себе текст, который будет отображаться
        /// </summary>

        private void WarningMessage_valid(string message)
        {
            MessageBox.Show(message,
                                "Ошибка!", MessageBoxButton.OK);
        }


        /// <summary>
        /// Универсальный метод парсинга числа (поддерживает точку и запятую)
        /// </summary>
        /// <param name="input">Строка с числом</param>
        /// <returns>double значение</returns>
        private double ParseDouble(string input)
        {
            input = input.Replace(".", ",");
            return double.Parse(input);
        }


    }
}