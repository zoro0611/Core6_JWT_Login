using System;
using System.Collections.Generic;

namespace Core6_JWT_Login.Models.DBModels
{
    public partial class User : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime LastLoginDateTime { get; set; }
    }
}
