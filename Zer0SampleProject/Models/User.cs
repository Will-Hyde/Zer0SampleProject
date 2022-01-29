using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer0SampleProject
{
    public class User
    {
		public User()
		{
		}

		[Key]
		public int UserId { get; set; }
		public string Name { get; set; }
		public string AuthenticationKey { get; set; }
		public virtual ICollection<UserProject> UserProject { get; set; }

	}
}
