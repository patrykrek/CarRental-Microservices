namespace app.Services.ReservationAPI.Models.DTO
{
    public class GetUserReservationDTO
    {
        public DateTime ReservationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CarId { get; set; }
    }
}
