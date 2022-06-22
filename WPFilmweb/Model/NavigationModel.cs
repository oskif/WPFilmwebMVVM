using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFilmweb.ViewModel.BaseClasses;
namespace WPFilmweb.Model
{
    public sealed class NavigationModel : ViewModelBase
    {
        private static NavigationModel instance = null;
        private ViewModelBase currentViewModel {get; set;}
        public ViewModelBase CurrentViewModel 
        { 
            get { return currentViewModel; }
            set 
            { 
                currentViewModel = value; 
                onPropertyChanged(nameof(CurrentViewModel));
            } 
        }
        private int currentPage { get; set;}
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                onPropertyChanged(nameof(CurrentPage));
            }
        }
        public void ChangeVM(ViewModelBase vm)
        {
            CurrentViewModel = vm;
        }
        private NavigationModel()
        {
            CurrentPage = 1;
        }   
        public static NavigationModel getInstance()
        {
            if(instance == null)
                instance = new NavigationModel();
            return instance;
        }
    }
}
