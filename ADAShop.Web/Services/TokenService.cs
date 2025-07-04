namespace ADAShop.Web.Services
{
    public class TokenService : ITokenService
    {
        public string? Token { get; set; }

        public void AddTokenCookies(HttpResponse httpResponse, string token)
        {
            httpResponse.Cookies.Append("jwt_token", token, new CookieOptions
            {
                HttpOnly = true, // 🔒 Bloquea acceso desde JS
                Secure = true, // // 🔒 Solo se envía por HTTPS
                SameSite = SameSiteMode.Strict, // // 🔒 Previene CSRF en la mayoría de los casos
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
        }

        public string? GetTokenCookies(HttpRequest httpRequest)
        {
            httpRequest.Cookies.TryGetValue("jwt_token", out string? token);
            return token;
        }
    }
}