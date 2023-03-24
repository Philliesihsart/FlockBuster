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
        public bool Login { get; set; } = true;


        public IActionResult OnPost()
        {
            Users founduser = new Users();
            founduser = _connection.LoginForUsers(Email, Password);
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                errormessage = "Alle felter skal være udfyldt";
                return Page();
            }
            if (founduser == null)
            {
                return Page();
            }
            else if (founduser.Email == Email && founduser.Password == Password)
            {
               
                HttpContext.Session.SetSessionString(Email, "Email");
                return RedirectToPage("/Index");
            }
            return Page();
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

