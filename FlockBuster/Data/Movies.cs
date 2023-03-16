using System.Reflection.Metadata;

namespace FlockBuster.Data
{
    public class Movies
    {
        public int filmID { get; set; }
        public int antalFilm { get; set; }
        public string Navn { get; set; }
        public int ReleaseYear { get; set; }
        public int Alderbeg { get; set; }
        public string beskrivelse { get; set; }
        public string Længde { get; set; }
        public string Images { get; set; }
        public Movies()
        {

        }
        public Movies(int FilmID, int antalfilm, string navn, int UdgivningsÅr, int alderbeg, string Beskrivelse, string længde, string images)
        {
            filmID = FilmID;
            antalFilm = antalfilm;
            Navn = navn;
            ReleaseYear = UdgivningsÅr;
            Alderbeg = alderbeg;
            beskrivelse = Beskrivelse;
            Længde = længde;
            Images = images;
        }
    }

}
