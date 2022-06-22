using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows;

namespace WPFilmweb.ViewModel
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WPFilmweb.Model;
    using WPFilmweb.ViewModel.BaseClasses;
    using WPFilmweb.View;
    using System.Security;

    internal class LoginViewModel : ViewModelBase
    {
        //private NavigationModel navigationModel { get; set; }
        //public NavigationModel NavigationModel
        //{
        //    get { return navigationModel; }
        //    set
        //    {
        //        navigationModel = value;
        //        onPropertyChanged(nameof(NavigationModel));
        //    }
        //}
        //private Model model { get; set; }
        //public Model Model
        //{
        //    get { return model; }
        //    set
        //    {
        //        model = value;
        //        onPropertyChanged(nameof(Model));
        //    }
        //}

        //private string currentUsername { get; set; }

        //public string CurrentUsername
        //{
        //    get { return currentUsername; }
        //    set
        //    {
        //        currentUsername = value;
        //        onPropertyChanged(nameof(CurrentUsername));
        //    }
        //}
        //private string currentPassword { get; set; }

        //public string CurrentPassword
        //{
        //    get { return currentPassword; }
        //    set
        //    {
        //        currentPassword = value;
        //        onPropertyChanged(nameof(CurrentPassword));
        //    }
        //}
        //public LoginViewModel(Model m, NavigationModel navimodel)
        //{
        //    NavigationModel = navimodel;
        //    Model = m;
        //}

        //private ICommand login;

        //public ICommand Login => login ?? (login = new RelayCommand(
        //    o =>
        //    {
        //        for(int i = 0; i < Model.UsersList.Count; i++)
        //        {
        //            if (Model.UsersList[i].Nickname == CurrentUsername && Model.UsersList[i].Password == CurrentPassword)
        //            {
        //                NavigationModel.ChangeVM(new MoviesViewModel(Model, NavigationModel));
        //            }
        //        }
        //    }, null));
    }
}
