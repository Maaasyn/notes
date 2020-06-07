using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var bookings =
                _unitOfWork.Query<Booking>()
                    .Where(b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
            {
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);
            }

            return bookings;
        }
    }
}
