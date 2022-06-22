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

    class AwardsViewModel : ViewModelBase
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

        private ObservableCollection<Nagrody> awards{ get; set; }
        public ObservableCollection<Nagrody> Awards
        {
            get { return awards; }
            set
            {
                awards = value;
                onPropertyChanged(nameof(Awards));
            }
        }

        private ObservableCollection<string> awardsVisibility { get; set; }
        public ObservableCollection<string> AwardsVisibility
        {
            get { return awardsVisibility; }
            set
            {
                awardsVisibility = value;
                onPropertyChanged(nameof(AwardsVisibility));
            }
        }

        private int currentPage { get; set; }

        public AwardsViewModel(Model m, NavigationModel navimodel)
        {
            NavigationModel = navimodel;
            Model = m;
            Awards = new ObservableCollection<Nagrody>();
            CurrentPage = navimodel.CurrentPage;
            model.RefreshAwards(Awards, CurrentPage);
            AwardsVisibility = model.AwardsVisibility;
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
                if ((float)CurrentPage < (float)Model.AwardList.Count() / 4)
                {
                    CurrentPage++;
                    NavigationModel.CurrentPage = CurrentPage;
                    model.RefreshAwards(Awards, CurrentPage);
                    AwardsVisibility = model.AwardsVisibility;
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
                    model.RefreshAwards(Awards, CurrentPage);
                    AwardsVisibility = model.AwardsVisibility;
                }

            }, null));

        private ICommand awardClick1;

        public ICommand AwardClick1 => awardClick1 ?? (awardClick1 = new RelayCommand(
            o =>
            {

                if (Awards[0].Name != "")
                {
                    NavigationModel.ChangeVM(new AwardDescriptionViewModel(Awards[0], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand awardClick2;

        public ICommand AwardClick2 => awardClick2 ?? (awardClick2 = new RelayCommand(
            o =>
            {
                if (Awards[1].Name != "")
                {
                    NavigationModel.ChangeVM(new AwardDescriptionViewModel(Awards[1], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand awardClick3;

        public ICommand AwardClick3 => awardClick3 ?? (awardClick3 = new RelayCommand(
            o =>
            {
                if (Awards[2].Name != "")
                {
                    NavigationModel.ChangeVM(new AwardDescriptionViewModel(Awards[2], NavigationModel, Model));
                }
            }, null
            ));
        private ICommand awardClick4;

        public ICommand AwardClick4 => awardClick4 ?? (awardClick4 = new RelayCommand(
            o =>
            {
                if (Awards[3].Name != "")
                {
                    NavigationModel.ChangeVM(new AwardDescriptionViewModel(Awards[3], NavigationModel, Model));
                }
            }, null
            ));
       
    }
}
