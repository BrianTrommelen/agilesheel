using System.Collections.Generic;

namespace agilesheel.Constants
{
    public enum Roles
    {
        Admin,
        Manager,
        Basic,
        Cashier
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

        public static class Movies
        {
            public const string View = "Permissions.Movies.View";
            public const string Create = "Permissions.Movies.Create";
            public const string Edit = "Permissions.Movies.Edit";
            public const string Delete = "Permissions.Movies.Delete";
        }

        public static class TextBar
        {
            public const string Edit = "Permissions.TextBar.Edit";
        }
    }
}
