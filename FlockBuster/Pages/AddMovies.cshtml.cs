using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace FlockBuster.Pages
{
    [BindProperties]
    public class AddMoviesModel : PageModel
    {
        private readonly Connection _connection;

        public AddMoviesModel(Connection connection)
        {
            _connection = connection;
        }
        public string errormessage = "";
        public string successmessage = "";

        public int? alderBeg { get; set; }
        public int? antalFilm { get; set;}
        public string? navnFilm { get; set; }

        public int? årstalfilm { get; set; }

        public int? AlderBeg { get; set; }
        
        public string? desc { get; set; }
        public string imagepath { get; set; }
        
        public TimeSpan time { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if(antalFilm.Value <= 0 || navnFilm.Length == 0 || årstalfilm.Value <= 0 || AlderBeg.Value <= 0 || desc.Length == 0 || time == TimeSpan.MinValue)
            {
                errormessage = "alle felter skal være udfyldt!";
            }

            try
            {
                _connection.AddMoviesToDb(antalFilm, navnFilm, årstalfilm, alderBeg, desc, time, imagepath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
