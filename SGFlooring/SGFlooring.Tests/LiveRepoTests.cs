using NUnit.Framework;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooring.Data;
using SGFlooring.Models;

namespace SGFlooring.Tests
{
    [TestFixture]
    public class LiveRepoTests
    {
        const string _liveProductFile = @"C:\Data\SGFlooring\Data\Products.txt";
        const string _liveStateTaxFile = @"C:\Data\SGFlooring\Data\Taxes.txt";

        const string _testOrderDir = @"C:\Data\SGFlooring\TestData";
        const string _testOrder = @"C:\Data\SGFlooring\TestData\Orders_06012013.txt";
        const string _testOrderSeed = @"C:\Data\SGFlooring\TestData\seed.txt";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(_testOrder))
            {
                File.Delete(_testOrder);
            }
            File.Copy(_testOrderSeed, _testOrder);
        }

        [Test]
        public void CanGetOrders()
        {
            FileOrderRepo repo = new FileOrderRepo(_testOrderDir);
            var ordersOnDay = repo.GetAllOrdersOnDate(new DateTime(2013, 6, 1));

            Assert.AreEqual(2, ordersOnDay.Count());
        }

        [Test]
        public void CanGetProducts()
        {
            FileMaterialRepo repo = new FileMaterialRepo(_liveProductFile);
            var products = repo.GetMaterials();

            Assert.AreEqual(4, products.Count());
        }

        [Test]
        public void CanGetStateTaxInfo()
        {
            FileTaxRepo repo = new FileTaxRepo(_liveStateTaxFile);
            var states = repo.GetStateTaxes();

            Assert.AreEqual(4, states.Count());
        }

        [TestCase(1, "Wise")]
        [TestCase(2, "Kellen")]
        public void CanLoadOrder(int orderNumber, string expectedName)
        {
            FileOrderRepo repo = new FileOrderRepo(_testOrderDir);
            var order = repo.LoadOrder(new DateTime(2013, 6, 1), orderNumber);

            Assert.AreEqual(expectedName, order.CustomerName);
        }

        [Test]
        public void CanRemoveOrder()
        {
            FileOrderRepo repo = new FileOrderRepo(_testOrderDir);
            var orderToRemove = repo.LoadOrder(new DateTime(2013, 6, 1), 1);
            repo.RemoveOrder(orderToRemove);
            var updatedOrders = repo.GetAllOrdersOnDate(new DateTime(2013, 6, 1));

            Assert.AreEqual(1, updatedOrders.Count());
        }

        [Test]
        public void CanEditOrder()
        {
            var order = new Order(
                new DateTime(2013, 6, 1),
                new Material("Wood", (decimal)5.15, (decimal)4.75),
                new StateTax("OH", "Ohio", (decimal)61.88),
                "John",
                100
                );

            FileOrderRepo repo = new FileOrderRepo(_testOrderDir);
            repo.SaveOrder(order);

            var editedOrders = repo.GetAllOrdersOnDate(new DateTime(2013, 6, 1));
            var addedOrder = repo.LoadOrder(new DateTime(2013, 6, 1), 3);
            Assert.AreEqual(3, editedOrders.Count());
            Assert.AreEqual(addedOrder.CustomerName, "John");
            Assert.AreEqual(addedOrder.OrderNumber, 3);
        }

        [TestCase(new int[] { 2013, 6, 1 }, true)]
        [TestCase(new int[] { 2011, 4, 5 }, false)]
        public void CanCheckIfOrderFileExists(int[] toBuildDateTime, bool expectedResult)
        {
            DateTime date = new DateTime(toBuildDateTime[0], toBuildDateTime[1], toBuildDateTime[2]);
            FileOrderRepo repo = new FileOrderRepo(_testOrderDir);
            bool actual = repo.CheckIfOrderFileExists(date);

            Assert.AreEqual(expectedResult, actual);
        }
    }
}
