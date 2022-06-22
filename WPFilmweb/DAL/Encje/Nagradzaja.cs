using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace WPFilmweb.DAL.Encje
{
    public class Nagradzaja
    {
        #region Properties
        public int IDmovie { get; set; }
        public int IDaward { get; set; }
        #endregion 

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Nagradzaja(MySqlDataReader reader)
        {
            IDmovie = int.Parse(reader["IDfilmu"].ToString());
            IDaward = int.Parse(reader["IDnagrody"].ToString());
        }

        public Nagradzaja(int movieId, int awardId)
        {
            IDmovie = movieId;
            IDaward = awardId;
        }
        #endregion



    }
}
