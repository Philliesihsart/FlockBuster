using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlockBuster.Pages
{
    [BindProperties]
    public class EditMoviesModel : PageModel
    {
        #region props
        [BindProperty(SupportsGet = true)]
        public int FilmID { get; set; }
        public int filmAntal { get; set; }
        public string filmNavn { get; set; }
        public int alderbeg { get; set; }
        public string filmDesc { get; set; }

        #endregion

        #region connectionstring
        //connectionstring til DB

        private readonly Connection _connection;
        public string successmessage = "", errormessage = "";
        public EditMoviesModel(Connection connection)
        {
            _connection = connection;
        }
        #endregion

        #region OnGet
        public void OnGet()
        {
            Movies foundmovie = new Movies();
            foundmovie = _connection.GetMovies(FilmID);
            filmAntal = foundmovie.antalFilm;
            filmNavn = foundmovie.Navn;
            alderbeg = foundmovie.Alderbeg;
            filmDesc = foundmovie.beskrivelse;
        }
        #endregion

        #region OnPost
        public IActionResult OnPost()
        {
            List<string> navn = new List<string>();
            try
            {
                navn = _connection.CheckForNameForMovies();
                string foundnavn = navn.Find(x => x == filmNavn);

                if (foundnavn != null)
                {
                    errormessage = "film navn eksistere allerede";
                }

                else
                {
                    _connection.RedigerFilm(FilmID, filmAntal, filmNavn, alderbeg, filmDesc);
                    successmessage = "Bruger opdateret";
                }
            }
            finally { Page(); }
            return Page();
        }
        #endregion
    }
}
