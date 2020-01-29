using System;
using System.Collections.Generic;
using System.Linq;

namespace BankProgram
{
    public abstract class Transaction
    {
        protected decimal _amount;
        private bool _executed;
        private bool _reversed;
        private DateTime _timeStamp;

        public Transaction(decimal amount)
        {

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

        public abstract bool Success
        {
            get;
        }

        public DateTime DateTime
        {
            get{ return _timeStamp;}
        }

        public abstract void Print();
        

        public virtual void Execute()
        {
            
            if (_executed)
            {
                throw new Exception("Transaction has allready been completed");
            }
            _executed = true;
            _timeStamp = DateTime.Now;
            System.Console.WriteLine(_timeStamp);
        }

        public virtual void Rollback()
        {
            if (_executed)
            {
                throw new Exception("Transaction has allready been completed");
            }
            _reversed = true;
            _timeStamp = DateTime.Now;
        }

    }
}