using System.ComponentModel.DataAnnotations;

namespace CheckMyStatus.Domain.ViewModels
{
	public class RequestViewModel
	{
		public int PAN { get; set; }
		public string Email { get; set; }
		public int UserId { get; set; }
		public DateTime RequserDate { get; set; }
		public List<OrganizationViewModel> Organizations { get; set; } = new();

    }
}
