using FlockBuster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlockBuster.Pages
{
    public class testpageModel : PageModel
    {
        public List<Movies> movielist = new List<Movies>();
        public int FilmID { get; set; }
        public string Navn { get; set; }
        public int Udgivnings≈r { get; set; }
        public int alderbeg { get; set;}
        private readonly Connection _connection;

        public testpageModel(Connection connection)
        {
            _connection = connection;
        }
       
        public void OnGet()
        {
            try
            {
                movielist = _connection.GetAllMoviesForList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
