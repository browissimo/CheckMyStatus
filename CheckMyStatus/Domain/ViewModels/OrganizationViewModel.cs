namespace CheckMyStatus.Domain.ViewModels
{
    public class OrganizationViewModel
    {
        public int pan { get; set; }
        public bool LocalStatus { get; set; }
        public bool PortalStatus { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
