using System;

namespace classes
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create a new bank account

            var account = new BankAccount("Richard", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");

            // Add transactions to the account

            account.MakeWithdrawal(500, DateTime.Now, "Rent");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);

            // Test the initial balance must be positive

            BankAccount invalidAccount;
            try
            {
 //               invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
                return;
            }

            // Test for a negative balance

            try
            {
 //               account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
                return;
            }


            // Test transaction report
            Console.WriteLine(account.GetAccountHistory());

            // Test derived classes

            // Test Gift Card Account
            var giftCard = new GiftCardAccount ("gift card", 100, 50);
            giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
            giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
            giftCard.PerformMonthEndTransactions();
            giftCard.MakeDeposit(27.5m, DateTime.Now, "add some additional cash");
            Console.WriteLine(giftCard.GetAccountHistory());


            // Test Interest Earning Account
            var savings = new InterestEarningAccount("savings account", 10000);
            savings.MakeDeposit(750, DateTime.Now, "save some money");
            savings.MakeDeposit(1250, DateTime.Now, "add more money");
            savings.MakeWithdrawal(250, DateTime.Now, "Needed to pay the bills");
            savings.PerformMonthEndTransactions();
            Console.WriteLine(savings.GetAccountHistory());

            // Test Line Of Credit Account

        }
    }
}