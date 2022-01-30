using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer0SampleProject.Models;

namespace Zer0SampleProject.Services
{
    public interface IProjectService
    {
        public ICollection<ProjectResponse> GetProjects(int? userId = 0);
        public bool VerifyUserAuth(int userId, string auth);
    }
}
