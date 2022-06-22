using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WPFilmweb.DAL.Encje;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Repozytoria
{
    class RepozytoriumAktorzy
    {
        #region Queries
        private const string GET_EVERY_ACTOR = "SELECT * FROM aktorzy";
        private const string ADD_ACTOR = "INSERT INTO aktorzy(imie, nazwisko, data_urodzenia, biografia, zdjecie) VALUES (@i, @n, @d, @b, @z)";
        #endregion

        #region CRUD methods
        public static ObservableCollection<Aktorzy> GetAllActors()
        {
            ObservableCollection<Aktorzy> actors = new ObservableCollection<Aktorzy>();
            using(var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_EVERY_ACTOR, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while(reader.Read())
                    actors.Add(new Aktorzy(reader));
                connection.Close();
            }
            return actors;
        }

        public static bool AddActor(Aktorzy actor)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_ACTOR}", connection);
                command.Parameters.Add("@i", MySqlDbType.VarChar);
                command.Parameters.Add("@n", MySqlDbType.VarChar);
                command.Parameters.Add("@d", MySqlDbType.Date);
                command.Parameters.Add("@b", MySqlDbType.VarChar);
                command.Parameters.Add("@z", MySqlDbType.Blob);
                command.Parameters["@i"].Value = actor.Name;
                command.Parameters["@n"].Value = actor.Surname;
                command.Parameters["@d"].Value = actor.BirthDate;
                command.Parameters["@b"].Value = actor.Bio;
                command.Parameters["@z"].Value = actor.ActorImage;
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                actor.IDActor = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool EditActor(Aktorzy actor, int IDActor)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_ACTOR = $"UPDATE aktorzy SET imie='{actor.Name}', nazwisko='{actor.Surname}', data_urodzenia='{actor.BirthDate}', " +
                    $"biografia='{actor.Bio}', zdjecie='{actor.ActorImage}' WHERE IDaktora = {IDActor}";
                MySqlCommand command = new MySqlCommand(EDIT_ACTOR, connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                if(id == 1)
                    state = true;
                
                connection.Close();
            }
            return state;
        }

        public static bool DeleteActor(Aktorzy actor)
        {
            bool state = false;
            using(var connection = DBConnection.Instance.Connection)
            {
                string DELETE_ACTOR = $"DELETE FROM aktorzy WHERE IDaktora = {actor.IDActor}";
                MySqlCommand command = new MySqlCommand(DELETE_ACTOR, connection);
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
