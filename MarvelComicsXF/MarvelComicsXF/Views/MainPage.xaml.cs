using MarvelComicsXF.Services.Navigation;
using MarvelComicsXF.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MarvelComicsXF.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<MainPageViewModel>();
        }

        private void SearchIcon_Tapped(object sender, System.EventArgs e)
        {
            var searchIcon = (Label)this.FindByName("SearchIcon");
            var searchBar = (SearchBar)this.FindByName("SearchBar");
            var backIcon = (Label)this.FindByName("BackIcon");
            searchIcon.IsVisible = !searchIcon.IsVisible;
            searchBar.IsVisible = !searchBar.IsVisible;
            backIcon.IsVisible = !backIcon.IsVisible;
            if (searchBar.IsVisible)
            {
                searchBar.Focus();
            }

        }
        private void SearchAgainTapped(object sender, System.EventArgs e)
        {
            var searchBar = (SearchBar)this.FindByName("SearchBar");
            if (searchBar.IsVisible)
            {
                //searchBar.Text = string.Empty;
                searchBar.Focus();

            }
        }
    }
}
