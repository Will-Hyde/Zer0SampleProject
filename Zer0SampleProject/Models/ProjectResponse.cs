using System.Collections.Generic;
using static Zer0SampleProject.Constants;

namespace Zer0SampleProject.Models
{
    public class ProjectResponse
	{
		public ProjectResponse(){}

		public string Name { get; set; }
		public string Description { get; set; }
		public int ProjectId { get; set; }
		public Format Type { get; set; }
		public string TypeText { get; set; }
		public Status Status { get; set; }
		public string StatusText { get; set; }
		public Visibility Visibility { get; set; }
		public string VisibilityText { get; set; }
		public virtual ICollection<UserResponse> Users { get; set; }
    }

	public class UserResponse
	{	
		public int UserId { get; set; }
		public string Name { get; set; }
	}
}
