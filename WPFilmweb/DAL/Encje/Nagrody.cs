using MySql.Data.MySqlClient;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System;
namespace WPFilmweb.DAL.Encje
{
    public class Nagrody
    {
        #region Properties

        public int? IDAward { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] AwardImage { get; set; }

        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Nagrody(MySqlDataReader reader)
        {
            IDAward = int.Parse(reader["IDnagrody"].ToString());
            Name = reader["nazwa"].ToString();
            Description = reader["opis_nagrody"].ToString();
            if (reader["zdjecie"] == DBNull.Value)
                AwardImage = null;
            else
                AwardImage = (byte[])reader["zdjecie"];

        }

        // New object created from scratch, to add into database
        public Nagrody(string name, string description, byte[] image)
        {
            IDAward = null;
            Name = name;
            Description = description;
            AwardImage = image;
        }

        // Copy ctor
        public Nagrody(Nagrody award)
        {
            IDAward = award.IDAward;
            Name = award.Name;
            Description = award.Description;
            AwardImage = award.AwardImage;
        }
        #endregion

        #region Methods
        public string ToInsert()
        {
            return $"('{Name}','{Description}','{AwardImage}')";
        }
        // Override Equals method to check if actor is not duplicated with Contains function
        public override bool Equals(object obj)
        {
            Nagrody Award = obj as Nagrody;
            if (Award == null) return false;
            if (Award.Name.ToLower() != Name.ToLower()) return false;
            if(Award.Description.ToLower() != Description.ToLower()) return false;
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
