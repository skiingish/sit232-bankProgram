using System;
namespace BankProgram
{
    public class TransferTransaction
    {
        Account _toAccount;
        Account _fromAccount;
        decimal _amount;
        DepositTransaction _theDeposit;
        WithdrawTransaction _theWithdraw;
        bool _executed;
        bool _success;
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

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) // ctor used to create the transfer object
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;


            _theWithdraw = new WithdrawTransaction(_fromAccount,_amount); //create new object from the account to be withdraw from and the amount to transfer 
            _theDeposit = new DepositTransaction(_toAccount,_amount);      // create new object from the account to be tranferered in to
        }

        public void Execute() //execute the transfer
        {
            if (_executed) //if it's allready been executed then throw a error
            {
                throw new Exception("Cannot execute as it has already been executed");
            }
            
            
            _executed = true; //set executed to true to show that a atempt to transfer happened
            
            _theWithdraw.Execute();  
            if (_theWithdraw._success) //if the withdraw happened successful then deposit the amount in to the to account
            {
                _theDeposit.Execute(); 
                
                if (_theDeposit._success != true) //if the deposit was not successfull then roll back the transcation
                
                Rollback();
                _success = false; //set to false to display it didn't happen
            }
            
            
            if (_reversed != true) // check to make sure the transfer wasn't reversed
            {
                _success = true; // if all went well then set it success to true  
            }
            
            
        }
        public void Rollback()
        {
            if (_executed == false) //if the transcation didn't happen print error
            {
                throw new Exception("No Transaction to rollback"); 
            }
            if (_reversed)
            {
                throw new Exception("Transaction has already been reveresed");
            }
            
            
            if (_theWithdraw._success) // if the withdraw happened then rollback the withdraw on the account
            {
                _theWithdraw.Rollback();
            }
            if (_theDeposit._success) // if the deposit happened then rollback from the desposit (the deposit can only happen if the withdraw is successful) 
            {
                _theDeposit.Rollback();
            }

            _reversed = true;
            
        }
        public void Print()
        {
            if (_success == true) //if all successful print the tranfer details and the methods from withdraw and deposit
            {
                Utl.lineBreak();
                System.Console.WriteLine("The transcation was successful");
                Utl.lineBreak();
                System.Console.WriteLine("Transferred ${0} from {1} to {2}", _amount, _fromAccount.Name, _toAccount.Name);
                Utl.lineBreak();
                _theWithdraw.Print();
                Utl.lineBreak();
                _theDeposit.Print();
            }
            else //else print that it was not succesful
            {
                Console.WriteLine("Transcation wasn't succesful");
            }
        }
    }
}