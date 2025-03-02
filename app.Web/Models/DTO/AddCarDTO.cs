namespace app.Web.Models.DTO
{
    public class AddCarDTO
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public decimal PricePerDay { get; set; }
        public int Year { get; set; }
        public int Days { get; set; }
    }
}
