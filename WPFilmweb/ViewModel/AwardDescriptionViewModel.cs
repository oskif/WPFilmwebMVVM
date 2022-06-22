using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFilmweb.ViewModel.BaseClasses;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace WPFilmweb.ViewModel
{
    using Model;
    using DAL.Encje;
    internal class AwardDescriptionViewModel : ViewModelBase
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
        private Nagrody currentAward;

        public Nagrody CurrentAward
        {
            get
            {
                return currentAward;
            }
            set
            {
                currentAward = value;
                onPropertyChanged(nameof(CurrentAward));
            }
        }


        public AwardDescriptionViewModel(Nagrody award, NavigationModel navi, Model model)
        {
            Model = model;
            CurrentAward = award;
            NavigationModel = navi;
        }

        public string Name => CurrentAward.Name;
        public string Description => "Opis: " + CurrentAward.Description;
        public ImageSource AwardImage => Tools.ByteArrToImageSrc(CurrentAward.AwardImage);

        private ICommand back;

        public ICommand Back => back ?? (back = new RelayCommand(
            o =>
            {
                NavigationModel.ChangeVM(new AwardsViewModel(Model, NavigationModel));
            }, null
            ));
    }
}
