using System;

namespace pz7
{
    public class CreditCalculator
    {
        private const decimal MIN_LOAN_AMOUNT = 1000;
        private const decimal MAX_LOAN_AMOUNT = 10000000;
        private const int MIN_LOAN_TERM = 1;
        private const int MAX_LOAN_TERM = 360;
        private const decimal MIN_INTEREST_RATE = 0.1m;
        private const decimal MAX_INTEREST_RATE = 99.9m;

        public void CalculateCredit()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== КРЕДИТНЫЙ КАЛЬКУЛЯТОР ===");

                decimal loanAmount = GetValidatedDecimalInput("Сумма кредита (руб): ", MIN_LOAN_AMOUNT, MAX_LOAN_AMOUNT);
                int loanTerm = GetValidatedIntInput("Срок кредита (месяцев): ", MIN_LOAN_TERM, MAX_LOAN_TERM);
                decimal interestRate = GetValidatedDecimalInput("Процентная ставка (% годовых): ", MIN_INTEREST_RATE, MAX_INTEREST_RATE);

                var result = CalculateAnnuityPayment(loanAmount, loanTerm, interestRate);

                Console.WriteLine("\n=== РЕЗУЛЬТАТЫ РАСЧЕТА ===");
                Console.WriteLine($"Ежемесячный платеж: {result.MonthlyPayment:F2} руб");
                Console.WriteLine($"Общая сумма выплат: {result.TotalPayment:F2} руб");
                Console.WriteLine($"Переплата по кредиту: {result.Overpayment:F2} руб");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
            }
        }

        private (decimal MonthlyPayment, decimal TotalPayment, decimal Overpayment) CalculateAnnuityPayment(
            decimal loanAmount, int loanTerm, decimal interestRate)
        {
            decimal monthlyRate = interestRate / 100 / 12;

            decimal coefficient = monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), loanTerm) /
                                (decimal)(Math.Pow((double)(1 + monthlyRate), loanTerm) - 1);

            decimal monthlyPayment = loanAmount * coefficient;
            decimal totalPayment = monthlyPayment * loanTerm;
            decimal overpayment = totalPayment - loanAmount;

            return (
                Math.Round(monthlyPayment, 2),
                Math.Round(totalPayment, 2),
                Math.Round(overpayment, 2)
            );
        }

        private decimal GetValidatedDecimalInput(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: значение должно быть от {min} до {max}");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное число");
                }
            }
        }

        private int GetValidatedIntInput(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: значение должно быть от {min} до {max}");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите целое число");
                }
            }
        }
    }
}