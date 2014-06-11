﻿using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using VSOTeams.Helpers;
using VSOTeams.Views;

namespace VSOTeams.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
            //Icon = "blog.png";
        }

        private LoginInfo credentials = new LoginInfo();
        public LoginInfo Credentials
        {
            get { return credentials;  }
            set { credentials = value; OnPropertyChanged("Credentials"); }
        }

        private Command logMeIn;
        public Command LogMeIn
        {
            get { return logMeIn ?? (logMeIn = new Command(async () => await ExecuteLogMeInCommand())); }
        }

        private async Task ExecuteLogMeInCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var credentialsOK = await credentials.CanLogin();
                if (credentialsOK == true)
                {
                    App.IsLoggedIn = true;
                    // de hele boel laden

                    IsBusy = false;
                    GetMainPage();
                }
                else
                {
                    IsBusy = false;
                }
               
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var error = page.DisplayAlert("Error", string.Format("Unable to connect to VSO. {0}", ex.InnerException), "OK", null).Result;
                IsBusy = false;
            }
        }

        public static Page GetMainPage()
        {
            var home = new HomeView();
            return new NavigationPage(home);
        }

    }
}