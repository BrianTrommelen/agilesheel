using System.Collections.Generic;

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

        public static class LostAndFound
        {
            public const string View = "Permissions.LostAndFound.View";
            public const string Create = "Permissions.LostAndFound.Create";
            public const string Edit = "Permissions.LostAndFound.Edit";
            public const string Delete = "Permissions.LostAndFound.Delete";
        }
    }
}
