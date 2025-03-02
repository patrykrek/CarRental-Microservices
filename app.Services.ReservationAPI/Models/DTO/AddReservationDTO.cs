namespace app.Services.ReservationAPI.Models.DTO
{
    public class AddReservationDTO
    {
        public int CarId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        
    }
}
