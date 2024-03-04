namespace CustomCookieBased.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // navigation property
        public List<AppUserRole> UserRoles { get; set; }
    }

    public class AppRole
    {
        public int Id { get; set; }
        public string Definition { get; set; }

        // navigation property
        public List<AppUserRole> UserRoles { get; set; }
    }

    public class AppUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // navigation properties
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }
    }
}
