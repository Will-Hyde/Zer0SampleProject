using System;

public class Project
{
	public Project()
	{
	}

	public int ProjectID { get; set; }
	public Format Format { get; set; }
	public Status Status { get; set; }
	public Visibility Visibility { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public virtual ICollection<Users> Users { get; set; }
}
