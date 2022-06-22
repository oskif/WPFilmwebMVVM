using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumGraja_w
    {
        #region Queries
        private const string ALL_PLAYING_ACTORS = "SELECT * FROM graja_w";
        private const string ADD_PLAY_IN = "INSERT INTO graja_w(IDfilmu, IDaktora) VALUES";
        #endregion

        public static ObservableCollection<Graja_w> GetPlayingActors()
        {
            var playingActors = new ObservableCollection<Graja_w>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_PLAYING_ACTORS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    playingActors.Add(new Graja_w(reader));
                connection.Close();
            }
            return playingActors;
        }

        public static bool AddPlayIn(Graja_w playIn)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_PLAY_IN} ({playIn.IDmovie}, {playIn.IDactor})", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                playIn.IDactor = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }
    }
}
