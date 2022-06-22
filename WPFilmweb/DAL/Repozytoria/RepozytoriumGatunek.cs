using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumGatunek
    {
        public const string GET_ALL_GENRES = "SELECT * FROM gatunek";
        public const string ADD_GENRE = "INSERT INTO 'gatunek'('nazwa') VALUES";

        public static ObservableCollection<Gatunek> GetGenres()
        {
            var genres = new ObservableCollection<Gatunek>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL_GENRES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    genres.Add(new Gatunek(reader));
                connection.Close();
            }
            return genres;
        }
        
        public static bool AddGenre(Gatunek genre)
        {
            bool state = false;
            using(var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_GENRE} {genre.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                genre.IDgenre = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }
        public static bool EditGenre(Gatunek genre, int IDgenre)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_ACTOR = $"UPDATE gatunek SET nazwa='{genre.Name}' WHERE IDgatunku = {IDgenre}";
                MySqlCommand command = new MySqlCommand(EDIT_ACTOR, connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                if (id == 1)
                    state = true;

                connection.Close();
            }
            return state;
        }
        public static bool DeleteGenre(Gatunek genre)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_GENRE = $"DELETE FROM gatunek WHERE IDgatunku = '{genre.IDgenre}'";
                MySqlCommand command = new MySqlCommand(DELETE_GENRE, connection);
                var id = command.ExecuteNonQuery();
                if (id == 1)
                    state = true;

                connection.Close();
            }
            return state;
        }
    }
}
