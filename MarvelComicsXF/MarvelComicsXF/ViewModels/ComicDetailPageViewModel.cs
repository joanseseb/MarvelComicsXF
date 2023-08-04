using MarvelComicsXF.Models;
using MarvelComicsXF.Services.Api;
using MarvelComicsXF.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MarvelComicsXF.ViewModels
{
    public class ComicDetailPageViewModel : BaseViewModel
    {
        public ComicDetailPageViewModel(INavigationService navigationService, IMarvelApiService marvelApiService) : base(navigationService, marvelApiService)
        {
            _navigationService = navigationService;
            _marvelApiService = marvelApiService;

            ListOfCharacters = new ObservableRangeCollection<Character>();

            this.PageAppearingCommand = new Command(async () => await LoadDataAsync());
            //this.PageDisappearingCommand = new Command(async () => await LoadCharactersAsync());

        }
        #region [ Commands ]

        public ICommand PageAppearingCommand { get; set; }
        public ICommand PageDisappearingCommand { get; set; }

        #endregion

        #region [ Bindings ]
        private Comic selectedComic;
        public Comic SelectedComic
        {
            get => selectedComic;
            set
            {
                if (selectedComic != value)
                {
                    selectedComic = value;
                    OnPropertyChanged(nameof(SelectedComic));
                }
            }
        }

        private ObservableRangeCollection<Character> listOfCharacters;
        public ObservableRangeCollection<Character> ListOfCharacters
        {
            get => listOfCharacters;
            set
            {
                if (listOfCharacters != value)
                {
                    listOfCharacters = value;
                    OnPropertyChanged(nameof(ListOfCharacters));
                }
            }
        }

        #endregion

        public override void Initialize(IDictionary<string, object> parameters)
        {
            base.Initialize(parameters);
            if(parameters.TryGetValue("SelectedItem", out var comic))
            {
                SelectedComic = (Comic)comic;
            }
        }

        public async Task LoadDataAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                await LoadCharactersAsync();

                IsBusy = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LoadCharactersAsync()
        {
            try
            {
                var result = await _marvelApiService.GetCharactersAsync(SelectedComic.Characters.CollectionURI);
                if (result.Results != null)
                    ListOfCharacters.AddRange(result.Results);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
