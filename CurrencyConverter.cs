using System;
using System.Collections.Generic;

namespace pz7
{
    public class CurrencyConverter
    {
        private readonly Dictionary<string, decimal> exchangeRates;

        public CurrencyConverter()
        {
            exchangeRates = new Dictionary<string, decimal>
            {
                {"USD_RUB", 80.6m},
                {"EUR_RUB", 93.7m},
                {"EUR_USD", 1.09m},
                {"RUB_USD", 1/80.6m},
                {"RUB_EUR", 1/93.7m},
                {"USD_EUR", 1/1.09m},
                {"USD_USD", 1.0m},
                {"EUR_EUR", 1.0m},
                {"RUB_RUB", 1.0m}
            };
        }

        public decimal Convert(string sourceCurrency, string targetCurrency, decimal amount)
        {
            if (sourceCurrency == targetCurrency)
                return amount;

            string key = $"{sourceCurrency}_{targetCurrency}";
            if (exchangeRates.ContainsKey(key))
            {
                return Math.Round(amount * exchangeRates[key], 2);
            }

            throw new InvalidOperationException($"Курс для конвертации {sourceCurrency} → {targetCurrency} не найден");
        }

        public void ConvertCurrency()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== КОНВЕРТЕР ВАЛЮТ ===");

                string sourceCurrency = GetCurrencyInput("Исходная валюта (RUB, USD, EUR): ");
                string targetCurrency = GetCurrencyInput("Целевая валюта (RUB, USD, EUR): ");

                Console.Write("Сумма для конвертации: ");
                decimal amount = GetValidatedAmount();

                decimal result = Convert(sourceCurrency, targetCurrency, amount);

                Console.WriteLine($"\n=== РЕЗУЛЬТАТ КОНВЕРТАЦИИ ===");
                Console.WriteLine($"{amount:F2} {sourceCurrency} = {result:F2} {targetCurrency}");
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

        private string GetCurrencyInput(string prompt)
        {
            string[] validCurrencies = { "RUB", "USD", "EUR" };

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.ToUpper().Trim();

                if (Array.Exists(validCurrencies, currency => currency == input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Ошибка: допустимые валюты - RUB, USD, EUR");
                }
            }
        }

        private decimal GetValidatedAmount()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal amount) && amount >= 0)
                {
                    return amount;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите неотрицательное число");
                    Console.Write("Сумма для конвертации: ");
                }
            }
        }
    }
}