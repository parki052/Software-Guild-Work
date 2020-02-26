using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGFlooring.BLL;
using SGFlooring.Data;
using SGFlooring.Models;
using SGFlooring.Models.Responses;

namespace SGFlooring.Tests
{


    [TestFixture]
    class InMemoryRepoTests
    {
        [SetUp]
        public void ResetRepo()
        {
            InMemoryOrderRepo.ResetRepo();
        }

        [Test]
        public void CanGetOrders()
        {
            
            InMemoryOrderRepo repo = new InMemoryOrderRepo();
            var orders = repo.GetAllOrdersOnDate(new DateTime(2012, 5, 4));           
            Assert.AreEqual(1, orders.Count());
        }

        [Test]
        public void CanAddOrder()
        {
            InMemoryOrderRepo repo = new InMemoryOrderRepo();

            DateTime date = new DateTime(2012, 5, 4);
            Material product = new Material("Wood", 2.50m, 4.50m);
            StateTax tax = new StateTax("HI", "Hawaii", 15m);

            Order order = new Order(date, product, tax, "Bob", 150);
            repo.SaveOrder(order);

            var orders = repo.GetAllOrdersOnDate(new DateTime(2012, 5, 4));

            Assert.AreEqual(2, orders.Count());
            
        }

        [Test]
        public void CanLoadSingleOrder()
        {
            InMemoryOrderRepo repo = new InMemoryOrderRepo();

            var order = repo.LoadOrder(new DateTime(2012, 5, 4), 1);

            Assert.AreEqual("Wise", order.CustomerName);
        }

        [Test]
        public void CanRemoveOrder()
        {
            InMemoryOrderRepo repo = new InMemoryOrderRepo();
            var orders = repo.GetAllOrdersOnDate(new DateTime(2012, 5, 4));
            var orderToRemove = orders.Single();

            repo.RemoveOrder(orderToRemove);
           
            Assert.AreEqual(0, orders.Count());


        }

        [Test]
        public void CanEditOrder()
        {
            InMemoryOrderRepo repo = new InMemoryOrderRepo();
            var order = repo.LoadOrder(new DateTime(2012, 5, 4), 1);

            var orderToEdit = order;
            orderToEdit.CustomerName = "Bob";

            repo.ReplaceOrder(orderToEdit);
            
            var editedOrder = repo.LoadOrder(new DateTime(2012, 5, 4), 1);

            Assert.AreEqual("Bob", editedOrder.CustomerName);
        }

        [Test]
        public static void CanGetProducts()
        {
            InMemoryMaterialRepo repo = new InMemoryMaterialRepo();
            var products = repo.GetMaterials();

            Assert.AreEqual(4, products.Count());
        }

        [Test]
        public static void CanGetStateTaxInfo()
        {
            InMemoryTaxRepo repo = new InMemoryTaxRepo();
            var stateTaxes = repo.GetStateTaxes();

            Assert.AreEqual(4, stateTaxes.Count());
        }

        [TestCase("OH", true)]
        [TestCase("PA", true)]
        [TestCase("MI", true)]
        [TestCase("IN", true)]
        [TestCase("HI", false)]
        [TestCase("", false)]
        public static void CanCheckRepoForState(string stateAbbreviation, bool expectedResult)
        {
            Manager manager = new Manager(new InMemoryOrderRepo(), new InMemoryMaterialRepo(), new InMemoryTaxRepo());
            InMemoryTaxRepo repo = new InMemoryTaxRepo();
            var stateTaxData = repo.GetStateTaxes();

            CheckStateResponse response = manager.CheckForRequestedState(stateAbbreviation);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("Tile", true)]
        [TestCase("Carpet", true)]
        [TestCase("Laminate", true)]
        [TestCase("Wood", true)]
        [TestCase("Gold", false)]
        [TestCase("", false)]
        public static void CanCheckRepoForProduct(string product, bool expectedResult)
        {
            Manager manager = new Manager(new InMemoryOrderRepo(), new InMemoryMaterialRepo(), new InMemoryTaxRepo());
            InMemoryMaterialRepo repo = new InMemoryMaterialRepo();
            var productData = repo.GetMaterials();

            CheckProductResponse response = manager.CheckForRequestedProduct(product);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
