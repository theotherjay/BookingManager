using System;
using BookingMgr.contracts;
using System.Collections;
using System.Collections.Generic;
namespace BookingMgr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");



            IBookingManager bookingMgr = new JasonsAwesomeBookingManager(new List<int> { 101, 102, 103, 201, 202, 203, 400, 401, 402 });
            IBookingCleanup bookingCleanup = (IBookingCleanup)bookingMgr;


        }
    }
}
