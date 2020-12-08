using System;
using System.Collections.Generic;
using System.Text;
using BookingMgr.contracts;
using BookingMgr.Model;
using System.Linq;

namespace BookingMgr
{
    public class JasonsAwesomeBookingManager : IBookingManager, IBookingCleanup
    {
        public JasonsAwesomeBookingManager(List<int> hotelRooms)
        {
            Bookings = new List<Booking>();
            _hotelRooms = hotelRooms;
            _objLock = new object();
        }

        private readonly object _objLock = new object();
        public List<Booking> Bookings { get; private set; }
        private List<int> _hotelRooms { get; set; }

        public bool AddBooking(string guest, int room, DateTime date)
        {
            //Don't let them book a room in the past
            if(date.Date < DateTime.Now.Date)
            {
                return false;
            }
            //Don't let them book a room that doesn't exist
            else if(!_hotelRooms.Contains(room))
            {
                return false;
            }
            //Don't let them book a room that is already taken (it might have been booked between the time you checked if it was available and now)
            else if( (from a in Bookings
             where a.Date == date && a.RoomNumber == room
             select a).Count() > 0)
            {
                return false;
            }
            else
            {
                //get lock (need to be thread safe)
                lock(_objLock)
                {
                    Bookings.Add(new Booking { Date = date, Guest = guest, RoomNumber = room });
                }
                return true;
            }
        }

        public void RemoveBooking(string guest, int room, DateTime date)
        {
            //no need to get a lock here.  We just want to make sure that any booking for this guest, room, and date don't exist in our system.
            var result = (from a in Bookings where a.Guest == guest && a.RoomNumber == room && a.Date == date select a).ToList();
            if(result.Count() > 0)
            {
                foreach(var a in result)
                {
                    Bookings.Remove(a);
                }
            }
        }

        public IEnumerable<int> getAvailableRooms(DateTime date)
        {
            //Example: 101, 102 are booked on this date. Our total set of rooms is: 101, 102, 103, 104 .  We need the result to be 103 and 104
            var booked = from a in Bookings
                         where a.Date == date
                         select a.RoomNumber;

            var result = new List<int>();
            for(int i=0; i<_hotelRooms.Count(); i++)
            {
                result.Add(_hotelRooms[i]);
            }
            foreach(int i in booked)
            {
                result.Remove(i);
            }
            return result;
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {           
            return (from a in Bookings where a.Date == date && a.RoomNumber == room select a).Count() == 0;
        }

        public void ClearAllBookings()
        {
            Bookings.Clear();
        }
    }
}
