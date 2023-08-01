using MarvelComicsXF.Models;
using MarvelComicsXF.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace MarvelComicsXF.Converters
{
    public class IndexValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is Binding binding &&
                value is Comic score &&
                binding.Source is CollectionView collectionView &&
                collectionView.BindingContext is MainPageViewModel viewModel)
            {
                return viewModel.ListOfComics.IndexOf(score);
            }

            return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
