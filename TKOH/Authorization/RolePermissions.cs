namespace TKOH.Authorization
{
    public static class RolePermissions
    {
        public static List<string> GetCreatableRolesFor(string creatorRole)
        {
            switch (creatorRole.ToLower())
            {
                case "owner":
                case "admin":
                    return new List<string> { "admin", "evaluator", "worker", "student" };

                case "evaluator":
                    return new List<string> { "worker", "student" };

                default:
                    return new List<string>();
            }
        }
    }
}
