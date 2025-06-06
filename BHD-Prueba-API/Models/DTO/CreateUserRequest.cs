namespace BHD_Prueba_API.Models.DTO
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<PhoneRequest> Phones { get; set; } = new();
    }


    public class PhoneRequest
    {
        public string Number { get; set; } = null!;
        public string CityCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
    }
}

