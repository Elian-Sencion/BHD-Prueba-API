namespace BHD_Prueba_API.Models.DTO
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
