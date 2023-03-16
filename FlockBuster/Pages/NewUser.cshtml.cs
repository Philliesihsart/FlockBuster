using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace FlockBuster.Pages
{
    public class NewUserModel : PageModel
    {
        private readonly Connection _connection;

        public NewUserModel(Connection connection)
        {
            _connection = connection;
        }
        [BindProperty]
        public string Navn { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Adgangskode { get; set; }
        public List<Users> brugereInfo = new List<Users>(); 
        public string errormessage = "";
        public string successmessage = "";
        
        public void OnGet()
        {

        }
        
        public void OnPost()
        {

            Navn = Request.Form["navn"];
            Email = Request.Form["email"];
            Adgangskode = Request.Form["adgangskode"];

            if (Navn.Length == 0 || Email.Length == 0 || Adgangskode.Length == 0)
            {
                errormessage = "Alle felter skal være udfyldt!";
                return;
            }
            List<string> mails = new List<string>();
            try
            {
                mails = _connection.CheckForMail();
                string foundmail = mails.Find(x => x == Email);
                if (foundmail == null)
                { 
                    _connection.AddUser(Navn, Email, Adgangskode);
                    Navn = ""; Email = ""; Adgangskode = "";
                    successmessage = "Bruger er nu oprettet";
                }

                else {
                    errormessage = "Mail eksisterer allerede";
                }
            }
            catch (Exception)
            {
                errormessage = "Mailen eksistere allerede";
                throw;
            }

        }
    }
}
