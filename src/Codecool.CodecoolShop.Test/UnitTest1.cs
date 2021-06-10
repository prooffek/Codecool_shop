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
        [SetUp]
        public void Setup()
        {
            _statusDaoMemory = new StatusDaoMemory();
        }

        [Test]
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
    }
}