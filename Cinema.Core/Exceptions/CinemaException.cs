using System;

namespace Cinema.Core.Exceptions
{
    public class CinemaException : Exception
    {
        public CinemaException(string message)
        {
            ErrorMessage = message;
        }

        public string ErrorMessage { get; }
    }
}
