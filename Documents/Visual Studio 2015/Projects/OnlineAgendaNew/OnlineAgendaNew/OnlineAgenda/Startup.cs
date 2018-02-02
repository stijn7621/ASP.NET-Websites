using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineAgenda.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineAgenda.Startup))]
namespace OnlineAgenda
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }
        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Database.Connection.Open();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Student"))
            {
                roleManager.Create(new IdentityRole()
                {
                    Name = "Student"
                });
            }

            if (!roleManager.RoleExists("Docent"))
            {
                roleManager.Create(new IdentityRole()
                {
                    Name = "Docent"
                });

            }
        }
    }
}
