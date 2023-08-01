using MarvelComicsXF.Models;
using MarvelComicsXF.Services;
using MarvelComicsXF.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarvelComicsXF.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int currentOffSet = 0;
        private bool moreData;

        public MainPageViewModel()
        {
            this.ListOfComics = new ObservableCollection<Comic>();
            this.NumberOfCallsToGetComicsAsync = 0;
            this.PageAppearingCommand = new Command(async () => await LoadComicsAsync());
            //this.PageDisappearingCommand = new Command(async () => await LoadCharactersAsync());
            this.RemainingItemsThresholdReachedCommand = new Command(async () => await LoadMoreComicsAsync(currentOffSet, SearchText));
            this.SearchCommand = new Command(async () => await SearchComicsByTitleAsync(SearchText));
            this.ItemTappedCommand = new Command(() => ItemTappedCommandExecuted());

        }


        #region [ Commands ]

        public ICommand PageAppearingCommand { get; set; }
        //public ICommand PageDisappearingCommand { get; set; }
        public ICommand RemainingItemsThresholdReachedCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }


        #endregion

        #region [ Bindings ]

        private ObservableCollection<Comic> listOfComics;
        public ObservableCollection<Comic> ListOfComics
        {
            get => listOfComics;
            set
            {
                if (listOfComics != value)
                {
                    listOfComics = value;
                    OnPropertyChanged(nameof(ListOfComics));
                }
            }
        }
        private int numberOfCallsToGetComicsAsync;
        public int NumberOfCallsToGetComicsAsync
        {
            get => numberOfCallsToGetComicsAsync;
            set
            {
                if (numberOfCallsToGetComicsAsync != value)
                {
                    numberOfCallsToGetComicsAsync = value;
                    OnPropertyChanged(nameof(NumberOfCallsToGetComicsAsync));
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }


        #endregion

        public async Task LoadComicsAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                var apiService = new MarvelApiService();
                var result = await apiService.GetComicsAsync();
                foreach (var item in result.Results)
                {
                    ListOfComics.Add(item);
                }
                NumberOfCallsToGetComicsAsync += 1;

                CheckIfMoreData(result);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task LoadMoreComicsAsync(int offset, string searchText)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                var apiService = new MarvelApiService();
                if (string.IsNullOrEmpty(searchText))
                {
                    if (moreData)
                    {
                        currentOffSet += 20;
                        var result = await apiService.GetMoreComicsAsync(offset);
                        foreach (var item in result.Results)
                        {
                            ListOfComics.Add(item);
                        }
                        CheckIfMoreData(result);
                        NumberOfCallsToGetComicsAsync += 1;

                    }
                    else
                    {
                        //TODO
                    }
                }
                else
                {
                    if (moreData)
                    {
                        currentOffSet += 20;
                        var result = await apiService.GetMoreComicsByTitleAsync(offset, searchText);
                        foreach (var item in result.Results)
                        {
                            ListOfComics.Add(item);
                        }
                        CheckIfMoreData(result);
                        NumberOfCallsToGetComicsAsync += 1;
                    }
                    else
                    {
                        //TODO
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SearchComicsByTitleAsync(string searchText)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                currentOffSet = 20;
                var apiService = new MarvelApiService();
                var result = await apiService.GetComicsByTitleAsync(searchText);
                ListOfComics = new ObservableCollection<Comic>();
                foreach (var item in result.Results)
                {
                    ListOfComics.Add(item);
                }

                NumberOfCallsToGetComicsAsync += 1;
                CheckIfMoreData(result);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CheckIfMoreData(ComicDataContainer result)
        {
            if (result.Total - (result.Count + result.Offset) > 0)
            {
                moreData = true;
            }
            else
            {
                moreData = false;
            }
        }

        #region [ Navigation ]

        public async void ItemTappedCommandExecuted()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ComicDetailPage());
        }
        #endregion
    }
}
