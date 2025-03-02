namespace app.Services.ReservationAPI.Utility
{
    public class SD
    {
        public static string CarApiBase { get; set; }
        public static string AuthApiBase { get; set; }
        public static string TokenCookie = "JwtToken";

        public enum ApiType
        {
            GET,
            PUT,
            DELETE,
            POST
        }
    }
}
