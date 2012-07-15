namespace AdmCRRN.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using AdmCRRN.Models.Transporte;
    using AdmCRRN.Models.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
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
