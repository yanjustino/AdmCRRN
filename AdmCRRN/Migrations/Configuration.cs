namespace AdmCRRN.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using AdmCRRN.Models.Transporte;

    internal sealed class Configuration : DbMigrationsConfiguration<AdmCRRN.Context.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AdmCRRN.Context.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!Roles.GetAllRoles().Contains(RegisterModel.SUPER))
                Roles.CreateRole(RegisterModel.SUPER);

            if (!Roles.GetAllRoles().Contains(RegisterModel.ADMIN))
                Roles.CreateRole(RegisterModel.ADMIN);

            if (!Roles.GetAllRoles().Contains(RegisterModel.USUARIO))
                Roles.CreateRole(RegisterModel.USUARIO);

            if (Membership.GetUser("admcrrn") == null)
            {
                Membership.CreateUser("admcrrn", "admcrrn");
                Roles.AddUserToRole("admcrrn", RegisterModel.SUPER);
            }
        }
    }
}
