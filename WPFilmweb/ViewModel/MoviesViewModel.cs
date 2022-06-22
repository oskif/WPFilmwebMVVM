using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WPFilmweb.ViewModel
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WPFilmweb.Model;
    using WPFilmweb.ViewModel.BaseClasses;
    using WPFilmweb.View;

    class MoviesViewModel : ViewModelBase
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
            set 
            { 
                model = value;
            }
            
        }
        private ObservableCollection<Filmy> movies { get; set; }
        public ObservableCollection<Filmy> Movies
        {
            get { return movies; }
            set
            {
                movies = value;
                onPropertyChanged(nameof(Movies));
            }
        }

        private int currentPage { get; set; }

        private ObservableCollection<string> moviesVisibility { get; set; }
        public ObservableCollection<string> MoviesVisibility
        {
            get { return moviesVisibility; }
            set
            {
                moviesVisibility = value;
                onPropertyChanged(nameof(MoviesVisibility));
            }
        }

        private int currentUserId { get; set; }

        public int CurrentUderId
        {
            get { return currentUserId; }
            set
            {
                currentUserId = value;
                onPropertyChanged(nameof(CurrentUderId));
            }
        }

        public MoviesViewModel(Model m, NavigationModel navimodel, int id)
        {
            NavigationModel = navimodel;
            Model = m;
            Movies = new ObservableCollection<Filmy>();
            CurrentPage = navimodel.CurrentPage;
            model.RefreshMovies(movies, currentPage);
            //MoviesVisibility.Clear();
            MoviesVisibility = model.MoviesVisibility;
            CurrentUderId = id;
            Ratings = model.GetRatingsForMovieList(Movies);
        }
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                onPropertyChanged(nameof(CurrentPage));
            }
        }
        private ObservableCollection<string> ratings{ get; set; }
        public ObservableCollection<string> Ratings
        {
            get
            {
                return ratings;
            }
            set
            {
                ratings = value;
                onPropertyChanged(nameof(Ratings));
            }
        }
        private ICommand nextPage;

        public ICommand NextPage => nextPage ?? (nextPage = new RelayCommand(
            o =>
            {
                if ((float)CurrentPage < (float)Model.MoviesList.Count() / 4)
                {
                    CurrentPage++;
                    NavigationModel.CurrentPage = CurrentPage;
                    model.RefreshMovies(Movies, currentPage);
                    MoviesVisibility = model.MoviesVisibility;
                    Ratings = model.GetRatingsForMovieList(Movies);
                }

            }, null));

        private ICommand previousPage;

        public ICommand PreviousPage => previousPage ?? (previousPage = new RelayCommand(
            o =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    NavigationModel.CurrentPage = CurrentPage;
                    model.RefreshMovies(Movies, currentPage);
                    MoviesVisibility = model.MoviesVisibility;
                    Ratings = model.GetRatingsForMovieList(Movies);
                }

            }, null));

        private ICommand movieClick1;

        public ICommand MovieClick1 => movieClick1 ?? (movieClick1 = new RelayCommand(
            o =>
            {
                if (Movies[0].Title != "")
                {
                    NavigationModel.ChangeVM(new MovieDescriptionViewModel(Movies[0], CurrentUderId, NavigationModel, Model));
                }
            }, null
            ));
        private ICommand movieClick2;

        public ICommand MovieClick2 => movieClick2 ?? (movieClick2 = new RelayCommand(
            o =>
            {
                if(Movies[1].Title != "")
                {
                    NavigationModel.ChangeVM(new MovieDescriptionViewModel(Movies[1], CurrentUderId, NavigationModel, Model));
                }
            }, null
            ));
        private ICommand movieClick3;

        public ICommand MovieClick3 => movieClick3 ?? (movieClick3 = new RelayCommand(
            o =>
            {
                if (Movies[2].Title != "")
                {
                    NavigationModel.ChangeVM(new MovieDescriptionViewModel(Movies[2], CurrentUderId, NavigationModel, Model));
                }
            }, null
            ));
        private ICommand movieClick4;

        public ICommand MovieClick4 => movieClick4 ?? (movieClick4 = new RelayCommand(
            o =>
            {
                if (Movies[3].Title != "")
                {
                    NavigationModel.ChangeVM(new MovieDescriptionViewModel(Movies[3], CurrentUderId, NavigationModel, Model));
                }
            }, null
            ));
    }
}
