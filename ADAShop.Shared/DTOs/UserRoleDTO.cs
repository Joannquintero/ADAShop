using ADAShop.Shared.Entities;

namespace ADAShop.Shared.DTOs
{
    public class UserRoleDTO
    {
        public User? user { get; set; }

        public string? roleName { get; set; }
    }
}