using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WPFilmweb.DAL.Encje
{
    public class Aktorzy
    {
        #region Properties
        public int? IDActor { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public string Bio { get; set; }
        public byte[] ActorImage { get; set; }
        #endregion

        #region Constructors
        // MySqlDataReader constructor - creates object based on MySql data
        public Aktorzy(MySqlDataReader reader)
        {
            IDActor = int.Parse(reader["IDaktora"].ToString());
            Name = reader["imie"].ToString();
            Surname = reader["nazwisko"].ToString();
            BirthDate = reader["data_urodzenia"].ToString();
            Bio = reader["biografia"].ToString();
            if (reader["zdjecie"] == DBNull.Value)
                ActorImage = null;
            else
                ActorImage = (byte[])reader["zdjecie"];
        }
        // New object ctcreated from scratch, to add into database
        public Aktorzy(string name, string surname, string birthdate, string bio, byte[] actorImage)
        {
            IDActor = null;
            Name = name.Trim();
            Surname = surname.Trim();
            BirthDate = birthdate.Trim();
            Bio = bio.Trim();
            ActorImage = actorImage;
        }

        //Copty ctor
        public Aktorzy(Aktorzy actor)
        {
            IDActor = actor.IDActor;
            Name = actor.Name;
            Surname = actor.Surname;
            BirthDate = actor.BirthDate;
            Bio = actor.Bio;
            ActorImage = actor.ActorImage;
        }
        #endregion

        #region Methods
        public string ToInsert()
        {
            return $"('{Name}', '{Surname}', '{BirthDate}', '{Bio}', '{ActorImage}')";
        }

        // Override Equals method to check if actor is not duplicated with Contains function
        public override bool Equals(object obj)
        {
            var Actor = obj as Aktorzy;
            if (Actor is null) return false;
            if(Name.ToLower() != Actor.Name.ToLower()) return false;
            if(Surname.ToLower() != Actor.Surname.ToLower()) return false;
            if(BirthDate.ToLower() != Actor.BirthDate.ToLower()) return false;
            if(Bio.ToLower() != Actor.Bio.ToLower()) return false;
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
