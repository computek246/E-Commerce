namespace eCommerce.Common.Constant
{
    public static class AppValues
    {
        public const string InvalidData = "Invalid data";
        public const string NotFound = "This {0} not found";
        public const string AlreadyExist = "This Name is already exist";

        public const string AuthenticationFailed =
            "The request has not been applied because it lacks valid authentication credentials for the target resource";

        

        public static class AppClaims
        {
            public const string Id = "Id";
            public const string UserName = "UserName";
            public const string Email = "Email";
            public const string Roles = "Roles";
            public const string User = "User";
            public const string FullName = "FullName";
        }

        public static class AppRoles
        {
            public const string Admin = "Admin";
        }
        
    }
}