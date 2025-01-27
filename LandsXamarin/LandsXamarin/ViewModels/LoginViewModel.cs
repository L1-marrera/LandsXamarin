﻿namespace LandsXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using LandsXamarin.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string password;
        private string email;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;

            //Temporal
            this.Email = "ltest@gmail.com";
            this.Password = "LTest1";
        }
        #endregion

        #region Commands
        public ICommand ValidateMeCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Please enter a valid E-mail",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Please enter a Password",
                    "Accept");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;
            if (this.Email != "ltest@gmail.com" || this.Password != "LTest1")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or Password incorrect",
                    "Accept");
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            this.Email = string.Empty;
            this.Password = string.Empty;

            //await Application.Current.MainPage.DisplayAlert(
            //        "Ok",
            //        "Please stand by...",
            //        "Accept");

            MainViewModel.GetInstance().Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }
        #endregion
    }
}
