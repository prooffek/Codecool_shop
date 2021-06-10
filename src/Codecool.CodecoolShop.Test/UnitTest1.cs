using System.Linq;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using NUnit.Framework;

namespace Codecool.CodecoolShop.Test
{
    [TestFixture]
    public class Tests
    {
        private StatusDaoMemory _statusDaoMemory;
        private TravelAgencyDaoMemory _travelAgencyDaoMemory;
        
        [SetUp]
        public void Setup()
        {
            _statusDaoMemory = new StatusDaoMemory();
            _travelAgencyDaoMemory = new TravelAgencyDaoMemory();
        }
        
        [TestCase(1)]
        public void StatusDaoMemory_DataBase_SelectStatusById(int id)
        {
            var status = new Status() {Name = "Error", Description = "An error has occured"};
            var result = _statusDaoMemory.Get(id);
            Assert.AreEqual(status.Name, result.Name, "problem with getting status from db");
            Assert.AreEqual(status.Description, result.Description, "problem with getting status from db");
        }

        [Test]
        public void StatusDaoMemory_DataBase_SelectAllStatuses()
        {
            int length = 1;
            var result = _statusDaoMemory.GetAll();
            Assert.AreEqual(length, result.Count());
        }
        
        [Test]
        public void StatusDao_DataBase_AddToDb()
        {
            var status = new Status() {Name = "Error2", Description = "An error has occured 2"};
            int prevLength = _statusDaoMemory.GetAll().Count();
            _statusDaoMemory.Add(status);
            int newLength = _statusDaoMemory.GetAll().Count();
            var result = _statusDaoMemory.Get(2);
            
            Assert.AreEqual(prevLength + 1, newLength);
            Assert.AreEqual(status, result);
        }

        [TestCase(2)]
        public void StatusDao_DataBase_RemoveFromDB(int id)
        {
            int prevLength = _statusDaoMemory.GetAll().Count();
            _statusDaoMemory.Remove(id);
            int newLength = _statusDaoMemory.GetAll().Count();
            var result = _statusDaoMemory.Get(2);
            
            Assert.AreEqual(prevLength - 1, newLength);
            Assert.AreEqual(null, result);
        }
        
        [TestCase(1)]
        public void TravelAgencyDaoMemory_DataBase_SelectTravelAgencyById(int id)
        {
            var travelAgency = new TravelAgency() {Name = "Tui", Description = "Description"};
            var result = _travelAgencyDaoMemory.Get(id);
            Assert.AreEqual(travelAgency.Name, result.Name);
            Assert.AreEqual(travelAgency.Description, result.Description);
        }

        [Test]
        public void TravelAgencyDaoMemory_DataBase_SelectAllTravelAgency()
        {
            int length = 1;
            var result = _travelAgencyDaoMemory.GetAll();
            Assert.AreEqual(length, result.Count());
        }
        
        [Test]
        public void TravelAgencyDaoMemory_DataBase_AddTravelAgencyToDb()
        {
            var travelAgency = new TravelAgency() {Name = "Rainbow", Description = "Description 2"};
            int prevLength = _travelAgencyDaoMemory.GetAll().Count();
            _travelAgencyDaoMemory.Add(travelAgency);
            int newLength = _travelAgencyDaoMemory.GetAll().Count();
            var result = _travelAgencyDaoMemory.Get(2);
            
            Assert.AreEqual(prevLength + 1, newLength);
            Assert.AreEqual(travelAgency, result);
        }

        [TestCase(2)]
        public void TravelAgencyDao_DataBase_RemoveTravelAgencyFromDB(int id)
        {
            int prevLength = _travelAgencyDaoMemory.GetAll().Count();
            _travelAgencyDaoMemory.Remove(id);
            int newLength = _travelAgencyDaoMemory.GetAll().Count();
            var result = _travelAgencyDaoMemory.Get(2);
            
            Assert.AreEqual(prevLength - 1, newLength);
            Assert.AreEqual(null, result);
        }
    }
}