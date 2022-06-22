using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Encje
{
    public class Uzytkownicy
    {
        #region Properties
        public int IDUser { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Uzytkownicy(MySqlDataReader reader)
        {
            IDUser = int.Parse(reader["IDuzytkownika"].ToString());
            Nickname = reader["nazwa_uzytkownika"].ToString();
            Password = reader["haslo"].ToString();
            Email = reader["adres_email"].ToString();
            AccountType = reader["typ_konta"].ToString();
        }
        #endregion
    }
}
