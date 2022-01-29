using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Zer0SampleProject.Services;

[assembly: FunctionsStartup(typeof(Zer0SampleProject.Startup))]

namespace Zer0SampleProject
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<ProjectDbContext>(
             
            //I have left the connection string in here for the ability to test locally. Normally, I would resolve this through Azure KeyVault secrets.
            options => options.UseSqlServer(
                @"Server=tcp:zer0db.database.windows.net,1433;Initial Catalog=ProjectDatabase;Persist Security Info=False;User ID=zer0;Password=Pineapples! ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            builder.Services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
