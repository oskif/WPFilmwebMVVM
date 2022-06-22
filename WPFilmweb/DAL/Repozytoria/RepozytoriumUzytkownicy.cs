using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;
namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumUzytkownicy
    {
        #region Queries
        private const string GET_EVERY_USERS = "SELECT * FROM uzytkownicy";
        private const string ADD_AWARD = "INSERT INTO 'uzytkownicy'( 'nazwa_uzytkownika', 'haslo', 'adres_email') VALUES";
        #endregion

        #region CRUD methods
        public static ObservableCollection<Uzytkownicy> GetAllUsers()
        {
            ObservableCollection<Uzytkownicy> users = new ObservableCollection<Uzytkownicy>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_EVERY_USERS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    users.Add(new Uzytkownicy(reader));
                connection.Close();
            }
            return users;
        }

        #endregion
    }

}