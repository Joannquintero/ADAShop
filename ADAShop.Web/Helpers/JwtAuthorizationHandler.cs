using ADAShop.Web.Services;
using System.Net.Http.Headers;

namespace ADAShop.Web.Helpers
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;

        public JwtAuthorizationHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_tokenService.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.Token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}