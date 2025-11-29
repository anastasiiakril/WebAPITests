using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITests {

    public class AuthRequest
    {
        public string username { get; set; } = "admin";
        public string password { get; set; } = "password123";
    }

    public class AuthResponse
    {
        public string token { get; set; }
    }

    public class BookingDates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }

    public class PostModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public double totalprice { get; set; }
        public bool depositpaid { get; set; }
        public BookingDates bookingdates { get; set; }
        public string additionalneeds { get; set; }


    }
}
