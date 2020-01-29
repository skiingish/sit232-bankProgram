using System;
using SplashKitSDK;

namespace BankProgram {

    public enum MenuOption // create menu options
    {
        Withdraw,
        Deposit,
        Print,
        Transfer,
        NewAccount,
        Quit
    }
    public class Program
    {
        public static void Main()
        {   
            MenuOption userSelection;
            
            Bank bank = new Bank(); //create new bank object
          
            Console.WriteLine("Hello and Welcome to the bank");
            //create the first bank account
            Account account = CreateAccount();
            //add it to the list of banks
            bank.AddAccount(account);
            
            /* Main do while loop            */
            /*                               */
            /* Program runs from here        */
            /* And calls methods as required */
            do {
                
                userSelection = ReadUserOption(); // call method to get user selection
                Utl.lineBreak();
                Console.WriteLine("You selected: {0}", userSelection); //let the user know what they picked

                // call required method by using the userSelection
                switch(userSelection) 
                {
                    case MenuOption.Withdraw: // withdraw by calling the DoWithdraw method
                    
                    DoWithdraw(bank);
                    break;

                    case MenuOption.Deposit: // deposit by calling the DoDeposit method
                    
                    DoDeposit(bank);
                    break;

                    case MenuOption.Print: // print by calling the Doprint method
                    
                    DoPrint(bank);
                    break;

                    case MenuOption.Transfer: // transfer by calling the DoTransfer method
                    
                    DoTransfer(bank); 
                    break;

                    case MenuOption.NewAccount: // create a new account
                    
                    //use the create account method to create a new account
                    Account newAccount = CreateAccount();
                    //add it to the list of accounts within the bank object
                    bank.AddAccount(newAccount);
                    break;
                }
            } while (userSelection != MenuOption.Quit); // quit if the user selects quit

        }
        
        /*Methods Below */
        public static MenuOption ReadUserOption() //method of getting the users menu option
        {
            int option;
            int count = 0; // used to store the amount of times user incorrectly inputted a option 
            
            Utl.lineBreak();
            
            Console.WriteLine("- 1.Withdraw 2.Deposit 3.Print 4.Transfer 5.New Account 6.Quit -"); //print starting text
            do {
                if (count > 1) // check to see if count is more then 1 
                {
                    Utl.lineBreak();
                    Console.WriteLine("Ummmmm.......try inputting a valid number"); // if the end user enters the wrong input more then once
                }
                Utl.lineBreak();
                Console.WriteLine("Pick one of the options 1, 2, 3, 4, 5, or 6"); // give the user their choices 
                // try to convert users input in to int, if there's an error, let the user know
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());               //get option from user
                }
                catch 
                {
                    Utl.lineBreak();
                    Console.WriteLine("Umm not sure what that is but thats not a number");
                    option = -1; // set option to - 1 so user can try again 
                }
                if (option > 0 && option < 7)                               //if option is within required range break the loop and pass selected option 
                {
                    break; // break do while loop 
                }
                Utl.lineBreak();
                Console.WriteLine("Lets try that again...");     //if loop did not break display error to end user asking for their input again
                
                count++; // add to count if wrong input 
                } while (option < 0 || option > 6); // input is not within required range keep looping 
            
