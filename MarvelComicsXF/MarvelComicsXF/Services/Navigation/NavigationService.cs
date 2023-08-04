using MarvelComicsXF.ViewModels;
using MarvelComicsXF.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MarvelComicsXF.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToPageAsync(string pageName)
        {
            try
            {
                Page page = GetPageInstance(pageName);

                if (page != null)
                {
                    await GetNavigationPage().PushAsync(page);
                }
                else
                {
                    // Handle navigation to a non-existing page
                    // You can throw an exception or log an error here.
                }
            }
            catch (Exception ex)
            {
                // Handle any navigation exceptions
                // For example, you can log the exception here.
                Console.WriteLine($"Navigation Error: {ex.Message}");
            }
        }
        public async Task NavigateToPageAsync(string pageName, IDictionary<string, object> parameters = null)
        {
            Page page = GetPageInstance(pageName);
            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.Initialize(parameters);
            }

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
        public async Task GoBackAsync()
        {
            try
            {
                await GetNavigationPage().PopAsync();
            }
            catch (Exception ex)
            {
                // Handle any navigation exceptions
                // For example, you can log the exception here.
                Console.WriteLine($"Navigation Error: {ex.Message}");
            }
        }
        private Page GetPageInstance(string pageName)
        {
            // Implement a logic to get the Page instance based on the pageName
            // For example, you can use a switch statement or a dictionary to map pageName to Page types.

            // Example:
            switch (pageName)
            {
                case "ComicDetailPage":
                    return new ComicDetailPage(); // Replace "NextPage" with the actual Page class.
                                           // Add other cases for more pages.
                default:
                    return null;
            }
        }

        private INavigation GetNavigationPage()
        {
            // Get the current NavigationPage instance from the application's MainPage
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                return navigationPage.Navigation;
            }

            // If the MainPage is not a NavigationPage, you might need to adjust this logic based on your app's structure.
            // For example, if you are using a MasterDetailPage or TabbedPage as the MainPage.

            throw new InvalidOperationException("MainPage is not a NavigationPage");
        }

    }
}
