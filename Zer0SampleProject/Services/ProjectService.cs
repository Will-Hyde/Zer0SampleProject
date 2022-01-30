using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Zer0SampleProject.Models;
using static Zer0SampleProject.Constants;

namespace Zer0SampleProject.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectDbContext _db;
        private readonly ILogger _logger;
        public ProjectService(ProjectDbContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of projects, censoring private project information based on the user passed in
        /// </summary>
        /// <param name="userId">UserId of the requesting user</param>
        /// <returns></returns>
        public ICollection<ProjectResponse> GetProjects(int requestingUserId, int filterUser, int filterType, int filterStatus)
        {
            try
            {
                var projects = _db.Projects.Include(p => p.UserProject).ThenInclude(up => up.User);
                //Filter by users, make sure that the private results are not returned if the user is not present in the group. 
                if (filterUser > 0) projects = projects.Where(p => p.UserProject.Any(u => u.UserId == filterUser) && (p.Visibility == Visibility.Public || p.UserProject.Any(u => u.UserId == requestingUserId))).Include(p => p.UserProject).ThenInclude(up => up.User);
                //Filter by project type
                if (filterType > 0) projects = projects.Where(p => p.Format == (Format)filterType).Include(p => p.UserProject).ThenInclude(up => up.User);
                //Filter by project status
                if (filterStatus > 0) projects = projects.Where(p => p.Status == (Status)filterStatus).Include(p => p.UserProject).ThenInclude(up => up.User);

                return projects.Select(p => ProjectToProjectResponse(p, requestingUserId)).ToList();
            } catch (Exception ex)
            {
                _logger.LogError(String.Format(@"Exception thrown while retrieving projects, message: {0}", ex.Message.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Retrieves a user object by userId and verifies the auth string matches the users authentication phrase
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="auth">User specific authentication string</param>
        /// <returns></returns>
        public bool VerifyUserAuth(int userId, string auth)
        {
            try
            {
                var user = _db.Users.Where(u => u.UserId == userId).FirstOrDefault();
                return user.AuthenticationKey == auth;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format(@"Exception thrown while authenticating user, message: {0}", ex.Message.ToString()));
                throw;
            }
        }

        private static ProjectResponse ProjectToProjectResponse( Project project, int userId)
        {
            var response = new ProjectResponse();
            if (userId != 0 && project.UserProject.Any(x => x.UserId == userId) || project.Visibility == Constants.Visibility.Public)
            {
                response.Name = project.Name;
                response.Description = project.Description;
                response.Users = project.UserProject.Select(up => new UserResponse() { Name = up.User.Name, UserId = up.UserId}).ToList();
                response.Type = project.Format;
                response.TypeText = response.Type.ToString();
            }
            response.Status = project.Status;
            response.StatusText = response.Status.ToString();
            response.Visibility = project.Visibility;
            response.VisibilityText = response.Visibility.ToString();
            response.ProjectId = project.ProjectId;
            return response;
        }
    }
}
