using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Domain.Data.Entities
{
    public class CinemaUser : IdentityUser<int>
    {
        public DateTime DateCreated { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public List<BookingEntity> Bookings { get; set; }
    }
}
