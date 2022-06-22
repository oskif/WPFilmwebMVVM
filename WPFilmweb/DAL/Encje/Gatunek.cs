using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WPFilmweb.DAL.Encje
{
    public class Gatunek
    {
        #region Properties
        public int? IDgenre { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Gatunek(MySqlDataReader reader)
        {
            IDgenre = int.Parse(reader["IDgatunku"].ToString());
            Name = reader["nazwa"].ToString();
        }
        // New object created from scratch, to add into database
        public Gatunek(string name)
        {
            IDgenre = null;
            Name = name;
        }
        //Copty ctor
        public Gatunek(Gatunek genre)
        {
            IDgenre = genre.IDgenre;
            Name = genre.Name;
        }
        #endregion

        #region Methods
        public string ToInsert()
        {
            return $"'{Name}'";
        }
        // Override Equals method to check if actor is not duplicated with Contains function
        public override bool Equals(object obj)
        {
            Gatunek genre = obj as Gatunek;
            if (genre == null) return false;
            if (Name.ToLower() != genre.Name.ToLower()) return false; 
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}";
        }
        #endregion
    }
}
