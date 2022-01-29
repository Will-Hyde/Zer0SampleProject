using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Zer0SampleProject.Services;

namespace Zer0SampleProject
{
    public class ProjectFunctions
    {
        private readonly IProjectService _projectService;
        public ProjectFunctions(IProjectService service)
        {
            this._projectService = service;
        }

        [FunctionName("GetProjectsPrivate")]
        public async Task<IActionResult> GetProjectsPrivate(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = "projects/{user}")] HttpRequest req,
            ILogger log, string user)
        {
            log.LogInformation(String.Format("GetProjectsPrivate function processed a request for User: {0}", user));
            _ = int.TryParse(user, out var userId);
            //TODO - Lookup authentication key for user, and use it to authenticate them
            return new OkObjectResult(_projectService.GetProjects(userId));
        }

        [FunctionName("GetProjectsPublic")]
        public async Task<IActionResult> GetProjectsPublic(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "projects")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetProjectsPublic function processed a request from an anonymous user");
            return new OkObjectResult(_projectService.GetProjects());
        }
    }
}
