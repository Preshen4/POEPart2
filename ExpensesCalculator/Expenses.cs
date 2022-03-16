using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ExpensesCalculator
{
    public abstract class Expenses
    {
        public delegate void ExpensesWarning(IDictionary<string, decimal> expenses);

        public decimal Price { get; set; }

        public decimal Deposit { get; set; }

        public decimal InterestRate { get; set; }

        public decimal NumOfMonths { get; set; }

        public decimal Expense { get; set; }

        public IDictionary<string, decimal> MonthlyExpenses { get; set; } // Properties... stores all of the income and expenses from the user

        public abstract decimal MonthlyLoanPayment(); // Abstract method 

        public void GetMonthlyExpenses(ExpensesWarning expensesWarning)
        {
            int option;
            MonthlyExpenses = new Dictionary<string,decimal>();

            Console.WriteLine("Please enter the following values:");
            MonthlyExpenses.Add("Income", GetExpenses("a) Gross monthly income (before deductions): "));

            MonthlyExpenses.Add("Tax", GetExpenses("b) Estimated tax deducted: "));
            expensesWarning(MonthlyExpenses);

            Console.WriteLine("c) Estimated monthly expenditures in each of the following categories: ");

            MonthlyExpenses.Add("Groceries", GetExpenses("i) Groceries: "));
            expensesWarning(MonthlyExpenses);

            MonthlyExpenses.Add("Utlities", GetExpenses("ii) Water and lights: "));
            expensesWarning(MonthlyExpenses);

            MonthlyExpenses.Add("Tavel", GetExpenses("iii) Travel costs (including petrol):"));
            expensesWarning(MonthlyExpenses);

            MonthlyExpenses.Add("Phone", GetExpenses("iv) Cellphone and Telephone: "));
            expensesWarning(MonthlyExpenses);

            MonthlyExpenses.Add("Other", GetExpenses("v) Other expenses: "));
            expensesWarning(MonthlyExpenses);

            option = GetOption("Would you like to", "rent" , "buy property ?:");

            if (option == 1)  // User enters the value of their rent per month

            {

                RentPayment();
                
                
                expensesWarning(MonthlyExpenses);

            }
            else // User enters details of the house they will be applying for a loan to buy a house

            {

                HomeLoanPayment();
                
                expensesWarning(MonthlyExpenses);

            }

            option = GetOption("Would you like to buy a vehicle?", "Yes", "No");

            if (option == 1)
            {
                VehiclePayment();
                
                expensesWarning(MonthlyExpenses);

            }

            DisplayExpenses();

        }

        private void DisplayExpenses()

        {

            string banner = new('-', 51);
            Console.WriteLine($"Your remaining month for the month after all deductions is: {GetRemainingMoney():C2}");

            Console.WriteLine("Your monthly expenses in descending order:");
            Console.WriteLine(banner);
            Console.WriteLine($"{"Expense",20}  {"Amount",10}");
            Console.WriteLine(banner);

            foreach (var expense in MonthlyExpenses.OrderByDescending(key => key.Value))
            {
                if (expense.Key != "Income")
                {
                    Console.WriteLine($"{expense.Key,20}  {expense.Value,12:C2}"); 
                }
            }
        }

        private void HomeLoanPayment()
        {
            HomeLoan homeLoan = new HomeLoan(); // Uses the A= P(1+in) formula

            MonthlyExpenses.Add("HomeLoan", homeLoan.MonthlyLoanPayment());

            Console.WriteLine($"Your monthly repayment for the house is {MonthlyExpenses["HomeLoan"]:C2}");

            if (MonthlyExpenses["HomeLoan"] > (MonthlyExpenses["Income"] * 0.33M)) // The user loan should be more than a third of their gross income for a likely approval of a home loan
            {

                Console.WriteLine("\aApproval of home loan is unlikely");

            }
            else
            {

                Console.WriteLine("Approval of home loan is likely");

            }
        }

        private void VehiclePayment()
            
        {

            Vehicle vehicle = new Vehicle();
            MonthlyExpenses.Add("VehicleLoan", vehicle.MonthlyLoanPayment());
            Console.WriteLine($"Monthly repayment for vehicle is {MonthlyExpenses["VehicleLoan"]:C2}");

        }

        private void RentPayment()

        {

            MonthlyExpenses.Add("Rent", GetExpenses("Enter your monthly rent amount: "));

        }

        private decimal GetRemainingMoney() 
        {

            decimal expenses = 0;
            
            for (int i = 1; i < MonthlyExpenses.Count; i++)
            {

                expenses += MonthlyExpenses.ElementAt(i).Value;

            }

            return MonthlyExpenses["Income"] - expenses;

        }

        private int GetOption(string question, params string[] options)
        {
            string prompt = question + ' ' + string.Join( ", " , options.Select( ( name , i ) => $"{ name } ({ i + 1 })" ) ) + ": ";

            while (true)
            {
                Console.Write(prompt);
                string answer = Console.ReadLine() ?? throw new EndOfStreamException();
                if (int.TryParse(answer, out int value)
                    && value > 0
                    && value <= options.Length)
                    return value;

                Console.WriteLine("Please enter a valid option");
            }
        }

        protected decimal GetExpenses(string name)
        {
            string prompt = $"{name} ";

            while (true)
            {
                Console.Write(prompt);
                string answer = Console.ReadLine() ?? throw new EndOfStreamException();

                if (decimal.TryParse(answer, out decimal amount) && amount >= 0)
                    return amount;

                Console.WriteLine("Please enter a valid amount");
            }

        }

    }
}
