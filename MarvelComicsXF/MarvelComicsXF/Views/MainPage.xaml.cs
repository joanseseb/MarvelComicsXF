using Xamarin.Forms;


namespace MarvelComicsXF.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
    }
}
