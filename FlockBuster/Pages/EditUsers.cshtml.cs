using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace FlockBuster.Pages
{
    [BindProperties]
    public class EditUsersModel : PageModel
    {
        private readonly Connection _connection;
        public string successmessage = "", errormessage = ""; 
        public EditUsersModel(Connection connection)
        {
            _connection = connection;
        }
        
        
        [BindProperty(SupportsGet = true)]
        public int UserID { get; set; }
        
        public string Navn { get; set; }
        
        public string Email { get; set; }
        public string Adgangskode { get; set; }
        

        public IActionResult OnGet()
        {
            Users founduser = new Users();
            founduser = _connection.GetUserByID(UserID);
            Email = founduser.Email;
            Navn = founduser.Navn;
            Adgangskode = founduser.Password;
            return Page();
        }
        public void OnPost()
        {
            List<string> mails = new List<string>();
            try
            {
                mails = _connection.CheckForMail();
                string foundmail = mails.Find(x => x == Email);
                if (Email == foundmail && foundmail == Navn && Adgangskode == Adgangskode)
                {
                    errormessage = "Intet data er ændret";
                }
                else if (foundmail != null)
                {
                    errormessage = "Email eksistere allerede";
                }

                else
                {
                    _connection.RedigerBruger(UserID, Email, Navn, Adgangskode);
                    successmessage = "Bruger opdateret";
                }

            }
           catch
            {
                throw;
            }
        }
    }
}
