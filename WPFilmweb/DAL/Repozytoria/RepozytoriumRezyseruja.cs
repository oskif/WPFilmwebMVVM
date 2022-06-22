using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumRezyseruja
    {
        #region Queries
        private const string GET_MOVIE_DIRECTORS = "SELECT * FROM rezyseruja";
        private const string ADD_DIRECT = "INSERT INTO rezyseruja(IDfilmu, IDrezysera) VALUES";
        #endregion

        public static ObservableCollection<Rezyseruja> GetMovieDirectors()
        {
            var movieDirectors = new ObservableCollection<Rezyseruja>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_MOVIE_DIRECTORS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    movieDirectors.Add(new Rezyseruja(reader));
                connection.Close();
            }
            return movieDirectors;
        }

        public static bool AddDirect(Rezyseruja direct)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_DIRECT} ({direct.IDmovie}, {direct.IDdirector})", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                direct.IDdirector = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }
    }
}