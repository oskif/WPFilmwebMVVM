using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WPFilmweb.DAL.Encje;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumFilmy
    {
        private const string GET_ALL_MOVIES = "SELECT * FROM filmy";
        private const string ADD_MOVIE = "INSERT INTO filmy(tytul,rok_wydania,czas_trwania,opis,plakat) VALUES (@tyt, @rok, @czas, @op, @pla)";
        public static List<Filmy> GetMovies()
        {
            List<Filmy> movies = new List<Filmy>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL_MOVIES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    movies.Add(new Filmy(reader));
                connection.Close();
            }
            return movies;
        }
        public static bool AddMovie(Filmy movie)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_MOVIE}", connection);
                command.Parameters.Add("@tyt", MySqlDbType.VarChar);
                command.Parameters.Add("@rok", MySqlDbType.Int64);
                command.Parameters.Add("@czas", MySqlDbType.Time);
                command.Parameters.Add("@op", MySqlDbType.VarChar);
                command.Parameters.Add("@pla", MySqlDbType.Blob);
                command.Parameters["@tyt"].Value = movie.Title;
                command.Parameters["@rok"].Value = movie.ReleaseYear;
                command.Parameters["@czas"].Value = TimeSpan.Parse(movie.Length);
                command.Parameters["@op"].Value = movie.Description;
                command.Parameters["@pla"].Value = movie.Poster;
                connection.Open();
                Console.WriteLine(command.CommandText);
                var id = command.ExecuteNonQuery();
                state = true;
                movie.IDmovie = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool EditMovie(Filmy movie, int IDmovie)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_MOVIE = $"UPDATE filmy SET tytul={movie.Title}, rok_wydania='{movie.ReleaseYear}', " +
                    $"czas_trwania='{movie.Length}', opis = '{movie.Description}', plakat='{movie.Poster}' WHERE IDfilmu = {IDmovie}";
                MySqlCommand command= new MySqlCommand(EDIT_MOVIE, connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                if (id == 1)
                    state = true;
                connection.Close();
            }
            return state;
        }
        public static bool DeleteMovie(Filmy movie)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_MOVIE = $"DELETE FROM filmy WHERE filmy.tytul = '{movie.Title}'";
                MySqlCommand command= new MySqlCommand(DELETE_MOVIE, connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                if (id == 1)
                    state = true;

                connection.Close();
            }
            return state;
        }

    }
}
