using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ConvertCurrency()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== КОНВЕРТЕР ВАЛЮТ ===");
                Console.WriteLine("Доступные валюты: RUB, USD, EUR");
                Console.WriteLine("Актуальные курсы:");
                Console.WriteLine($"1 USD = {exchangeRates["USD_RUB"]} RUB");
                Console.WriteLine($"1 EUR = {exchangeRates["EUR_RUB"]} RUB");
                Console.WriteLine($"1 EUR = {exchangeRates["EUR_USD"]} USD");
                Console.WriteLine();

                string sourceCurrency = GetCurrencyInput("Исходная валюта (RUB, USD, EUR): ");
                string targetCurrency = GetCurrencyInput("Целевую валюту (RUB, USD, EUR): ");

                Console.Write("Сумма для конвертации: ");
                decimal amount = GetValidatedAmount();

                if (sourceCurrency == targetCurrency)
                {
                    Console.WriteLine($"\nРезультат: {amount:F2} {targetCurrency}");
                    Console.WriteLine("Валюты одинаковые - конвертация не требуется");
                }
                else
                {
                    decimal rate = GetExchangeRate(sourceCurrency, targetCurrency);
                    decimal result = amount * rate;

                    Console.WriteLine($"\n=== РЕЗУЛЬТАТ КОНВЕРТАЦИИ ===");
                    Console.WriteLine($"{amount:F2} {sourceCurrency} = {result:F2} {targetCurrency}");
                    Console.WriteLine($"Курс: 1 {sourceCurrency} = {rate:F4} {targetCurrency}");

                    if (sourceCurrency == "USD" && targetCurrency == "RUB")
                        Console.WriteLine($"Актуальный курс ЦБ: 1 USD = {exchangeRates["USD_RUB"]} RUB");
                    else if (sourceCurrency == "EUR" && targetCurrency == "RUB")
                        Console.WriteLine($"Актуальный курс ЦБ: 1 EUR = {exchangeRates["EUR_RUB"]} RUB");
                }
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

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Ошибка: введите название валюты");
                    continue;
                }

                if (Array.Exists(validCurrencies, currency => currency == input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Ошибка: допустимые валюты - RUB, USD, EUR");
                    Console.WriteLine("Пример: USD, EUR, RUB");
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

        private decimal GetExchangeRate(string source, string target)
        {
            string key = $"{source}_{target}";
            if (exchangeRates.ContainsKey(key))
            {
                return exchangeRates[key];
            }
            else
            {
                throw new InvalidOperationException($"Курс для конвертации {source} → {target} не найден");
            }
        }

        public void DisplayCurrentRates()
        {
            Console.WriteLine("=== АКТУАЛЬНЫЕ КУРСЫ ВАЛЮТ ===");
            Console.WriteLine($"USD → RUB: {exchangeRates["USD_RUB"]}");
            Console.WriteLine($"EUR → RUB: {exchangeRates["EUR_RUB"]}");
            Console.WriteLine($"EUR → USD: {exchangeRates["EUR_USD"]}");
            Console.WriteLine($"RUB → USD: {exchangeRates["RUB_USD"]:F4}");
            Console.WriteLine($"RUB → EUR: {exchangeRates["RUB_EUR"]:F4}");
        }
    }
}
