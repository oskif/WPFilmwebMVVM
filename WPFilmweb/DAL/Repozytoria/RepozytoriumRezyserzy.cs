using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumRezyserzy
    {
        #region Queries
        private const string GET_EVERY_DIRECTOR = "SELECT * FROM rezyserzy";
        private const string ADD_DIRECTOR = "INSERT INTO rezyserzy(imie, nazwisko, data_urodzenia, biografia, zdjecie) VALUES (@i, @n, @d, @b, @z)";
        #endregion

        #region CRUD methods
        public static ObservableCollection<Rezyserzy> GetAllDirectors()
        {
            ObservableCollection<Rezyserzy> directors = new ObservableCollection<Rezyserzy>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_EVERY_DIRECTOR, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    directors.Add(new Rezyserzy(reader));
                connection.Close();
            }
            return directors;
        }

        public static bool AddDirector(Rezyserzy director)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_DIRECTOR}", connection);
                connection.Open();
                command.Parameters.Add("@i", MySqlDbType.VarChar);
                command.Parameters.Add("@n", MySqlDbType.VarChar);
                command.Parameters.Add("@d", MySqlDbType.Date);
                command.Parameters.Add("@b", MySqlDbType.VarChar);
                command.Parameters.Add("@z", MySqlDbType.Blob);
                command.Parameters["@i"].Value = director.Name;
                command.Parameters["@n"].Value = director.Surname;
                command.Parameters["@d"].Value = director.Birthdate;
                command.Parameters["@b"].Value = director.Bio;
                command.Parameters["@z"].Value = director.DirectorImage;
                var id = command.ExecuteNonQuery();
                state = true;
                director.IDDirector = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool EditDirector(Rezyserzy director, int IDDirector)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_DIRECTOR = $"UPDATE rezyserzy SET imie='{director.Name}', nazwisko='{director.Surname}', data_urodzenia='{director.Birthdate}', " +
                    $"biografia='{director.Bio}', zdjecie='{director.DirectorImage}' WHERE IDrezysera = {IDDirector}";
                MySqlCommand command = new MySqlCommand(EDIT_DIRECTOR, connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                if (id == 1)
                    state = true;

                connection.Close();
            }
            return state;
        }

        public static bool DeleteDirector(Rezyserzy director)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_DIRECTOR = $"DELETE FROM rezyserzy WHERE IDrezysera = {director.IDDirector}";
                MySqlCommand command = new MySqlCommand(DELETE_DIRECTOR, connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                if (id == 1)
                    state = true;

                connection.Close();
            }
            return state;
        }
        #endregion
    }
}
