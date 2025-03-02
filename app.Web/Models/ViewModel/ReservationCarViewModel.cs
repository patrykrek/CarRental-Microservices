using app.Web.Models.DTO;

namespace app.Web.Models.ViewModel
{
    public class ReservationCarViewModel
    {
        public GetCarDTO carDTO { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
    }
}
