using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrBaschet.Domain.Entities;

namespace FrBaschet.Infrastructure.Data
{
    public static class RoleHelper
    {
        public static readonly string Commissioner = nameof(Commissioner).ToUpper();
        public static readonly string FrbUser = nameof(FrbUser).ToUpper();
        public static readonly string Referee = nameof(Referee).ToUpper();

        public static IEnumerable<string> GetRoles()
        {
            return typeof(RoleHelper)
                .GetFields(BindingFlags.Static | BindingFlags.Public).Select(a => a.Name);
        }

        public static ApplicationUser CreateUser(string userType)
        {
            if (userType == Commissioner) return new CommissionerEntity();
            if (userType == Referee) return new RefereeEntity();
            if (userType == FrbUser) return new FrbUserEntity();
            throw new Exception("UserTypeNotFound");
        }
        public static string GetRole(string userType)
        {
            if (userType == Commissioner) return Commissioner;
            if (userType == Referee) return FrbUser;
            if (userType == FrbUser) return Referee;
            throw new Exception("UserTypeNotFound");
        }
    }
}