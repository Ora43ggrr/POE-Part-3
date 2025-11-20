namespace POE_Part_3.Models
{
    public class User
    {
        public int Id { get; set; } // User ID
        public string Username { get; set; } // User's full name
        public string Email { get; set; } // User's email address
        public string Password { get; set; } // User's password
        public string Role { get; set; } // User's role (Lecturer, ProgrammeCoordinator, AcademicManager)
    }
}