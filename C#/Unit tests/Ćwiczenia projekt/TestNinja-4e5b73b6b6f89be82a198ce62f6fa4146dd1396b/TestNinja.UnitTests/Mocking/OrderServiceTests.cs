using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            //Arrange
            var storageMock = new Mock<IStorage>();
            var orderService = new OrderService(storageMock.Object);
            
            //Act
            var order = new  Order();
            orderService.PlaceOrder(order);

            //Assert
            storageMock.Verify(st => st.Store(order));
        }
    }
}
