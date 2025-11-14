using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                DisplayMainMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        creditCalculator.CalculateCredit();
                        break;
                    case "2":
                        currencyConverter.ConvertCurrency();
                        break;
                    case "3":
                        depositCalculator.CalculateDeposit();
                        break;
                    case "4":
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
