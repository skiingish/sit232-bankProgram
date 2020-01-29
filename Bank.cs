using System;
using System.Collections.Generic;
using System.Linq;

namespace BankProgram
{
    public class Bank
    {
        //field used to store bank accounts
        public static List<Account> _accounts = new List<Account>();
        private List<Transaction> _transactions {get; set;}
        //method to add an account to the accounts list
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }
        //returns a required account if it can be found, else it returns null
        public Account GetAccount(String name) 
        {
            Account _name = null;
            //loop through the list of accounts lookibg for the passed in name;
            for (int i = 0; i < _accounts.Count; i++)
            {
                _name = _accounts[i]; //_name is equal to the current account being stored
                if (name == _name.Name) //if the name of the current account matches the search term then return that account
                {
                    //System.Console.WriteLine("found it");
                    return _name;
                }     
            
            }
            //no matching name found.
            //System.Console.WriteLine("not found");
            return _name = null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            //transaction.Execute();
            _transactions.Add(transaction);

            System.Console.WriteLine(_transactions);
        }
    }


    
}