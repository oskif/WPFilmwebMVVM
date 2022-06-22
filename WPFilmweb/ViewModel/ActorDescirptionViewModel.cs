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
    internal class ActorDescriptionViewModel : ViewModelBase
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
        private Aktorzy currentActor;

        public Aktorzy CurrentActor
        {
            get
            {
                return currentActor;
            }
            set
            {
                currentActor = value;
                onPropertyChanged(nameof(CurrentActor));
            }
        }


        public ActorDescriptionViewModel(Aktorzy actor, NavigationModel navi, Model model)
        {
            Model = model;
            CurrentActor = actor;
            NavigationModel = navi;
        }

        public string NameSurname => CurrentActor.Name + " " + CurrentActor.Surname;
        public string Bio => "Biografia: " + CurrentActor.Bio;
        public ImageSource ActorImage => Tools.ByteArrToImageSrc(CurrentActor.ActorImage);
        public string BirthDate => "Data Urodzenia: " + Model.BirthDateToString(CurrentActor.BirthDate);
        public string Movies => "Zagrał w: " + Model.GetActorsMovies(CurrentActor);

        private ICommand back;

        public ICommand Back => back ?? (back = new RelayCommand(
            o =>
            {
                NavigationModel.ChangeVM(new ActorsViewModel(Model, NavigationModel));
            }, null
            ));
    }
}
