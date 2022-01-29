using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Zer0SampleProject.Models;

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
        public ICollection<ProjectResponse> GetProjects(int? userId = null)
        {
            try
            {
                return _db.Projects.Include(p => p.UserProject).ThenInclude(up => up.User).Select(p => ProjectToProjectResponse(p, userId)).ToList();
            } catch (Exception ex)
            {
                _logger.LogError(String.Format(@"Exception thrown while retrieving projects, message: {0}", ex.Message.ToString()));
                throw;
            }
        }

        private static ProjectResponse ProjectToProjectResponse( Project project, int? userId)
        {
            var response = new ProjectResponse();
            if (userId != null && project.UserProject.Any(x => x.UserId == userId) || project.Visibility == Constants.Visibility.Public)
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
