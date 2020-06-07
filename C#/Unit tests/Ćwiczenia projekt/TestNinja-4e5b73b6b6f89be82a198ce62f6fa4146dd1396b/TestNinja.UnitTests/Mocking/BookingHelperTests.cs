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
    class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartureOn(2017, 1, 20),
                Reference = "a"
            };

            _repositoryMock = new Mock<IBookingRepository>();
            _repositoryMock.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            //Action
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _repositoryMock.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfExistingBooking_ReturnsExistingBookingReference()
        {
            //Action
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repositoryMock.Object);

            //Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfExistingBookingButFinishesAfter_ReturnsExistingBookingReference()
        {
            //Action
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _repositoryMock.Object);

            //Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterExistingBooking_ReturnsEmptyString()
        {
            //Action
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = Before(_existingBooking.DepartureDate, days:2)
            }, _repositoryMock.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingsOverlapsButNewBookingIsCancelled_ReturnsEmptyString()
        {
            //Action
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                Status = "Cancelled",
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _repositoryMock.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }


        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartureOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
