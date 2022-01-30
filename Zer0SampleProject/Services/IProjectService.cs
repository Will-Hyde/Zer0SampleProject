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
        public ICollection<ProjectResponse> GetProjects(int filterUser, int filterType, int requestingUserId, int filterstatus);
        public bool VerifyUserAuth(int userId, string auth);
    }
}
