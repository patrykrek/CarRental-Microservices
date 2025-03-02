namespace app.Web.Utility
{
    public class SD
    {
        public static string CarApiBase { get; set; }
        public static string AuthApiBase { get; set; }
        public static string ReservationApiBase { get; set; }

        public const string TokenCookie = "JwtToken";

        public const string AdminRole = "Admin";

        public const string UserRole = "User";
        public enum ApiType 
        {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
}
