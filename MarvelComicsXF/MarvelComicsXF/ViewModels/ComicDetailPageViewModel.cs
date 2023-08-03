using MarvelComicsXF.Models;
using MarvelComicsXF.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MarvelComicsXF.ViewModels
{
    public class ComicDetailPageViewModel : BaseViewModel
    {
        public ComicDetailPageViewModel()
        {
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

                var apiService = new MarvelApiService();
                var result = await apiService.GetCharactersAsync(SelectedComic.Characters.CollectionURI);
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
