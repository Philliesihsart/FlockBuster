using FlockBuster.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Xml;

namespace FlockBuster.Data
{

    public class Connection
    {

        private readonly string _connectionString;
        private readonly SqlConnection _sqlConnection;

        public Connection(string connection)
        {
            _connectionString = connection;
            _sqlConnection = new SqlConnection(_connectionString);
        }
        private SqlCommand MySqlCommand(string StoredProcedure)
        {
            SqlCommand myCommand = new(StoredProcedure)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = _sqlConnection
            };
            return myCommand;
        }

        public bool GetLogin(string usern, string passn)
        {
            bool LoginCorrect = false;
            string passdb, userdb;
            SqlCommand command = MySqlCommand("SPLogin");
            try
            {
                _sqlConnection.Open();
                SqlDataReader myReader = command.ExecuteReader();
                while (myReader.Read())
                {
                    userdb = myReader.GetString("Navn");
                    passdb = myReader.GetString("Adgangskode");
                    if (userdb == usern && passdb == passn)
                    {
                        LoginCorrect = true;
                        return LoginCorrect;
                    }
                    else
                        LoginCorrect = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return LoginCorrect;

        }

        public bool AddUser(string navn, string email, string pass)
        {
            bool usercreated = false;
            SqlCommand _command = MySqlCommand("SPOpretBruger");
            _command.Parameters.AddWithValue("@Navn", navn);
            _command.Parameters.AddWithValue("@Mail", email);
            _command.Parameters.AddWithValue("@Adgangskode", pass);
            try
            {
                _sqlConnection.Open();
                _command.ExecuteNonQuery();
                usercreated = true;
            }
            catch (Exception)
            {
                throw;

            }
            finally { _sqlConnection.Close(); }
            return usercreated;
        }

        public void Deleteuser(int UserID)
        {
            SqlCommand _command = MySqlCommand("SPDeleteUser");
            _command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                _sqlConnection.Open();
                _command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public List<Users> GetAllUsers()
        {

            List<Users> users = new List<Users>();
            SqlCommand _command = MySqlCommand("SPSelectUsers");
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new Users(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), null));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return users;
        }

        public List<string> CheckForMail()
        {
            List<string> mails = new List<string>();
            SqlCommand _command = MySqlCommand("SPCheckAllMail");
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    mails.Add(reader.GetString(0));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return mails;
        }

        public void RedigerBruger(int userID, string email, string navn, string pass)
        {
            SqlCommand command = MySqlCommand("SPRedigerBruger");
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@Navn", navn);
            command.Parameters.AddWithValue("@Mail", email);
            command.Parameters.AddWithValue("@Adgangskode", pass);
            try
            {
                _sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public Users GetUserByID(int userID)
        {
            SqlCommand command = MySqlCommand("SPGetUserByID");
            command.Parameters.AddWithValue("@UserID", userID);

            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users founduser = new Users(reader.GetInt32(0), reader.GetString(2), reader.GetString(1), reader.GetString(4));
                    return founduser;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return null;
        }

        public Users LoginForUsers(string email, string adgangskode)
        {
            SqlCommand command = MySqlCommand("SPLoginUser");
            command.Parameters.AddWithValue("@Adgangskode", adgangskode);
            command.Parameters.AddWithValue("@Mail", email);
            try
            {

                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (email == reader.GetString("Mail") && adgangskode == reader.GetString("Adgangskode"))
                    {
                        Users userinfo = new Users(reader.GetInt32("UserID"), reader.GetString("Navn"), reader.GetString("Mail"), reader.GetString("Adgangskode"));
                        return userinfo;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return null;

        }

        public Movies GetMovies(int FilmID)
        {
            SqlCommand command = MySqlCommand("SPGetMovieByFilmID");
            command.Parameters.AddWithValue("@FilmID", FilmID);

            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Movies foundmovie = new Movies(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetTimeSpan(6).ToString(), reader.GetString(7));
                    return foundmovie;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return null;
        }

        public List<Movies> GetAllMoviesForList()
        {

            List<Movies> movies = new List<Movies>();
            SqlCommand _command = MySqlCommand("SPGetAllMoviesForList");
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(
                        new Movies(
                        reader.GetInt32("FilmID"),
                        reader.GetInt32("antalFilm"),
                        reader.GetString("Navn"),
                        reader.GetInt32("Årstal"),
                        reader.GetInt32("AlderBegrænsning"),
                        reader.GetString("Beskrivelse"),
                        reader.GetString("time"),
                        reader.GetString("ImagePath")));
                    
                }
            }
            finally { _sqlConnection.Close(); }
            return movies;
        }

        public bool AddMoviesToDb(int? qty, string? navn, int? UdgivningsÅr, int? alderBeg, string? Desc, TimeSpan Time, string imagepath)
        {
            bool movieadded = false;
            SqlCommand _command = MySqlCommand("SPAddMovieToDb");
            _command.Parameters.AddWithValue("@antalFilm", qty);
            _command.Parameters.AddWithValue("@navn", navn);
            _command.Parameters.AddWithValue("@Årstal", UdgivningsÅr);
            _command.Parameters.AddWithValue("@AlderBeg", alderBeg);
            _command.Parameters.AddWithValue("@Beskrivelse", Desc);
            _command.Parameters.AddWithValue("@Længde", Time);
            _command.Parameters.AddWithValue("@imagepath", imagepath);
            try
            {
                _sqlConnection.Open();
                _command.ExecuteNonQuery();
                movieadded = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return movieadded;
        }

        public void RedigerFilm(int filmID, int antalFilm, string filmNavn, int alderbeg, string Desc)
        {
            SqlCommand command = MySqlCommand("SPEditMovies");
            command.Parameters.AddWithValue("@filmID", filmID);
            command.Parameters.AddWithValue("@antalFilm", antalFilm);
            command.Parameters.AddWithValue("@navn", filmNavn);
            command.Parameters.AddWithValue("@alderbeg", alderbeg);
            command.Parameters.AddWithValue("@filmDesc", Desc);
            try
            {
                _sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public List<string> CheckForNameForMovies()
        {
            List<string> navn = new List<string>();
            SqlCommand _command = MySqlCommand("SPGetAllNamesForMovieList");
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    Movies foundmovie = new Movies(reader.GetInt32("FilmID"), reader.GetInt32("antalFilm"), reader.GetString("Navn"), reader.GetInt32("Årstal"), reader.GetInt32("AlderBegrænsning"), reader.GetString("Beskrivelse"), reader.GetTimeSpan(6).ToString(), reader.GetString(7));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { _sqlConnection.Close(); }
            return navn;
        }
    }
}