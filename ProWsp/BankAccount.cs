using System;


namespace BankAccountNS
{
    public class BankAccount
    {
        private readonly string clientName;
        private double money;
      
        public BankAccount(string cN, double m)
        {
            clientName  = cN;
            money = m;
        }


        public void MoneyOut(double amount)
        {
            if(amount > money | amount <= 0)
            {
                throw new ArgumentOutOfRangeException($"{amount} is invalid argument");
            }

            money -= amount;
        }
        public void MoneyIn(double amount)
        {
            if(amount <= 0) {
                throw new ArgumentOutOfRangeException($"{amount} is invalid argument");
            }
            money += amount;
        }


        public double getMoney()
        {
            return money;
        }
        public string getClientName()
        {
            return clientName;
        }

        public bool isRich()
        {
            if(money >= 100000)
            {
                return true;
            }
            return false;
        }

        public static void Main()
        { 
            BankAccount one = new BankAccount("Jon Smith", 5000);
            Console.WriteLine($"Name of the bank client is {one.getClientName()}, money is {one.getMoney()}.");
        }
    }
}