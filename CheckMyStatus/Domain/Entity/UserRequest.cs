namespace CheckMyStatus.Domain.Entity
{
    public class UserRequest
    {
        public int Id { get; set; }
        public int pan  { get; set; }
        public DateTime CheckDate { get; set; }

        public bool LocalStatus { get; set; }
        public bool RemoteStatus { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }  
   
    }
}
