using System;

namespace classes
{
	public class GiftCardAccount : BankAccount
	{
		// Fields
		private decimal _monthlyDeposit = 0m;


		// Constructors

		public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
		{
			_monthlyDeposit = monthlyDeposit;
		}


		// Methods

		public override void PerformMonthEndTransactions()
        {
			if (_monthlyDeposit != 0)
            {
				MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
	}
}
