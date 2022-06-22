using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace WPFilmweb.Model
{
    using DAL.Encje;
    using DAL.Repozytoria;
    
    using System.Collections.ObjectModel;

    public class Model
    {
        #region Properties

        #region Movies

            public ObservableCollection<Filmy> MoviesList { get; set; } = new ObservableCollection<Filmy>();
            public ObservableCollection<string> MoviesVisibility { get; set; } = new ObservableCollection<string>();
            public ObservableCollection<string> MoviesTitles { get; set; } = new ObservableCollection<string>();

            private Filmy emptyMovie = new Filmy("", 0, "", "", null);

        #endregion

        #region Actors

            public ObservableCollection<Aktorzy> ActorList { get; set; } = new ObservableCollection<Aktorzy>();
            public ObservableCollection<Graja_w> ActorsMoviesList { get; set; } = new ObservableCollection<Graja_w>();
            public ObservableCollection<string> ActorsVisibility { get; set; } = new ObservableCollection<string>();
            public ObservableCollection<string> ActorNames { get; set; } = new ObservableCollection<string>();

            private Aktorzy emptyActor = new Aktorzy("", "", "", "", null);

        #endregion

        #region Directors

            public ObservableCollection<Rezyserzy> DirectorsList { get; set; } = new ObservableCollection<Rezyserzy>();
            public ObservableCollection<Rezyseruja> DirectorsMoviesList { get; set; } = new ObservableCollection<Rezyseruja>();
            public ObservableCollection<string> DirectorsVisibility { get; set; } = new ObservableCollection<string>();
            public ObservableCollection<string> DirectorNames { get; set; } = new ObservableCollection<string>();

            private Rezyserzy emptyDirector = new Rezyserzy("", "", "", "", null);
        #endregion

        #region Users

            public ObservableCollection<Uzytkownicy> UsersList { get; set; } = new ObservableCollection<Uzytkownicy>();
            public ObservableCollection<Oceniaja> MoviesRatio { get; set; } = new ObservableCollection<Oceniaja>();

        #endregion

        #region Genres

            public ObservableCollection<Gatunek> Genres { get; set; } = new ObservableCollection<Gatunek>();
            public ObservableCollection<Okresla> MoviesAndGenresList { get; set; } = new ObservableCollection<Okresla>();

        #endregion

        #region Awards

            public ObservableCollection<Nagrody> AwardList { get; set; } = new ObservableCollection<Nagrody>();
            public ObservableCollection<Nagradzaja> MoviesAwards { get; set; } = new ObservableCollection<Nagradzaja>();
            public ObservableCollection<string> AwardsVisibility { get; set; } = new ObservableCollection<string>();
            

            private Nagrody emptyAward = new Nagrody("", "", null);

        #endregion

        #endregion

        #region Constructors

        public void resetModel()
        {

            GetMovies();
            MoviesVisibility.Clear();
            GetActors();
            GetActorsMovies();
            ActorsVisibility.Clear();
            GetDirectors();
            GetDirectorsMovies();
            DirectorsVisibility.Clear();
            GetGenres();
            MoviesAndGenres();
            GetAwards();
            GetMoviesAwards();
            GetUsers();
            GetMoviesRatio();
            GetMovieTitles();
            GetActorNames();
            GetDirectorNames();
        }
        private Model()
        {
            GetMovies();
            MoviesVisibility.Clear();
            GetActors();
            GetActorsMovies();
            ActorsVisibility.Clear();
            GetDirectors();
            GetDirectorsMovies();
            DirectorsVisibility.Clear();
            GetGenres();
            MoviesAndGenres();
            GetAwards();
            GetMoviesAwards();
            GetUsers();
            GetMoviesRatio();
            GetMovieTitles();
            GetActorNames();
            GetDirectorNames();
        }
        private static Model instance = null;
        public static Model getInstance()
        {
            if (instance == null)
                instance = new Model();
            return instance;
        }

        #endregion

        #region Methods

        #region Movies methods
        public void GetMovieTitles()
        {
            var movies = MoviesList;
            MoviesTitles.Clear();
            foreach (var movie in movies)
            {
                MoviesTitles.Add(movie.Title);
            }
        }
        public void AddMovie(string title, int releaseYear, string length, string description, byte[] image = null)
        {
            Filmy movie = new Filmy(title, releaseYear, length, description, image);
            RepozytoriumFilmy.AddMovie(movie);
            GetMovies();
            GetMovieTitles();
        }

        public void DeleteMovie(int index)
        {
            Filmy movie = MoviesList[index];
            RepozytoriumFilmy.DeleteMovie(movie);
        }

        public void GetMovies() // Filling up models movie list
            {
                var movies = RepozytoriumFilmy.GetMovies();
                MoviesList.Clear();
                foreach (var movie in movies)
                {
                    MoviesList.Add(movie);
                }
                MoviesList = new ObservableCollection<Filmy>(MoviesList.OrderBy(movie => movie.Title).ToList());
            }
            public void RefreshMovies(ObservableCollection<Filmy> movies, int n)
            {
                movies.Clear();
                MoviesVisibility.Clear();
                if (MoviesList.Count != 0)
                {
                     // this line is intentional. It's purpouse is clearing ObservableCollection passed as reference, that way it is always filled with 4 objects
                    MoviesVisibility.Clear();
                    for (int i = 4 * n - 4; i <= n * 3 + 1; i++)
                    {
                        movies.Add(MoviesList[i]);
                        MoviesVisibility.Add("Visible");
                        if (i == MoviesList.Count()-1)
                        {
                        Console.WriteLine("TEST");
                            for (int z = i; z <= n * 3 + 2; z++)
                            {
                                movies.Add(emptyMovie);
                                MoviesVisibility.Add("Hidden");
                            }
                            break;
                        }
                        
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        movies.Add(emptyMovie);
                        MoviesVisibility.Add("Hidden");
                    }
                }
            }
            private Filmy FindMovieById(int id)
            {
                foreach (var m in MoviesList)
                {
                    if (m.IDmovie == id)
                        return m;
                }
                return null;
            }

            public Filmy FindMovieByTitle(string title)
            {
                foreach (var m in MoviesList)
                {
                    if (m.Title == title)
                        return m;
                }
                return null;
            }


            public ObservableCollection<string> GetRatingsForMovieList(ObservableCollection<Filmy> movies)
            {
                double grade = 0;
                int temp = 0;
                ObservableCollection<string> result = new ObservableCollection<string>();
                for (int i = 0; i < movies.Count; i++)
                {
                    if (movies[i].Title != "") // check if list isnt filled with empty movie
                    {
                        for (int j = 0; j < MoviesRatio.Count; j++)
                        {
                            if (movies[i].IDmovie == MoviesRatio[j].IDmovie)
                            {
                                grade += MoviesRatio[j].Grade;
                                temp++;
                            }
                        }
                        if (temp > 0)
                            result.Add((grade / temp).ToString("F"));
                        else
                            result.Add(0.ToString("F"));
                        grade = 0;
                        temp = 0;
                    }
                    else
                    {
                        result.Add("");
                    }
                }
                return result;
            }
            public void GetMoviesByTitle(string title)
            {
                if (string.IsNullOrEmpty(title))
                {
                    GetMovies();

                }
                else
                {
                    GetMovies();
                    ObservableCollection<Filmy> temp = new ObservableCollection<Filmy>();
                    MoviesVisibility.Clear();
                    foreach (var movie in MoviesList)
                    {
                        if (movie.Title.ToLower().Contains(title.ToLower()))
                        {
                            temp.Add(movie);
                            MoviesVisibility.Add("Visible");
                        }
                        else
                        {
                            MoviesVisibility.Add("Hidden");
                        }
                    }
                    MoviesList.Clear();
                    MoviesList = temp;
                }
            }
        public string GetMovieDirectors(Filmy movie)
        {
            string dir = "";
            foreach (var d in DirectorsMoviesList)
            {
                if (d.IDmovie == movie.IDmovie)
                {
                    dir += FindDirectorById(d.IDdirector).Name + " " + FindDirectorById(d.IDdirector).Surname + ", ";
                }
            }
            if (dir != String.Empty)
                return dir.Remove(dir.Length - 2);
            else
                return "Brak danych";
        }
        public string GetMovieCast(Filmy movie)
        {
            string cast = "";
            foreach (var m in ActorsMoviesList)
            {
                if (m.IDmovie == movie.IDmovie)
                {
                    cast += FindActorById(m.IDactor).Name + " " + FindActorById(m.IDactor).Surname + ", ";
                }
            }
            if (cast != String.Empty)
                return cast.Remove(cast.Length - 2);
            else
                return "Brak danych";
        }
        #endregion

        #region Actors methods
        public void AddActor(string name, string surname, string dateOfBirth, string bio, byte[] image = null)
        {
            Aktorzy actor = new Aktorzy(name, surname, dateOfBirth, bio, image);
            RepozytoriumAktorzy.AddActor(actor);
            GetActors();
            GetActorNames();
        }

        public void DeleteActor(int index)
        {
            Aktorzy actor = ActorList[index];
            RepozytoriumAktorzy.DeleteActor(actor);
        }

        public void GetActorNames()
        {
            var actors = ActorList;
            ActorNames.Clear();
            foreach (var actor in actors)
            {
                ActorNames.Add(actor.Name + " " + actor.Surname);
            }
        }


        public void GetActors()
            {
                var actors = RepozytoriumAktorzy.GetAllActors();
                ActorList.Clear();
                foreach (var actor in actors)
                {
                    ActorList.Add(actor);
                }
                ActorList = new ObservableCollection<Aktorzy>(ActorList.OrderBy(actor => (actor.Name + actor.Surname)).ToList());
            }
        
        public void GetActorsMovies()
            {
                var actorsMovies = RepozytoriumGraja_w.GetPlayingActors();
                ActorsMoviesList.Clear();
                foreach (var actorsMovie in actorsMovies)
                {
                    ActorsMoviesList.Add(actorsMovie);
                }
            }
            public void RefreshActors(ObservableCollection<Aktorzy> actors, int n)
            {
                ActorsVisibility.Clear();
                actors.Clear();
                if (ActorList.Count != 0)
                {
                    
                    actors.Clear(); // this line is intentional. It's purpouse is clearing ObservableCollection passed as reference, that way it is always filled with 4 objects
                    for (int i = 4 * n - 4; i <= n * 3 + 1; i++)
                    {
                        actors.Add(ActorList[i]);
                        ActorsVisibility.Add("Visible");
                        if (i == ActorList.Count()-1)
                        {
                            for (int z = i; z <= n * 3 + 2; z++)
                            {
                                actors.Add(emptyActor);
                                ActorsVisibility.Add("Hidden");
                            }
                            break;
                        }
                        
                    }
                }
                else
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        actors.Add(emptyActor);
                        ActorsVisibility.Add("Hidden");
                    }
                }
            }
            private Aktorzy FindActorById(int id)
            {
                foreach (var a in ActorList)
                {
                    if (a.IDActor == id)
                        return a;
                }
                return null;
            }
            public ObservableCollection<Aktorzy> GetActorsFromMovie(Filmy movie)
            {
                var actors = new ObservableCollection<Aktorzy>();
                foreach (var a in ActorsMoviesList)
                {
                    if (a.IDmovie == movie.IDmovie)
                    {
                        actors.Add(FindActorById(a.IDactor));
                    }
                }
                return actors;
            }
            public void GetActorsByName(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    GetActors();
                }
                else
                {
                    GetActors();
                    ObservableCollection<Aktorzy> temp = new ObservableCollection<Aktorzy>();
                    ActorsVisibility.Clear();
                    foreach (var actor in ActorList)
                    {
                        if ((actor.Name + " " + actor.Surname).ToLower().Contains(name.ToLower()))
                        {
                            temp.Add(actor);
                            ActorsVisibility.Add("Visible");
                        }
                        else
                        {
                            ActorsVisibility.Add("Hidden");
                        }
                    }
                    ActorList.Clear();
                    ActorList = temp;
                }
            }
            public ObservableCollection<Filmy> GetMoviesFromActor(Aktorzy actor)
            {
                var movies = new ObservableCollection<Filmy>(); //
                foreach (var a in ActorsMoviesList)
                {
                    if (a.IDactor == actor.IDActor)
                    {
                        movies.Add(FindMovieById(a.IDmovie));
                    }
                }
                return movies;
            }
        public string GetActorsMovies(Aktorzy actor)
        {
            string movies = "";
            foreach (var m in ActorsMoviesList)
            {
                if (m.IDactor == actor.IDActor)
                {
                    movies += FindMovieById(m.IDmovie).Title + ", ";
                }
            }
            if (movies != String.Empty)
                return movies.Remove(movies.Length - 2);
            else
                return "Brak danych";
        }
        #endregion

        #region Directos methods
        public void GetDirectorsName()
        {
            var directors = DirectorsList;
            DirectorNames.Clear();
            foreach (var director in directors)
            {
                DirectorNames.Add(director.Name + " " + director.Surname);
            }
        }

        public void AddDirector(string name, string surname, string dateOfBirth, string bio, byte[] image = null)
        {
            Rezyserzy director = new Rezyserzy(name, surname, dateOfBirth, bio, image);
            RepozytoriumRezyserzy.AddDirector(director);
            GetDirectors();
            GetDirectorsName();
        }

        public void DeleteDirector(int index)
        {
            Rezyserzy director = DirectorsList[index];
            RepozytoriumRezyserzy.DeleteDirector(director);
        }

        public void GetDirectorNames()
        {
            var directors = DirectorsList;
            DirectorNames.Clear();
            foreach (var director in directors)
            {
                DirectorNames.Add(director.Name + " " + director.Surname);
            }
        }

        public void GetDirectors()
            {
                var directors = RepozytoriumRezyserzy.GetAllDirectors();
                DirectorsList.Clear();
                foreach (var director in directors)
                {
                    DirectorsList.Add(director);
                }
                DirectorsList = new ObservableCollection<Rezyserzy>(DirectorsList.OrderBy(director => (director.Name + director.Surname)).ToList());
            }

            public void GetDirectorsMovies()
            {
                var directorMovies = RepozytoriumRezyseruja.GetMovieDirectors();
                DirectorsMoviesList.Clear();
                foreach (var directorMovie in directorMovies)
                {
                    DirectorsMoviesList.Add(directorMovie);
                }
            }
            public void RefreshDirectors(ObservableCollection<Rezyserzy> directors, int n)
            {
                DirectorsVisibility.Clear();
                directors.Clear();
                if (DirectorsList.Count != 0)
                {
                    
                    directors.Clear(); // this line is intentional. It's purpouse is clearing ObservableCollection passed as reference, that way it is always filled with 4 objects
                    for (int i = 4 * n - 4; i <= n * 3 + 1; i++)
                    {
                        directors.Add(DirectorsList[i]);
                        DirectorsVisibility.Add("Visible");
                        if (i == DirectorsList.Count()-1)
                        {
                            for (int z = i; z <= n * 3 + 2; z++)
                            {
                                directors.Add(emptyDirector);
                                DirectorsVisibility.Add("Hidden");
                            }
                            break;
                        }
                        
                    }
                }
                else
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        directors.Add(emptyDirector);
                        DirectorsVisibility.Add("Hidden");
                    }
                }
            }
            public void GetDirectorsByName(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    GetDirectors();
                }
                else
                {
                    GetDirectors();
                    ObservableCollection<Rezyserzy> temp = new ObservableCollection<Rezyserzy>();
                    DirectorsVisibility.Clear();
                    foreach (var director in DirectorsList)
                    {
                        if ((director.Name + " " + director.Surname).ToLower().Contains(name.ToLower()))
                        {
                            temp.Add(director);
                            DirectorsVisibility.Add("Visible");
                        }
                        else
                        {
                            DirectorsVisibility.Add("Hidden");
                        }
                    }
                    DirectorsList.Clear();
                    DirectorsList = temp;
                }
            }
            private Rezyserzy FindDirectorById(int id)
            {
                foreach (var d in DirectorsList)
                {
                    if (d.IDDirector == id)
                        return d;
                }
                return null;
            }
            public ObservableCollection<Rezyserzy> GetDirectorsFromMovie(Filmy movie)
            {
                var directors = new ObservableCollection<Rezyserzy>();
                foreach (var d in DirectorsMoviesList)
                {
                    if (d.IDmovie == movie.IDmovie)
                    {
                        directors.Add(FindDirectorById(d.IDdirector));
                    }
                }
                return directors;
            }
            public ObservableCollection<Filmy> GetMoviesFromDirector(Rezyserzy director)
            {
                var movies = new ObservableCollection<Filmy>(); //
                foreach (var d in DirectorsMoviesList)
                {
                    if (d.IDdirector == director.IDDirector)
                    {
                        movies.Add(FindMovieById(d.IDmovie));
                    }
                }
                return movies;
            }
        public string GetDirectorsMovies(Rezyserzy director)
        {
            string movies = "";
            foreach (var m in DirectorsMoviesList)
            {
                if (m.IDdirector == director.IDDirector)
                {
                    movies += FindMovieById(m.IDmovie).Title + ", ";
                }
            }
            if (movies != String.Empty)
                return movies.Remove(movies.Length - 2);
            else
                return "Brak danych";
        }
        #endregion

        #region Users methods
        public void GetUsers()
                {
                    var users = RepozytoriumUzytkownicy.GetAllUsers();
                    UsersList.Clear();
                    foreach (var user in users)
                    {
                        UsersList.Add(user);
                    }
                    UsersList = new ObservableCollection<Uzytkownicy>(UsersList.OrderBy(user => user.Nickname).ToList());
                }
            public void UpdateAddGrade(int movieId, int userId, int grade)
                {
                    bool temp = true;
                    for (int i = 0; i < MoviesRatio.Count; i++)
                    {
                        if (MoviesRatio[i].IDmovie == movieId && MoviesRatio[i].IDuser == userId)
                        {
                            RepozytoriumOceniaja.UpdateGrade(movieId, userId, grade);
                            temp = false;
                            break;
                        }
                    }
                    if (temp)
                    {
                        RepozytoriumOceniaja.AddGrade(movieId, userId, grade);
                    }
                    MoviesRatio.Clear();
                    GetMoviesRatio();
                }
            public void GetMoviesRatio()
            {
                var moviesratio = RepozytoriumOceniaja.GetAllReviews();
                MoviesRatio.Clear();
                foreach (var mr in moviesratio)
                {
                    MoviesRatio.Add(mr);
                }
            }

            public string GetMovieRatio(Filmy m)
            {
                double result = 0;
                int temp = 0;
                for (int i = 0; i < MoviesRatio.Count; i++)
                {
                    if (MoviesRatio[i].IDmovie == m.IDmovie)
                    {
                        result += (MoviesRatio[i].Grade);
                        temp++;
                    }
                }
                // 
                if (temp > 0)
                    return "Ocena filmu: " + (result / temp).ToString("F");
                else
                    return "Ocena filmu: " + 0.ToString("F");
            }
            public int GetUserRatio(Filmy m, int user)
            {
                for (int i = 0; i < MoviesRatio.Count; i++)
                {
                    if (MoviesRatio[i].IDmovie == m.IDmovie && MoviesRatio[i].IDuser == user)
                    {
                        return int.Parse(MoviesRatio[i].Grade.ToString());
                    }
                }
                return 0;
            }
            public bool checkAdmin(int userID)
            {
                var users = UsersList;
                for (int i = 0; i < UsersList.Count(); i++)
                {
                    if (users[i].IDUser == userID && users[i].AccountType == "Administrator")
                        return true;
                }
                return false;
            }
        #endregion

        #region Genres methods
        public void GetGenres()
            {
                var genres = RepozytoriumGatunek.GetGenres();
                Genres.Clear();
                foreach (var genre in genres)
                {
                    Genres.Add(genre);
                }
            }
            public void MoviesAndGenres()
            {
                var MoviesAndGenres = RepozytoriumOkresla.GetDefiningGenres();
                MoviesAndGenresList.Clear();
                foreach (var mg in MoviesAndGenres)
                {
                    MoviesAndGenresList.Add(mg);
                }
            }
            private Gatunek FindGenreByID(int id)
            {
                foreach (var g in Genres)
                {
                    if (g.IDgenre == id)
                        return g;
                }
                return null;
            }
            public string GetMovieGenres(Filmy movie)
            {
                string genre = "";
                foreach (var g in MoviesAndGenresList)
                {
                    if (g.IDmovie == movie.IDmovie)
                    {
                        genre += FindGenreByID(g.IDgenre).Name + ", ";
                    }
                }
                if (genre != String.Empty)
                    return genre.Remove(genre.Length-2);
                else
                    return "Brak danych";
            }
        #endregion

        #region Awards methods

        public void DeleteAward(int index)
        {
            Nagrody award = AwardList[index];
            RepozytoriumNagrody.DeleteAward(award);
        }

        public void GetAwards()
            {
                var awards = RepozytoriumNagrody.GetAllAwards();
                AwardList.Clear();
                foreach (var award in awards)
                {
                AwardList.Add(award);
                }
            }
            public void RefreshAwards(ObservableCollection<Nagrody> awards, int n)
            {
                awards.Clear(); // this line is intentional. It's purpouse is clearing ObservableCollection passed as reference, that way it is always filled with 4 objects
                AwardsVisibility.Clear();
                if (AwardList.Count != 0)
                {
                    
                    for (int i = 4 * n - 4; i <= n * 3 + 1; i++)
                    {
                        awards.Add(AwardList[i]);
                        AwardsVisibility.Add("Visible");
                        if (i == AwardList.Count()-1)
                        {
                            for (int z = i; z <= n * 3 + 2; z++)
                            {
                                awards.Add(emptyAward);
                                AwardsVisibility.Add("Hidden");
                            }
                            break;
                        }
                        
                    }
                }
                else
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        awards.Add(emptyAward);
                        AwardsVisibility.Add("Hidden");
                    }
                }
            }
        public void AddAward(string name, string description, byte[] image = null)
        {
            Nagrody award = new Nagrody(name, description, image);
            RepozytoriumNagrody.AddAward(award);
            GetAwards();
            //GetActorNames();
        }
        public void GetAwardsByName(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    GetAwards();
                }
                else
                {
                    GetAwards();
                    ObservableCollection<Nagrody> temp = new ObservableCollection<Nagrody>();
                    AwardsVisibility.Clear();
                    foreach (var award in AwardList)
                    {
                        if (award.Name.ToLower().Contains(name.ToLower()))
                        {
                            temp.Add(award);
                            AwardsVisibility.Add("Visible");
                        }
                        else
                        {
                            AwardsVisibility.Add("Hidden");
                        }
                    }
                    AwardList.Clear();
                AwardList = temp;
                }
            }
            public void GetMoviesAwards()
            {
                MoviesAwards.Clear();
                var MoviesRewards = RepozytoriumNagradzaja.GetMovieRewards();
                foreach (var mr in MoviesRewards)
                {
                    MoviesAwards.Add(mr);
                }
            }
            private Nagrody GetAwardById(int id)
            {
                foreach (var a in AwardList)
                {
                    if (a.IDAward == id)
                        return a;
                }
                return null;
            }
            public string GetMovieAwards(Filmy movie)
            {
                string awards = "";
                foreach (var a in MoviesAwards)
                {
                    if (a.IDmovie == movie.IDmovie)
                    {
                        awards += GetAwardById(a.IDaward).Name + ", ";
                    }
                }
                if (awards != String.Empty)
                    return awards.Remove(awards.Length-2);
                else
                    return "Brak danych";
            }
        #endregion

        #region PlayIn methods
            
        public void AddPlayIn(int movieId, int actorId)
        {
            Graja_w playIn = new Graja_w(movieId, actorId);
            RepozytoriumGraja_w.AddPlayIn(playIn);
            GetActorsMovies();
        }

        #endregion

        #region Direct methods

        public void AddDirect(int movieId, int directorId)
        {
            Rezyseruja direct = new Rezyseruja(movieId, directorId);
            RepozytoriumRezyseruja.AddDirect(direct);
            GetDirectorsMovies();
        }

        #endregion

        #region Reward methods

        public void AddReward(int movieId, int rewardId)
        {
            Nagradzaja reward = new Nagradzaja(movieId, rewardId);
            RepozytoriumNagradzaja.AddReward(reward);
            GetMoviesAwards();
        }

        #region Genre methods

        public void AddGenre(int movieId, int genreId)
        {
            Okresla genre = new Okresla(movieId, genreId);
            RepozytoriumOkresla.AddGenre(genre);
            MoviesAndGenres();
        }

        #endregion

        #endregion

        #endregion

        public string BirthDateToString(string Date)
        {
            string result = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                result += Date.ToString()[i];
            }
            return result;
        }
        public byte[] GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filename = openFileDialog.FileName;
                if(Path.GetExtension(filename) == ".jpg")
                {
                    
                    byte[] file =  File.ReadAllBytes(filename);
                    return file;
                }
            }
            return null;
        }
        /*public MySqlDbType.Blob GetImages()
        {

        }*/
    }
}
