using NUnit.Framework;
using BookingMgr.contracts;
using BookingMgr.Model;
using BookingMgr;
using System.Collections.Generic;
using System;

namespace BookingMgrTests
{
    public class JasonsAwesomeBookingManagerTests
    {
        /// <summary>
        /// For simplicity's sake, I am not using dependency injection here.  
        /// If I was going to make this application more robust, I would use 
        /// a dependency injection container to wire up concrete classes.
        /// </summary>
        /// 
        public static List<int> ROOMS = new List<int> { 101, 102, 103, 201, 202, 203, 400, 401, 402 };
        public static IBookingManager _bookingMgr = new JasonsAwesomeBookingManager(ROOMS);
        public static IBookingCleanup _bookingCleanup = _bookingMgr as IBookingCleanup;

        [SetUp]
        public void Setup()
        {
            _bookingCleanup.ClearAllBookings();
        }

        [Test]
        public void BookInvalidRoomFail()
        {
            int invalidRoom = 1000;
            Assert.IsTrue(!ROOMS.Contains(invalidRoom));
            Assert.IsFalse(_bookingMgr.AddBooking("jimbob", invalidRoom, DateTime.Now));
        }

        [Test]
        public void BookInvalidDateFail()
        {
            DateTime invalidDate = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0)) ;
            Assert.IsFalse(_bookingMgr.AddBooking("jimbob", ROOMS[0], invalidDate));
        }

        [Test]
        public void BookValidDate()
        {
            DateTime validDate = DateTime.Today.Add(new TimeSpan(1, 0, 0, 0));
            Assert.IsTrue(_bookingMgr.AddBooking("jimbob", ROOMS[0], validDate));
        }

        [Test]
        public void DoubleBookRoomFail()
        {
            DateTime validDate = DateTime.Today.Add(new TimeSpan(1, 0, 0, 0));
            Assert.IsTrue(_bookingMgr.AddBooking("jimbob", ROOMS[0], validDate));
            Assert.IsFalse(_bookingMgr.AddBooking("jimbob", ROOMS[0], validDate));
        }

        [Test]
        public void IsRoomAvailableTest()
        {
            DateTime validDate = DateTime.Today.Add(new TimeSpan(1, 0, 0, 0));
            Assert.IsTrue(_bookingMgr.IsRoomAvailable(ROOMS[0], validDate));
            Assert.IsTrue(_bookingMgr.AddBooking("jimbob", ROOMS[0], validDate));
            Assert.IsFalse(_bookingMgr.IsRoomAvailable(ROOMS[0], validDate));
            Assert.IsFalse(_bookingMgr.AddBooking("jimbob", ROOMS[0], validDate));
        }

        [Test]
        public void GetAvailableRoomsTest()
        {
            DateTime validDate = DateTime.Today.Add(new TimeSpan(1, 0, 0, 0));
            var availableRooms = _bookingMgr.getAvailableRooms(validDate) as List<int>;

            Assert.IsTrue(availableRooms.Contains(ROOMS[0]));
            Assert.IsTrue(_bookingMgr.AddBooking("jimbob", ROOMS[0], validDate));
            Assert.IsFalse(_bookingMgr.IsRoomAvailable(ROOMS[0], validDate));

            availableRooms = _bookingMgr.getAvailableRooms(validDate) as List<int>;
            Assert.IsFalse(availableRooms.Contains(ROOMS[0]));
        }
    }
}