using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlockBuster.SessionHelper;
namespace FlockBuster.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Connection _connection;

        public LoginModel(Connection connection)
        {
            _connection = connection;
        }
        string successmessage = "";
        string errormessage = "";
        //[BindProperty]
        //public Credential Credential { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Users founduser = new Users();
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                errormessage = "Alle felter skal være udfyldt";
                return RedirectToPage("/Login");
            }
            if (founduser == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                founduser = _connection.LoginForUsers(Email, Password);
                HttpContext.Session.SetSessionString(Email, "Email");
                return RedirectToPage("/Index");
                
                                

            }
        }

    }

    //public class Credential
    //{
    //    [Required]
    //    public string Brugernavn { get; set; }
    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Adgangskode { get; set; }
    //}
}
