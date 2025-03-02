using app.Web.Service.IService;
using app.Web.Utility;

namespace app.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("token");
        }

        public string? GetToken()
        {
            string? token = null;

            bool? hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);

            if(hasToken is true)
            {
                return token;
            }
            else
            {
                return null;
            }


        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}
