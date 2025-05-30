﻿namespace app.Services.ReservationAPI.Models.DTO
{
    public class GetReservationDTO
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
    }
}
