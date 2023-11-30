namespace Cinema.Core.Exceptions
{
    public class EntityNotFoundException : CinemaException
    {
        public EntityNotFoundException(string message, long id)
            : base(message)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
