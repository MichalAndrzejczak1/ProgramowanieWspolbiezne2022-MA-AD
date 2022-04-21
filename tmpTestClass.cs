using System;

public class tmpTestClass
{
	public tmpTestClass()
	{

    private readonly string clientName;
    private double money;

    public BankAccount(string cN, double m)
    {
        clientName = cN;
        money = m;
    }


    public void MoneyOut(double amount)
    {
        if (amount > money | amount <= 0)
        {
            throw new ArgumentOutOfRangeException($"{amount} is invalid argument");
        }

        money -= amount;
    }
    public void MoneyIn(double amount)
    {
        if (amount <= 0)
        {
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

    public static void Main()
    {
        BankAccount one = new BankAccount("Jon Smith", 5000);
        Console.WriteLine($"Name of the bank client is {one.getClientName()}, money is {one.getMoney()}.");
    }

}
}
