using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlockBuster.Pages
{
    public class MoviesModel : PageModel
    {
        private readonly Connection _connection;

        public MoviesModel(Connection connection)
        {
            _connection = connection;
        }
        public int FilmID { get; set; }
        public string Navn { get; set; }
        public int Udgivnings≈r { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            Movies foundmovie = new Movies();
            foundmovie = _connection.GetMovies(FilmID);
            if (foundmovie == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                return RedirectToPage("/BrugerInfo", new { FilmID = foundmovie.filmID });
            }
        }
    }
}
