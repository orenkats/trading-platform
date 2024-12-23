using System;

namespace UserService.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
