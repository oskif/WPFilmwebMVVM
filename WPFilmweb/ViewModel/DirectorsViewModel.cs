using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Input;
namespace WPFilmweb.ViewModel
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WPFilmweb.Model;
    using WPFilmweb.ViewModel.BaseClasses;
    using WPFilmweb.View;

    class DirectorsViewModel : ViewModelBase
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

        private ObservableCollection<Rezyserzy> directors { get; set; }
        public ObservableCollection<Rezyserzy> Directors
        {
            get { return directors; }
            set
            {
                directors = value;
                onPropertyChanged(nameof(Directors));
            }
        }
        private int currentPage { get; set; }

        private ObservableCollection<string> directorsVisibility { get; set; }
        public ObservableCollection<string> DirectorsVisibility
        {
            get { return directorsVisibility; }
            set
            {
                directorsVisibility = value;
                onPropertyChanged(nameof(DirectorsVisibility));
            }
        }

        public DirectorsViewModel(Model m, NavigationModel navimodel)
        {
            NavigationModel = navimodel;
            Model = m;
            Directors = new ObservableCollection<Rezyserzy>();
            CurrentPage = navimodel.CurrentPage;
            model.RefreshDirectors(Directors, CurrentPage);
            DirectorsVisibility = m.DirectorsVisibility;
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
                if ((float)CurrentPage < (float)Model.DirectorsList.Count() / 4)
                {
                    CurrentPage++;
                    NavigationModel.CurrentPage = CurrentPage;
                    model.RefreshDirectors(Directors, CurrentPage);
                    DirectorsVisibility = model.DirectorsVisibility;
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
                    model.RefreshDirectors(Directors, CurrentPage);
                    DirectorsVisibility = model.DirectorsVisibility;
                }

            }, null));

        private ICommand directorClick1;

        public ICommand DirectorClick1 => directorClick1 ?? (directorClick1 = new RelayCommand(
            o =>
            {

                if (Directors[0].Name != "")
                {
                    NavigationModel.ChangeVM(new DirectorDescriptionViewModel(Directors[0], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand directorClick2;

        public ICommand DirectorClick2 => directorClick2 ?? (directorClick2 = new RelayCommand(
            o =>
            {
                if (Directors[1].Name != "")
                {
                    NavigationModel.ChangeVM(new DirectorDescriptionViewModel(Directors[1], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand directorClick3;

        public ICommand DirectorClick3 => directorClick3 ?? (directorClick3 = new RelayCommand(
            o =>
            {
                if (Directors[2].Name != "")
                {
                    NavigationModel.ChangeVM(new DirectorDescriptionViewModel(Directors[2], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand directorClick4;

        public ICommand DirectorClick4 => directorClick4 ?? (directorClick4 = new RelayCommand(
            o =>
            {
                if (Directors[3].Name != "")
                {
                    NavigationModel.ChangeVM(new DirectorDescriptionViewModel(Directors[3], NavigationModel, Model));
                }
            }, null
            ));
    }
}
