namespace Cinema.Core.Exceptions
{
    public class BusinessOperationException : CinemaException
    {
        public BusinessOperationException(string message)
            : base(message)
        {
        }
    }
}
