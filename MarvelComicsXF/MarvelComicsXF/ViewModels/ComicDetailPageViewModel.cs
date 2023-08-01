using MarvelComicsXF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarvelComicsXF.ViewModels
{
    public class ComicDetailPageViewModel : BaseViewModel
    {
        public ComicDetailPageViewModel()
        {
            //this.PageAppearingCommand = new Command(async () => await LoadComicsAsync());
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

        #endregion
    }
}
