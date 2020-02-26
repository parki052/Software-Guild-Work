using Data.Repos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Dealership.Tests
{
    [TestFixture]
    public class InMemRepoTests
    {
        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(3, repo.GetAllPurchaseTypes().Count);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(5, repo.GetAllColors().Count);
        }

        [Test]
        public void CanLoadConditions()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(2, repo.GetAllConditions().Count);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(2, repo.GetAllMakes().Count);
        }

        [Test]
        public void CanLoadModels()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(2, repo.GetAllMakes().Count);
        }

        [Test]
        public void CanLoadStyles()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(4, repo.GetAllBodyStyles().Count);
        }

        [Test]
        public void CanLoadTransmissions()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(2, repo.GetAllTransmissions().Count);
        }

        [Test]
        public void CanLoadVehicles()

        {
            var repo = new InMemRepo();
            Assert.AreEqual(3, repo.GetAllVehicles().Count);
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(4, repo.GetAllStates().Count);
        }

        [Test]
        public void CanLoadAddresses()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(1, repo.GetAllAddresses().Count);
        }

        [Test]
        public void CanLoadCustomers()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(1, repo.GetAllCustomers().Count);
        }

        [Test]
        public void CanLoadSales()
        {
            var repo = new InMemRepo();
            Assert.AreEqual(1, repo.GetAllSales().Count);
        }

        [Test]
        public void CanEditVehicle()
        {
            var repo = new InMemRepo();
        }
    }
}
