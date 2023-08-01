using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MarvelComicsXF.Controls;
using MarvelComicsXF.iOS.Renderers;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]

namespace MarvelComicsXF.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.ShowsSearchResultsButton = false;
            }
        }
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SearchBar.IsFocusedProperty.PropertyName && Control != null)
            {
                if (Element.IsFocused)
                {
                    Control.BecomeFirstResponder();
                }
                else
                {
                    Control.ResignFirstResponder();
                }
            }
        }
    }
}
