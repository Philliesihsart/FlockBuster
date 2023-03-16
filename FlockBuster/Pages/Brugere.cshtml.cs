using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace FlockBuster.Pages
{
    public class Index1Model : PageModel
    {
        private readonly Connection _connection;

        public Index1Model(Connection connection)
        {
            _connection = connection;
        }
        

        public List<Users> brugerliste = new List<Users>();

        public int UserID { get; set; }
        public void OnGet()
        {
            try
            {
                brugerliste = _connection.GetAllUsers();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult OnPostDelete()
        {
            try
            {
                _connection.Deleteuser(UserID);
                return RedirectToPage("/Brugere");
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }

}
