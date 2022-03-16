using System;

namespace ValidateTypeLibrary
{
    public static class Validate
    {
        // Validates the user input as input required from the user is decimal type
        public static bool ValidateDecimal(dynamic value) 
        {
             
            decimal temp;

            if (decimal.TryParse(value, out temp))

            {

                return true;

            }

            return false;

        }

        // Validates the user input as input required from the user is string type and cannot be empty
        public static bool ValidateString(string value)
        {

            if (String.IsNullOrEmpty(value))
            {
                return false;
            }

            return true;

        }

        // Validates the user input as the input required from the user must be positive
        public static bool ValidateNegative(double value) 
        {
            if (value > 0)

            {

                return false;

            }

            return true;
        }

        // Validates the user input as the input required from the user must be negative
        public static bool ValidatePositve(double value)
        {
            if (value < 0)

            {

                return false;

            }

            return true;
        }
    }
}
