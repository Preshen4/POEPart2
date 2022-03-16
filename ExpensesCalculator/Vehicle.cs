using System;

namespace ExpensesCalculator
{
    class Vehicle : Expenses
    {
        public Vehicle()
        {
            GetVehicleDetails();
            
        }

        public string Name { get; set; }
        public decimal Insurance { get; set; }
        public override decimal MonthlyLoanPayment()
        {
            // A = P(1+in) this formula will be used to calculate the home loan repayment, formula was modified to return the monthly repayment

            decimal A = Price - Deposit; // P 
            A = A * (1 + ((InterestRate * 0.01m) * (NumOfMonths / 12))); // n = number of years 
            
            return ((A / NumOfMonths) + Insurance);

        }

        public void GetVehicleDetails() 
        { 
            NumOfMonths = 60; // Repayment is assumed to be repaid over five years (60 months)
            Console.WriteLine("Please enter the following details: ");

            Console.Write("Model and make: ");
            Name = Console.ReadLine();

            Price = GetExpenses("Purcase price: ");

            Deposit = GetExpenses("Total deposit: ");

            InterestRate = GetExpenses("Interest rate (For 5 years): ");

            decimal Insurance = GetExpenses("Estimated insurance premium: ");
        }
    }
}
