namespace Habitat.Teasers.Services
{
    public static class HumanFriendlyIntegerService
    {
        private static readonly string[] Ones =
        {
            "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
            "Nine"
        };

        private static readonly string[] Teens =
        {
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
            "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        private static readonly string[] Tens =
        {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty",
            "Ninety"
        };

        private static readonly string[] ThousandsGroups = {"", " Thousand", " Million", " Billion"};

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
                return leftDigits;

            var friendlyInt = leftDigits;
            if (friendlyInt.Length > 0)
                friendlyInt += " ";

            if (n < 10)
            {
                friendlyInt += Ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += Teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n%10, Tens[n/10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n%100, (Ones[n/100] + " Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n%1000, FriendlyInteger(n/1000, "", thousands + 1), 0);
            }

            return friendlyInt + ThousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
                return "Zero";
            
            if (n < 0)
                return "Negative " + IntegerToWritten(-n);
            
            return FriendlyInteger(n, "", 0);
        }
    }
}