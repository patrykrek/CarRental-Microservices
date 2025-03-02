using app.Services.ReservationAPI.Service.Interface;
using app.Services.ReservationAPI.Utility;

namespace app.Services.ReservationAPI.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetToken()
        {
            string? token = null;

            if (_httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out var authHeader) == true)
            {
                token = authHeader.ToString().Replace("Bearer ", "");
            }

            return token;


        }
    }
}
