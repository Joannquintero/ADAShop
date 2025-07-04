namespace ADAShop.Web.Services
{
    public interface ITokenService
    {
        string Token { get; set; }

        void AddTokenCookies(HttpResponse httpResponse, string token);

        string? GetTokenCookies(HttpRequest httpRequest);
    }
}