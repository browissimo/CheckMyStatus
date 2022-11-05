namespace CheckMyStatus.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<UserRequest> Requests { get; set; }
    }
}
