using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumOceniaja
    {
        #region Queries
        private const string ALL_REVIEWS = "SELECT * FROM oceniaja";
        
        #endregion

        public static ObservableCollection<Oceniaja> GetAllReviews()
        {
            var reviews = new ObservableCollection<Oceniaja>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_REVIEWS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    reviews.Add(new Oceniaja(reader));
                connection.Close();
            }
            return reviews;
        }

        public static bool UpdateGrade(int movieId, int userId, int grade)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"UPDATE oceniaja SET Wartosc={grade} WHERE IDfilmu={movieId}" +
                    $" AND IDuzytkownika={userId};", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                connection.Close();
            }
            return state;
        }
        public static bool AddGrade(int movieId, int userId, int grade)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"INSERT INTO oceniaja (IDfilmu, IDuzytkownika, wartosc) " +
                    $"VALUES({movieId},{userId},{grade});",connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                connection.Close();
            }
            return state;
        }
    }
}