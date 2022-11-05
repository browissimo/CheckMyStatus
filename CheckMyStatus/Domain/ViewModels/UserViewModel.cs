using CheckMyStatus.Domain.Entity;

namespace CheckMyStatus.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Organization> Organizations { get; set; } = new();
    }
}
