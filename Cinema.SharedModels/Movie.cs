using System;

namespace Cinema.SharedModels
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan Length { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
    }
}
