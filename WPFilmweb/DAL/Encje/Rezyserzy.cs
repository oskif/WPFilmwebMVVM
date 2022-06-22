using MySql.Data.MySqlClient;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System;
namespace WPFilmweb.DAL.Encje
{
    public class Rezyserzy
    {
        #region Properties
        public int? IDDirector { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthdate { get; set; }
        public string Bio { get; set; }
        public byte[] DirectorImage { get; set; }
        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Rezyserzy(MySqlDataReader reader)
        {
            IDDirector = int.Parse(reader["IDrezysera"].ToString());
            Name = reader["imie"].ToString();
            Surname = reader["nazwisko"].ToString();
            Birthdate = reader["data_urodzenia"].ToString(); 
            Bio = reader["biografia"].ToString();
            if (reader["zdjecie"] == DBNull.Value)
                DirectorImage = null;
            else
                DirectorImage = (byte[])reader["zdjecie"];
            
        }
        // New object ctcreated from scratch, to add into database
        public Rezyserzy(string name, string surname, string birthdate, string bio, byte[] image)
        {
            IDDirector = null;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Bio = bio;
            DirectorImage = image;
        }
        //Copty ctor
        public Rezyserzy(Rezyserzy director)
        {
            IDDirector = director.IDDirector;
            Name = director.Name;
            Surname = director.Surname;
            Birthdate = director.Birthdate;
            Bio = director.Bio;
            DirectorImage = director.DirectorImage;
        }
        #endregion

        #region Methods
        public string ToInsert()
        {
            return $"('{Name}', '{Surname}', '{Birthdate}', '{Bio}', '{DirectorImage}')";
        }
        // Override Equals method to check if actor is not duplicated with Contains function
        public override bool Equals(object obj)
        {
            Rezyserzy director = obj as Rezyserzy;
            if (director == null) return false;
            if (director.Name.ToLower() != Name.ToLower()) return false;
            if (director.Surname.ToLower() != Surname.ToLower()) return false;
            if (director.Birthdate.ToLower() != Birthdate.ToLower()) return false;
            if (director.Bio.ToLower() != Bio.ToLower()) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
        #endregion

    }
}
