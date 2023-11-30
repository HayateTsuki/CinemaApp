using System.Collections.Generic;

namespace Cinema.Domain.Data.Entities
{
    public class HallEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SeatsPerRow { get; set; }

        public int Rows { get; set; }

        public List<ScreeningEntity> Screenings { get; set; }
    }
}
