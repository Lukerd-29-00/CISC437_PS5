using DOOR.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;


namespace DOOR.Server.Data
{
    public partial class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("UD_LUCASD")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            builder.ToUpperCaseTables();
            builder.ToUpperCaseColumns();
            builder.ToUpperCaseForeignKeys();


            // builder.AddFootprintColumns();
            builder.FinalAdjustments();




        }
    }
}