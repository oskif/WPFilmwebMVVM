using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace WPFilmweb.DAL.Encje
{
    public class Rezyseruja
    {
        #region Properties
        public int IDdirector { get; set; }
        public int IDmovie { get; set; }
        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Rezyseruja(MySqlDataReader reader)
        {
            IDdirector = int.Parse(reader["IDrezysera"].ToString());
            IDmovie = int.Parse(reader["IDfilmu"].ToString());
        }

        public Rezyseruja(int movieId, int directorId)
        {
            IDdirector = directorId;
            IDmovie = movieId;
        }
        #endregion
    }
}
