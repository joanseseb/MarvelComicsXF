using MarvelComicsXF.Models;
using MarvelComicsXF.Services.Api;
using MarvelComicsXF.Services.Navigation;
using MarvelComicsXF.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MarvelComicsXF.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int currentOffSet = 0;
        private bool moreData;

        public MainPageViewModel(INavigationService navigationService, IMarvelApiService marvelApiService) : base(navigationService, marvelApiService)
        {
            _navigationService = navigationService;
            _marvelApiService = marvelApiService;


            this.ListOfComics = new ObservableRangeCollection<Comic>();
            this.NumberOfCallsToGetComicsAsync = 0;

            this.PageAppearingCommand = new Command(async () => await LoadComicsAsync());
            //this.PageDisappearingCommand = new Command(async () => await LoadCharactersAsync());
            this.RemainingItemsThresholdReachedCommand = new Command(async () => await LoadMoreComicsAsync(currentOffSet, SearchText));
            this.SearchCommand = new Command(async () => await SearchComicsByTitleAsync(SearchText));
            this.ItemTappedCommand = new Command<Comic>((selectedItem) => ItemTappedCommandExecuted(selectedItem));
            this.RefreshCommand = new Command(async async => await RefreshDataAsync());
            this.RestartSearchCommand = new Command(() => RestartSearchCommandExecuted());
        }


        #region [ Commands ]

        public ICommand PageAppearingCommand { get; set; }
        public ICommand PageDisappearingCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand RemainingItemsThresholdReachedCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }
        public ICommand RestartSearchCommand { get; set; }

        #endregion

        #region [ Bindings ]
        private ObservableRangeCollection<Comic> originalListOfComics;
        private ObservableRangeCollection<Comic> listOfComics;
        public ObservableRangeCollection<Comic> ListOfComics
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
        public async Task RefreshDataAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                ListOfComics = new ObservableRangeCollection<Comic>();
                this.SearchText = string.Empty;
                var result = await _marvelApiService.GetComicsAsync();
                ListOfComics.AddRange(result.Results);
                originalListOfComics = ListOfComics;

                NumberOfCallsToGetComicsAsync += 1;

                CheckIfMoreData(result);

                IsBusy = false;
                IsRefreshing = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LoadComicsAsync()
        {
            try
            {
                IsRefreshing = true;
                if (IsBusy || !string.IsNullOrEmpty(SearchText))
                    return;
                IsBusy = true;

                var result = await _marvelApiService.GetComicsAsync();
                ListOfComics.AddRange(result.Results);
                originalListOfComics = ListOfComics;

                NumberOfCallsToGetComicsAsync += 1;
                CheckIfMoreData(result);

                IsBusy = false;
                IsRefreshing = false;
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

                if (string.IsNullOrEmpty(searchText))
                {
                    if (moreData)
                    {
                        currentOffSet += 20;
                        var result = await _marvelApiService.GetMoreComicsAsync(offset);
                        ListOfComics.AddRange(result.Results);
                        originalListOfComics = ListOfComics;

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
                        var result = await _marvelApiService.GetMoreComicsByTitleAsync(offset, searchText);
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

                var result = await _marvelApiService.GetComicsByTitleAsync(searchText);
                ListOfComics = new ObservableRangeCollection<Comic>();
                ListOfComics.AddRange(result.Results);

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

        private async void RestartSearchCommandExecuted()
        {
            ListOfComics = originalListOfComics;
            await Task.Delay(500);
            SearchText = string.Empty;
        }

        #region [ Navigation ]

        public async void ItemTappedCommandExecuted(Comic selectedItem)
        {
            if (selectedItem is null)
            {
                throw new ArgumentNullException(nameof(selectedItem));
            }
            var parameters = new Dictionary<string, object> { { "SelectedItem", selectedItem } };
            await _navigationService.NavigateToPageAsync("ComicDetailPage", parameters);
        }
        #endregion
    }
}
