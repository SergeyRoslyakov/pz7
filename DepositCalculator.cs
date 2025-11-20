using System;

namespace pz7
{
    public class DepositCalculator
    {
        private const decimal MIN_DEPOSIT_AMOUNT = 100;
        private const decimal MAX_DEPOSIT_AMOUNT = 10000000;
        private const int MIN_DEPOSIT_TERM = 1;
        private const int MAX_DEPOSIT_TERM = 60;
        private const decimal MIN_DEPOSIT_RATE = 0.1m;
        private const decimal MAX_DEPOSIT_RATE = 99.9m;

        public void CalculateDeposit()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== КАЛЬКУЛЯТОР ВКЛАДОВ ===");

                decimal depositAmount = GetValidatedDecimalInput("Сумма вклада (руб): ", MIN_DEPOSIT_AMOUNT, MAX_DEPOSIT_AMOUNT);
                int depositTerm = GetValidatedIntInput("Срок вклада (месяцев): ", MIN_DEPOSIT_TERM, MAX_DEPOSIT_TERM);
                decimal interestRate = GetValidatedDecimalInput("Процентная ставка (% годовых): ", MIN_DEPOSIT_RATE, MAX_DEPOSIT_RATE);

                bool withCapitalization = GetCapitalizationChoice();

                var result = CalculateDepositResult(depositAmount, depositTerm, interestRate, withCapitalization);

                Console.WriteLine("\n=== РЕЗУЛЬТАТЫ РАСЧЕТА ===");
                Console.WriteLine($"Доход по вкладу: {result.Income:F2} руб");
                Console.WriteLine($"Итоговая сумма: {result.FinalAmount:F2} руб");
                Console.WriteLine($"Тип вклада: {(withCapitalization ? "с капитализацией" : "без капитализации")}");
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

        private (decimal Income, decimal FinalAmount) CalculateDepositResult(
            decimal amount, int term, decimal rate, bool withCapitalization)
        {
            if (withCapitalization)
            {
                decimal monthlyRate = rate / 100 / 12;
                decimal finalAmount = amount * (decimal)Math.Pow((double)(1 + monthlyRate), term);
                decimal income = finalAmount - amount;

                return (Math.Round(income, 2), Math.Round(finalAmount, 2));
            }
            else
            {
                decimal income = amount * rate * term / 12 / 100;
                decimal finalAmount = amount + income;

                return (Math.Round(income, 2), Math.Round(finalAmount, 2));
            }
        }

        private bool GetCapitalizationChoice()
        {
            while (true)
            {
                Console.Write("Тип вклада (1 - с капитализацией, 2 - без капитализации): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        return true;
                    case "2":
                        return false;
                    default:
                        Console.WriteLine("Ошибка: введите 1 или 2");
                        break;
                }
            }
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