using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace WPFilmweb.DAL.Encje
{
    public class Graja_w
    {
        #region Properties
        public int IDmovie { get; set; }
        public int IDactor { get; set; }
        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Graja_w(MySqlDataReader reader)
        {
            IDmovie = int.Parse(reader["IDfilmu"].ToString());
            IDactor = int.Parse(reader["IDaktora"].ToString());
        }

        public Graja_w(int movieId, int actorId)
        {
            IDmovie = movieId;
            IDactor = actorId;
        }
        #endregion
    }
}
