using System;

namespace Cinema.Domain.Models
{
    public interface IUserContext
    {
        int Id { get; }

        string Name { get; }

        string Email { get; }
    }
}