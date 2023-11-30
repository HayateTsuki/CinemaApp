using System;

namespace Cinema.SharedModels
{
    public class Screening
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public int HallId { get; set; }

        public Hall Hall { get; set; }

        public Movie Movie { get; set; }
    }
}
