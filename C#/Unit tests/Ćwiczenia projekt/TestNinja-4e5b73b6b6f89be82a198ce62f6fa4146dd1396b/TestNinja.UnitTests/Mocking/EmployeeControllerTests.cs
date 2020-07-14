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
    class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDataBase()
        {
            //Arrange
            var storageMock = new Mock<IEmployeeStorage>();
            var employeeController = new EmployeeController(storageMock.Object);

            //Act
            employeeController.DeleteEmployee(1);

            storageMock.Verify(s => s.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_RedirectsToEmployeesAction()
        {
            //Arrange
            var storageMock = new Mock<IEmployeeStorage>();
            var employeeController = new EmployeeController(storageMock.Object);

            //Act
            var result=employeeController.DeleteEmployee(1);

            //Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
