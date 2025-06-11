namespace ADAShop.Web.Models
{
    public class ClaimsIdentityModel
    {
        public long UserId { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Token { get; set; }
    }
}