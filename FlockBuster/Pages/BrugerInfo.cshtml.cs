using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlockBuster.Pages
{
    public class BrugerInfoModel : PageModel
    {
        private readonly Connection _connection;

        public BrugerInfoModel(Connection connection)
        {
            _connection = connection;
        }
        [BindProperty]
        public int UserID { get; set; }
        [BindProperty]
        public string Navn { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public int UID { get; set; }
        public void OnGet(int userID)
        {
            try
            {
                Users foundUser = new Users();
                foundUser = _connection.GetUserByID(userID);
                UserID = foundUser.UserID;
                Email = foundUser.Email;
                Navn = foundUser.Navn;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult OnGetEdit()
        {
            Users founduser = new Users();
            founduser = _connection.GetUserByID(UserID);
            Email = founduser.Email;
            Navn = founduser.Navn;
            return RedirectToPage("/BrugerInfo");
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
