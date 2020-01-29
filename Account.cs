using System;
namespace BankProgram //namespace which groups the two cs files together
{
    public class Account // create a new class called Account
    {
        private decimal _balance; //define balance var set to private
        private string _name; //define name var set to private

        public string Name //calls the private value stored in _name public
        {
            get { return _name; }
        }
        
        public Account(string name, decimal startingBalance) //create a new method that takes the account name and starting balance and creates an account object (a constructor)
        {
            _name = name;
            _balance = startingBalance;
        } 

        public bool Deposit(decimal amountToAdd) //function to add amount to the account balance as long as the user enters a vaild amount
        {
            if (amountToAdd > 0)
            {
                _balance += amountToAdd; // add to current account balance 
                return true;
            }
            else
            {
                Console.WriteLine("You can't deposit a negative amount"); // let the user know they went wrong 
                return false;
            }
            
        }

        public bool Withdraw(decimal amountToWithdraw) //function to change account balance on withdraw as long as the user enters a vaild amount
        {
            if (amountToWithdraw > 0)
            {
                _balance -= amountToWithdraw; // take away from the current account balance 
                return true;
            }
            else
            {
            Console.WriteLine("You can't withdraw a negative amount"); // let the user know they went wrong 
            return false;
            }
        }


        public void Print() //method to write the current account balance and account name to the console
        {
            Console.WriteLine("Name: {0} Account Balance: ${1}", _name, _balance);
        }

        

    }

}    