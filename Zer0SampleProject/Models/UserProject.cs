using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer0SampleProject
{
	public class UserProject
	{
		public UserProject()
		{
		}

		public int UserId { get; set; }
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		public virtual User User { get; set; }
	}
}
