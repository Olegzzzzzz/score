using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ToyStore.Areas.Access.Models;

namespace ToyStore.Areas.Access.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] role = new string[] { };
            using (ToyStoreAccessContext db = new ToyStoreAccessContext())
            {
                // Получаем пользователя
                TBuyer user = db.TBuyers.FirstOrDefault(u => u.Firstname == username);
                if (user != null)
                {
                    // получаем роль
                    TRole userRole = db.TRoles.Find(user.C_TRole);
                    if (userRole != null)
                        role = new string[] { userRole.Name };
                }
            }
            return role;
        }
        public override void CreateRole(string roleName)
        {
            TRole newRole = new TRole() { Name = roleName };
            ToyStoreAccessContext db = new ToyStoreAccessContext();
            db.TRoles.Add(newRole);
            db.SaveChanges();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (ToyStoreAccessContext db = new ToyStoreAccessContext())
            {
                // Получаем пользователя
                TBuyer user = db.TBuyers.FirstOrDefault(u => u.Firstname == username);
                if (user != null)
                {
                    // получаем роль
                    TRole userRole = db.TRoles.Find(user.C_TRole);
                    //сравниваем
                    if (userRole != null && userRole.Name == roleName)
                        outputResult = true;
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}