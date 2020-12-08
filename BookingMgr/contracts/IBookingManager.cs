using System;
using System.Collections.Generic;
using System.Text;

namespace BookingMgr.contracts
{
    public interface IBookingManager
    {
        /// <summary>
        /// Determines if a room is available for a particular date.
        /// </summary>
        /// <param name="room">Room number</param>
        /// <param name="date">The date in which a guest would like to stay in the room (not the current date of the user booking the room).</param>
        /// <returns>Returns true if the room is not booked for a particular date. Otherwise, it returns false.</returns>
        bool IsRoomAvailable(int room, DateTime date);

        /// <summary>
        /// Allows a user to book a room for a particular guest (surname), room, and date.
        /// </summary>
        /// <param name="guest"></param>
        /// <param name="room"></param>
        /// <param name="date"></param>
        /// <returns>Returns true if booking was successful.  Just because a room is listed as "available" 
        ///          doesn't mean that when the user adds a booking, it hasn't already been snatched up by 
        ///          someone else.
        /// </returns>
        bool AddBooking(string guest, int room, DateTime date);

        /// <summary>
        /// Based on a passed in date, determine which rooms are available for that date.
        /// </summary>
        /// <param name="date">The date in which a guest would like to stay in the room (not the current date of the user booking the room).</param>
        /// <returns>A collection of room numbers available for booking on the supplied date.</returns>
        IEnumerable<int> getAvailableRooms(DateTime date);
    }
}
