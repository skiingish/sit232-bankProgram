using System;
namespace BankProgram
{
    public class DepositTransaction
    {
        Account _account;
        decimal _amount;
        bool _executed;
        public bool _success;
        bool _reversed;

        public bool Success 
        {
            get
            {
                return _success;
            }
        }
        public bool Reversed
        {
            get
            {
                return _reversed;
            }
        }
        public bool Executed
        {
            get
            {
                return _executed;
            }
        }
        public DepositTransaction(Account account, decimal amount) //create new object containing the account to action on and the amount to be moved
        {
            _account = account;
            _amount = amount;
        }

        public void Execute() // withdraw from the passed withdraw account as long as the method hasn't been called before
        {
            if (_executed)
            {
                throw new Exception("Cannot execute as it has already been executed");
            }
            _success = _account.Deposit(_amount); //deposit in to the required bank account
            _executed = true;
        }
        public void Rollback() // if rollback is called, check to make sure rollback hasn't happened before and that it hasn't already been rolled back
        {
            if (_executed == false)
            {
                throw new Exception("No Transaction to rollback");
            }
            if (_reversed)
            {
                throw new Exception("Transaction has already been reveresed");
            }
            _reversed = true; //set _reversed to true be used to make sure rollback doesn't happen more required 
            
            _account.Withdraw(_amount); //withdraw the amount from the deposited bank account
        }
        public void Print()
        {
            if (_success == true)
            {
                System.Console.WriteLine("The deposit transcation was successful");
                System.Console.WriteLine("The amount deposited was: ${0}", _amount);
            }
            else
            {
                Console.WriteLine("Deposit transcation wasn't succusful");
            }
        }
    }
}