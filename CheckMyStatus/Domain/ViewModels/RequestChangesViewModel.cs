namespace CheckMyStatus.Domain.ViewModels
{
    public class RequestChanges
    {
        public int UserId { get; set; } 
        public string Email { get; set; }
        public int Pan { get; set; } 
        public bool Local { get; set; } = false;
        public bool Remote { get; set; } = false;
    }
}
