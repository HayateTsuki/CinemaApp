using System;

namespace Cinema.Domain.Data.Entities
{
    public class BookingEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public CinemaUser User { get; set; }

        public int ScreeningId { get; set; }

        public ScreeningEntity Screening { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime Date { get; set; }
    }
}
