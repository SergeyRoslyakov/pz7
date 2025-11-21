using System;

namespace pz7
{
    public class FinancialCalculator
    {
        private readonly CreditCalculator creditCalculator;
        private readonly CurrencyConverter currencyConverter;
        private readonly DepositCalculator depositCalculator;

        public FinancialCalculator()
        {
            creditCalculator = new CreditCalculator();
            currencyConverter = new CurrencyConverter();
            depositCalculator = new DepositCalculator();
        }

        public string SelectOption(string choice)
        {
            switch (choice)
            {
                case "1":
                    return "CreditCalculator";
                case "2":
                    return "CurrencyConverter";
                case "3":
                    return "DepositCalculator";
                case "4":
                    return "Exit";
                default:
                    return "Invalid";
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                DisplayMainMenu();

                string choice = Console.ReadLine();
                string result = SelectOption(choice);

                switch (result)
                {
                    case "CreditCalculator":
                        creditCalculator.CalculateCredit();
                        break;
                    case "CurrencyConverter":
                        currencyConverter.ConvertCurrency();
                        break;
                    case "DepositCalculator":
                        depositCalculator.CalculateDeposit();
                        break;
                    case "Exit":
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("=== ФИНАНСОВЫЙ КАЛЬКУЛЯТОР ===");
            Console.WriteLine("1. Расчет кредита");
            Console.WriteLine("2. Конвертер валют");
            Console.WriteLine("3. Калькулятор вкладов");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите опцию: ");
        }
    }
}