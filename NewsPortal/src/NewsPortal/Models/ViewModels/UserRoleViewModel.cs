using System;

namespace NewsPortal.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}