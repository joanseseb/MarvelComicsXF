using Android.Content;
using Android.Graphics.Drawables;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.Content;
using MarvelComicsXF.Controls;
using MarvelComicsXF.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]

namespace MarvelComicsXF.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            var icon = Control?.FindViewById(Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null));
            (icon as ImageView)?.SetImageDrawable(null);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if ((e.PropertyName == SearchBar.IsFocusedProperty.PropertyName || e.PropertyName == SearchBar.TextProperty.PropertyName ) && Control != null)
            {
                if (Element.IsFocused || string.IsNullOrEmpty(Element.Text))
                {
                    //Control.RequestFocus();
                    InputMethodManager imm = (InputMethodManager)Control.Context.GetSystemService(Context.InputMethodService);
                    imm.ShowSoftInput(Control, ShowFlags.Forced);
                    imm.ShowSoftInput(Control, ShowFlags.Implicit);
                }
                else
                {
                    InputMethodManager imm = (InputMethodManager)Control.Context.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(Control.WindowToken, 0);
                }
            }
        }
    }
}
