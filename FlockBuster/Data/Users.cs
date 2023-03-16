using Microsoft.AspNetCore.Identity;

namespace FlockBuster.Data
{
    public class Users
    {
        public int UserID { get; set; }
        public string Navn { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Users()
        {

        }
        public Users (int userID, string navn, string email, string pass)
        {
            UserID = userID;
            Navn = navn;
            Email = email;
            Password = pass;
        }
    }
}
