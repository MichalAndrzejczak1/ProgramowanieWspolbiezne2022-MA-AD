using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTests
{
    [TestClass]
    public class ExampleProgramTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            double money1 = 12;
            double money2 = 50;

            BankAccount one = new BankAccount("Jon Smith", money1);
            BankAccount two = new BankAccount("Joanna Dark", money2);
            Assert.AreNotEqual(one.getMoney(), two.getMoney()); 
            Assert.AreEqual(money1, one.getMoney());
            Assert.AreEqual(money2, two.getMoney());
            
        }

        [TestMethod]
        public void MoneyOutTest()
        {
            double money1 = 12;

            BankAccount one = new BankAccount("Jon Smith", money1);

            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => one.MoneyOut(200));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => one.MoneyOut(0));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => one.MoneyOut(-5));
            Assert.AreEqual(money1, one.getMoney());

            one.MoneyOut(5);
            Assert.AreEqual(7, one.getMoney());
        }

        [TestMethod]
        public void MoneyInTest()
        {
            double money1 = 12;

            BankAccount one = new BankAccount("Jon Smith", money1);

            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => one.MoneyIn(0));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => one.MoneyIn(-5));
            Assert.AreEqual(money1, one.getMoney());

            one.MoneyIn(5);
            Assert.AreEqual(17, one.getMoney());
        }

        [TestMethod]
        public void isRichTest()
        {
            double money1 = 1000;
            double money2 = 100000;

            BankAccount one = new BankAccount("Jon Smith", money1);
            BankAccount two = new BankAccount("John Doe", money2);

            Assert.IsFalse(one.isRich());
            Assert.IsTrue(two.isRich());
            
        }
    }
}