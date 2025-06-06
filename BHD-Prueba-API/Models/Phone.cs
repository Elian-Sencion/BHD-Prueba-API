namespace BHD_Prueba_API.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string CityCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
