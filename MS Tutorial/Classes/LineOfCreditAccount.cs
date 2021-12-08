using System;

namespace classes
{
	public class LineOfCreditAccount : BankAccount
	{
		// Fields



		// Constructors

		public LineOfCreditAccount(string name, decimal initialBalance) : base(name, initialBalance)
		{
		}


		// Methods

		public override void PerformMonthEndTransactions()
        {
			if (Balance < 0)
            {
				// Negate the balance to get a positive interest charge
				var interest = -Balance * 0.07m;
				MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }
	}
}
