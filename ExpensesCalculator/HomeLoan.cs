using System;

namespace ExpensesCalculator
{
    public class HomeLoan : Expenses
    {
        
        public HomeLoan() 
        {
            GetHomeLoanDetails();
        }
        public override decimal MonthlyLoanPayment()
        {
            // A = P(1+in) this formula will be used to calculate the home loan repayment, formula was modified to return the monthly repayment

            decimal A = Price - Deposit; // P 
            A = A * (1 + ((InterestRate * 0.01m) * (NumOfMonths / 12))); // n = number of years 

            return (A / NumOfMonths); // the monthly payment for the loan
        }

        public void GetHomeLoanDetails() 
        {
            // Gets the info for the home loan

            Console.WriteLine("Please enter the following details: ");

            Price = GetExpenses("Purcase price of the property: ");

            Deposit = GetExpenses("Total deposit: ");

            InterestRate = GetExpenses("Interest rate: ");

            NumOfMonths = GetExpenses("Number of months to repay (Between 240 and 360 months): ");

            do
            {
                NumOfMonths = GetExpenses("Number of months to repay (Between 240 and 360 months)");
            } while (NumOfMonths is < 240 or > 360);
        }
    }
}
