using System;
using System.Collections.Generic;

namespace Cinema.Domain.Data.Entities
{
    public class ScreeningEntity
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public MovieEntity Movie { get; set; }

        public int HallId { get; set; }

        public HallEntity Hall { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public List<BookingEntity> Bookings { get; set; }
    }
}
