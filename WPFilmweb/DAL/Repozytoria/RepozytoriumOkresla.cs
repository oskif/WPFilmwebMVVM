using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumOkresla
    {
        #region Queries
        private const string GET_DEFINING_GENRES = "SELECT * FROM okresla";
        private const string ADD_GENRE = "INSERT INTO okresla(IDfilmu, IDgatunku) VALUES";
        #endregion

        public static ObservableCollection<Okresla> GetDefiningGenres()
        {
            var definingGenres = new ObservableCollection<Okresla>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_DEFINING_GENRES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    definingGenres.Add(new Okresla(reader));
                connection.Close();
            }
            return definingGenres;
        }

        public static bool AddGenre(Okresla genre)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_GENRE} ({genre.IDmovie}, {genre.IDgenre})", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                genre.IDgenre = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }
    }
}