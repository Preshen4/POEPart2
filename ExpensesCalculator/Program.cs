using System;
using System.Collections.Generic;
using System.Linq;

// Code Attribution: 
// 1) Link: https://codeasy.net/lesson/input_validation 
// 2) Link: https://www.tutorialspoint.com/chash-currency-c-format-specifier#:~:text=The%20%22C%22%20(or%20currency,C3%E2%80%9D)%20currency%20format%20specifier. 
//    Name: Samual Sam  
//    DatePublish: 12-Sep-2018 
//    DateTaken:   04-Mar-2022 
// 3) Link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
// 4) Link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is


namespace ExpensesCalculator
{
    public class Program : Expenses
    {

        static void Main(string[] args)
        {
          
            var program = new Program();
            program.GetMonthlyExpenses(ExpensesAlert); // Calls the override method
            
        }

        public override decimal MonthlyLoanPayment() // Method is not needed
        {
            throw new NotImplementedException();
        }

        private static void ExpensesAlert(IDictionary<string, decimal> expenses) // Method used in the delegate
        {
            // This method calculates if the expenses is greater than 75% of the income
            decimal total = 0;

            for (int i = 1; i < expenses.Count; i++)
            {

                total += expenses.ElementAt(i).Value;

            }

            decimal expenesPercent = ( total / expenses["Income"]) * 100 ;

            if (expenesPercent>=75)
            {
                Console.WriteLine($"\aYou current expenses ({total:C2}) exceed 75% of your total income ({expenses["Income"]:C2})!");
            }

        }
    }
}
