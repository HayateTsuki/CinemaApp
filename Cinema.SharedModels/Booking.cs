using System;

namespace Cinema.SharedModels
{
    public class Booking
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime Date { get; set; }

        public Screening Screening { get; set; }
    }
}
