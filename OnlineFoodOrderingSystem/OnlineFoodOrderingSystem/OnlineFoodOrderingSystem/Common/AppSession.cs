namespace OnlineFoodOrderingSystem.Common
{
    public static class AppSession
    {
        public static int UserId { get; set; }
        public static string FullName { get; set; }
        public static string Email { get; set; }
        public static string Role { get; set; }

        public static bool IsAdmin
        {
            get { return Role == "Admin"; }
        }

        public static void Clear()
        {
            UserId = 0;
            FullName = null;
            Email = null;
            Role = null;
        }
    }
}