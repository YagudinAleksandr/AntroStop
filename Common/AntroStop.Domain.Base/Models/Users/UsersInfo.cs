using AntroStop.Interfaces.Base.Entities;
using System;

namespace AntroStop.Domain.Base.Models.Users
{
    public class UsersInfo : IStringEntity
    {
        public string ID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
