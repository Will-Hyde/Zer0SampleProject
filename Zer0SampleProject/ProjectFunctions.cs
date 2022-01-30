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

        /// <summary>
        /// HTTP triggered function that takes a user and their authorization phrase, before returning a list of projects
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <param name="log">Logger</param>
        /// <param name="user">UserId of requesting user</param>
        /// <param name="auth">Authorization phrase of requesting user</param>
        /// <returns></returns>
        [FunctionName("GetProjectsPrivate")]
        public async Task<IActionResult> GetProjectsPrivate(
            [HttpTrigger(AuthorizationLevel.Function,"get", Route = "projects/user/{user}/auth/{auth}")] HttpRequest req,
            ILogger log, string user, string auth)
        {
            log.LogInformation(String.Format("GetProjectsPrivate function processed a request for User: {0}", user));
            _ = int.TryParse(user, out var userId);
            if( _projectService.VerifyUserAuth(userId, auth))
            {
                return new OkObjectResult(_projectService.GetProjects(userId));
            } else
            {
                return new UnauthorizedResult();
            }
        }

        /// <summary>
        /// HTTP triggered function that allows anonymous users to retrieve a list of projects
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <param name="log">Logger</param>
        /// <returns></returns>
        [FunctionName("GetProjectsPublic")]
        public async Task<IActionResult> GetProjectsPublic(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "projects")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetProjectsPublic function processed a request from an anonymous user");
            return new OkObjectResult(_projectService.GetProjects());
        }
    }
}
