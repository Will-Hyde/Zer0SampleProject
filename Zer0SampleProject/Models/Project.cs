using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Zer0SampleProject.Constants;

namespace Zer0SampleProject
{
	public class Project
	{
		public Project()
		{
		}

		[Key]
		public int ProjectId { get; set; }
		[Column("ProjectFormatTypeId")]
		public Format Format { get; set; }
		[Column("ProjectStatusTypeId")]
		public Status Status { get; set; }
		[Column("VisibilityTypeId")]
		public Visibility Visibility { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<UserProject> UserProject { get; set; }
	}
}
