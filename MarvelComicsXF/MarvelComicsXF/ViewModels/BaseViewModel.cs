using MarvelComicsXF.Services.Api;
using MarvelComicsXF.Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MarvelComicsXF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected INavigationService _navigationService;
        protected IMarvelApiService _marvelApiService;
        protected BaseViewModel(INavigationService navigationService, IMarvelApiService marvelApiService)
        {
            _navigationService = navigationService;
            _marvelApiService = marvelApiService;
        }

        public virtual void Initialize(IDictionary<string, object> parameters)
        {
        }


        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                }
            }
        }
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
    }
}
