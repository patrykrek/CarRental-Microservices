namespace app.Web.Models.DTO
{
    public class GetCarDTO
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerDay { get; set; }
        public int Year { get; set; }
    }
}
