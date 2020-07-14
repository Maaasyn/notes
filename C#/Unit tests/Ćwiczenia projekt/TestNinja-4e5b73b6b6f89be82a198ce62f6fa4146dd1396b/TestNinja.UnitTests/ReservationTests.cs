using System;
using NUnit.Framework;
using TestNinja.Fundamentals;
//using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User() {IsAdmin = true} );

            // Assert
            Assert.IsTrue(result);
            Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            //Arrange
            var userNotAdminMadeBy = new User();
            var reservation = new Reservation()
            {
                MadeBy = userNotAdminMadeBy
            };

            //Act
            var result = reservation.CanBeCancelledBy(userNotAdminMadeBy);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //Arrange
            var reservation = new Reservation() {MadeBy = new User()};
            var userNotMakingOrder = new User() {IsAdmin = false};
            //Act
            var result = reservation.CanBeCancelledBy(userNotMakingOrder);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
