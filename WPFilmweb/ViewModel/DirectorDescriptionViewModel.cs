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
    internal class DirectorDescriptionViewModel : ViewModelBase
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
        private Rezyserzy currentDirector;

        public Rezyserzy CurrentDirector
        {
            get
            {
                return currentDirector;
            }
            set
            {
                currentDirector = value;
                onPropertyChanged(nameof(CurrentDirector));
            }
        }


        public DirectorDescriptionViewModel(Rezyserzy director, NavigationModel navi, Model model)
        {
            Model = model; 
            CurrentDirector = director;
            NavigationModel = navi;
        }

        public string NameSurname => CurrentDirector.Name + " " + CurrentDirector.Surname;
        public string Bio => "Biografia: " + CurrentDirector.Bio;
        public ImageSource ActorImage => Tools.ByteArrToImageSrc(CurrentDirector.DirectorImage);
        public string BirthDate => "Data Urodzenia: " + Model.BirthDateToString(CurrentDirector.Birthdate);
        public string Movies => "Wyreżyserował: " + Model.GetDirectorsMovies(CurrentDirector);

        
        private ICommand back;

        public ICommand Back => back ?? (back = new RelayCommand(
            o =>
            {
                NavigationModel.ChangeVM(new DirectorsViewModel(Model, NavigationModel));
            }, null
            ));
    }
}
    

