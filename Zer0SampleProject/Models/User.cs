using System.Collections.Generic;
namespace Zer0SampleProject
{
    public class User
    {
		public User(){}

		public int UserId { get; set; }
		public string Name { get; set; }
		public string AuthenticationKey { get; set; }
		public virtual ICollection<UserProject> UserProject { get; set; }

	}
}
