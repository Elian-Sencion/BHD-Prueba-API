namespace BHD_Prueba_API.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Modified { get; set; } = DateTime.UtcNow;
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        public string Token { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();
    }

}
