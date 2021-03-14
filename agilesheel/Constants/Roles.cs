using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Constants
{
    public enum Roles
    {
        Admin,
        Manager,
        Basic
    }

    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }
        public static class Tickets
        {
            public const string View = "Permissions.Tickets.View";
            public const string Create = "Permissions.Tickets.Create";
            public const string Edit = "Permissions.Tickets.Edit";
            public const string Delete = "Permissions.Tickets.Delete";
        }
    }
}
