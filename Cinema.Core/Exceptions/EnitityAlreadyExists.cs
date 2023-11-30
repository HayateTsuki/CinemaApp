namespace Cinema.Core.Exceptions
{
    public class EnitityAlreadyExists : CinemaException
    {
            public EnitityAlreadyExists(string message)
                : base(message)
            {
            }
    }
}
