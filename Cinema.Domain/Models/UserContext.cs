using System;

namespace Cinema.Domain.Models
{
    public class UserContext : IUserContext
    {
        public UserContext(int id, string userName, string email)
        {
            Id = id;
            Name = userName;
            Email = email;
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; }

        public Guid TenantId { get; set; }
    }
}
