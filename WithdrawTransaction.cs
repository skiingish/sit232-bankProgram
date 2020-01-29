using System;
namespace BankProgram
{
    public class WithdrawTransaction : Transaction
    {
        Account _account;
        new decimal _amount;
        public bool _executed;
        public bool _success;
        bool _reversed;

        public override bool Success 
        {
            get
            {
                return _success;
            }
        }
        new public bool Reversed
        {
            get
            {
                return _reversed;
            }
        }
        new public bool Executed
        {
            get
            {
                return _executed;
            }
        }
        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            _amount = amount;
        }

        public override void Execute() // withdraw from the passed withdraw account as long as the method hasn't been called before
        {
            if (_executed)
            {
                throw new Exception("Cannot execute as it has already been executed");
            }
            base.Execute();
            _success = _account.Withdraw(_amount); //withdraw from the required bank account
            _executed = true;
            
        }
        public override void Rollback() // if rollback is called, check to make sure rollback hasn't happened before and that it hasn't already been rolled back
        {
            if (_executed == false)
            {
                throw new Exception("No Transaction to rollback");
            }
            if (_reversed)
            {
                throw new Exception("Transaction has already been reveresed");
            }
            base.Rollback();
            _reversed = true; //set _reversed to true be used to make sure rollback doesn't happen more then required 
            
            _account.Deposit(_amount); //deposit the amount back in to the withdrawn bank account
        }
        public override void Print()
        {
            if (_success == true)
            {
                System.Console.WriteLine("The withdraw transcation was successful");
                System.Console.WriteLine("The amount withdrawn was: ${0}", _amount);
            }
            else
            {
                Console.WriteLine("Withdraw Transcation wasn't succusful");
            }
        }
    }
}