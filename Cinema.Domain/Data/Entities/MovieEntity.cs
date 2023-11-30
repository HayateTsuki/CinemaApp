using System;
using System.Collections.Generic;

namespace Cinema.Domain.Data.Entities
{
    public class MovieEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan Length { get; set; }

        public string Description { get; set; }

        public List<ScreeningEntity> Screenings { get; set; }

        public string PictureUrl { get; set; }
    }
}
