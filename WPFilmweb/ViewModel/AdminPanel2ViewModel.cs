using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace WPFilmweb.ViewModel
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WPFilmweb.Model;
    using WPFilmweb.ViewModel.BaseClasses;

    internal class AdminPanel2ViewModel : ViewModelBase
    {
        private NavigationModel navigationModel { get; set; }
        public NavigationModel NavigationModel
        {
            get { return navigationModel; }
            set
            {
                navigationModel = value;
                onPropertyChanged(nameof(NavigationModel));
            }
        }

        private Model model { get; set; }
        public Model Model
        {
            get { return model; }
            set { model = value; }
        }


        private ObservableCollection<string> movieTitles { get; set; }
        public ObservableCollection<string> MovieTitles
        {
            get { return movieTitles; }
            set
            {
                movieTitles = value;
                onPropertyChanged(nameof(MovieTitles));
            }
        }

        private ObservableCollection<string> actorNames { get; set; }
        public ObservableCollection<string> ActorNames
        {
            get { return actorNames; }
            set
            {
                actorNames = value;
                onPropertyChanged(nameof(ActorNames));
            }
        }

        private ObservableCollection<string> directorNames { get; set; }
        public ObservableCollection<string> DirectorNames
        {
            get { return directorNames; }
            set
            {
                directorNames = value;
                onPropertyChanged(nameof(DirectorNames));
            }
        }

        private int movieIndex { get; set; }
        public int MovieIndex
        {
            get { return movieIndex; }
            set
            {
                movieIndex = value;
                onPropertyChanged(nameof(MovieIndex));
            }
        }

        private int actorIndex { get; set; }
        public int ActorIndex
        {
            get { return actorIndex; }
            set
            {
                actorIndex = value;
                onPropertyChanged(nameof(ActorIndex));
            }
        }

        private int directorIndex { get; set; }
        public int DirectorIndex
        {
            get { return directorIndex; }
            set
            {
                directorIndex = value;
                onPropertyChanged(nameof(DirectorIndex));
            }
        }

        public AdminPanel2ViewModel(Model m, NavigationModel navimodel)
        {
            NavigationModel = navimodel;
            Model = m;
            MovieIndex = 0;
            MovieTitles = Model.MoviesTitles;
            DirectorNames = Model.DirectorNames;
            ActorNames = Model.ActorNames;
        }

        private ICommand deleteMovie;

        public ICommand DeleteMovie => deleteMovie ?? (deleteMovie = new RelayCommand(
            o=>
            {
                Model.DeleteMovie(MovieIndex);
                Model.GetMovies();
                Model.GetMovieTitles();
                MovieTitles = Model.MoviesTitles;
            },null
            ));

        private ICommand deleteActor;

        public ICommand DeleteActor => deleteActor ?? (deleteActor = new RelayCommand(
            o =>
            {
                Model.DeleteActor(ActorIndex);
                Model.GetActors();
                Model.GetActorNames();
                ActorNames = Model.ActorNames;
            }, null
            ));

        private ICommand deleteDirector;

        public ICommand DeleteDirector => deleteDirector ?? (deleteDirector = new RelayCommand(
            o =>
            {
                Model.DeleteDirector(DirectorIndex);
                Model.GetDirectors();
                Model.GetDirectorNames();
                DirectorNames = Model.DirectorNames;
            }, null
            ));
    }
}
