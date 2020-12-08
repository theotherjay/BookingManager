using System;
using System.Collections.Generic;
using System.Text;

namespace BookingMgr.contracts
{
    public interface IBookingCleanup
    {
        /// <summary>
        /// Removes a booking from the collection of bookings that exist based on the passed in guest, room, and date.
        /// </summary>
        /// <param name="guest"></param>
        /// <param name="room"></param>
        /// <param name="date"></param>
        void RemoveBooking(string guest, int room, DateTime date);
        void ClearAllBookings();
    }
}