            return (MenuOption)(option -1 ); // return selected option in the correct numbering convention 
            
        }

        private static void DoDeposit(Bank toBank) // deposit method 
        {
            decimal input; //used to store the user inputted amount
            Account toAccount = FindAccount(toBank);
            if (toAccount == null) return;
            
            Utl.lineBreak();
            Console.WriteLine("Enter how much you would like to deposit?");
            input = Convert.ToDecimal(Console.ReadLine()); //store the amount after converting to decimal
            var deposit = new DepositTransaction(toAccount,input); //create a new deposit object
            deposit.Execute(); // excute the deposit on the deposit object
            deposit.Print(); // and print the object  
            
            //account.Deposit(input); // call the Deposit method pass the amount to be deposited
        }
        private static void DoWithdraw(Bank toBank) // withdraw method 
        {
            decimal input; //used to store the user inputted amount
            Account fromAccount = FindAccount(toBank);
            if (fromAccount == null) return;
            

            Utl.lineBreak();
            Console.WriteLine("Enter how much you would like to withdraw?");
            input = Convert.ToDecimal(Console.ReadLine()); //store the amount after converting to decimal
            
            var withdraw = new WithdrawTransaction(fromAccount,input);
            withdraw.Execute();
            withdraw.Print();
            toBank.ExecuteTransaction(withdraw);
            // account.Withdraw(input); //call the Withdraw method passing the amount to be withdraw
        }
        private static void DoTransfer(Bank transferBank) //create new method for the transfer
        {
            decimal input; //used to store the input to transfer
            
            Utl.lineBreak();
            System.Console.WriteLine("Please find the account to transfer from...");
            Account fromAccount = FindAccount(transferBank);
            if (fromAccount == null) return; //return if couldn't find bank
            Utl.lineBreak();
            System.Console.WriteLine("Please find the account to transfer in to...");
            Account toAccount = FindAccount(transferBank);
            if (toAccount == null) return; //return if couldn't find bank
            
            Utl.lineBreak(); //line break
            Console.WriteLine("Enter how much you would like to transfer from {0} to {1}?", fromAccount.Name,toAccount.Name); //ask how much to transfer
            input = Convert.ToDecimal(Console.ReadLine()); //store the amount after converting to decimal
            
            TransferTransaction theTransfer = new TransferTransaction(fromAccount, toAccount, input); //create a new transfer object taking in the jakes account object and my account plus the amount I would like to transfer
            
            theTransfer.Execute(); // execute the transfer
            theTransfer.Print(); // print the transfer
        }
        
        
        private static void DoPrint(Bank printBank) // print current balance of the passed account object
        {
            
            Account printAccount = FindAccount(printBank);
            if (printAccount == null) return;
            printAccount.Print(); //call the print method 
        }
        
        //create account method below
        private static Account CreateAccount()
        {
            string accountName;
            decimal openingBalance = 0;

            Utl.lineBreak();
            Console.WriteLine("Please enter an account name...");
            accountName = Console.ReadLine(); // set account name 
            
            Utl.lineBreak();
             // let the user create an account and define a opening balance (in the next version will need to add error handling when user create an account)
            // ask user to enter opening balance 
            // use a do while loop to make sure they don't enter a negative amount
            do {
                Utl.lineBreak();
                Console.WriteLine("Please Enter a opening balance...");
                // try to convert user input to decimal 
                try 
                {
                    openingBalance = Convert.ToDecimal(Console.ReadLine()); // set opening balance
                    if (openingBalance < 0) // display error to user when there is a negative amount
                    {
                        Console.WriteLine("Can't open account with negative balance");
                    }
                }
                // tell user that they didn't enter a vaild input
                catch {
                    Utl.lineBreak();
                    Console.WriteLine("not a vaild input");
                    openingBalance = -1; // set openingBalance to -1 so while loop keeps running 
                } 
            } while (openingBalance < 0);
            
            //create a new account based on the user input
            Account account = new Account(accountName, openingBalance);

            return account;
        }
        // method used to find the account that a action is requested on
        // takes in a bank object
        private static Account FindAccount(Bank fromBank)
        {
            System.Console.WriteLine("Enter account name: ");
            String name = Console.ReadLine();
            Account result = fromBank.GetAccount(name);

            if ( result == null )
            {
                Console.WriteLine($"No account found named {name}");
            }
            return result;
        }     
    }

    // extra untilies class below
    public class Utl // create new utilites class 
    {
        public static void lineBreak() // static method for creating a line break
        {
            Console.WriteLine(" "); //new blank console line string
        } 
    }


}
