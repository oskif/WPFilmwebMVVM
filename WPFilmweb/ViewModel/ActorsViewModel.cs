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

    class ActorsViewModel : ViewModelBase
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

        private ObservableCollection<Aktorzy> actors { get; set; }
        public ObservableCollection<Aktorzy> Actors
        {
            get { return actors; }
            set
            {
                actors = value;
                onPropertyChanged(nameof(Actors));
            }
        }


        private ObservableCollection<string> actorsVisibility { get; set; }
        public ObservableCollection<string> ActorsVisibility
        {
            get { return actorsVisibility; }
            set
            {
                actorsVisibility = value;
                onPropertyChanged(nameof(ActorsVisibility));
            }
          }

        private int currentPage { get; set; }

        public ActorsViewModel(Model m, NavigationModel navimodel)
        {
            NavigationModel = navimodel;
            Model = m;
            Actors = new ObservableCollection<Aktorzy>();
            CurrentPage = navimodel.CurrentPage;
            model.RefreshActors(Actors, CurrentPage);
            ActorsVisibility = model.ActorsVisibility;
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

        private ICommand nextPage;

        public ICommand NextPage => nextPage ?? (nextPage = new RelayCommand(
            o =>
            {
                if ((float)CurrentPage < (float)Model.ActorList.Count() / 4)
                {
                    CurrentPage++;
                    NavigationModel.CurrentPage = CurrentPage;
                    model.RefreshActors(Actors, CurrentPage);
                    ActorsVisibility=model.ActorsVisibility;
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
                    model.RefreshActors(Actors, CurrentPage);
                    ActorsVisibility = model.ActorsVisibility;
                }

            }, null));

        private ICommand actorClick1;

        public ICommand ActorClick1 => actorClick1 ?? (actorClick1 = new RelayCommand(
            o =>
            {

                if (Actors[0].Name != "")
                {
                    NavigationModel.ChangeVM(new ActorDescriptionViewModel(Actors[0], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand actorClick2;

        public ICommand ActorClick2 => actorClick2 ?? (actorClick2 = new RelayCommand(
            o =>
            {
                if (Actors[1].Name != "")
                {
                    NavigationModel.ChangeVM(new ActorDescriptionViewModel(Actors[1], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand actorClick3;

        public ICommand ActorClick3 => actorClick3 ?? (actorClick3 = new RelayCommand(
            o =>
            {
                if (Actors[2].Name != "")
                {
                    NavigationModel.ChangeVM(new ActorDescriptionViewModel(Actors[2], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand actorClick4;

        public ICommand ActorClick4 => actorClick4 ?? (actorClick4 = new RelayCommand(
            o =>
            {
                if (Actors[3].Name != "")
                {
                    NavigationModel.ChangeVM(new ActorDescriptionViewModel(Actors[3], NavigationModel, Model));
                }
            }, null
            ));
    }
}
