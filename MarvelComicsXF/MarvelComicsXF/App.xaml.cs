using MarvelComicsXF.Services.Api;
using MarvelComicsXF.Services.Navigation;
using MarvelComicsXF.ViewModels;
using MarvelComicsXF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace MarvelComicsXF
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();

            SetupServices();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        void SetupServices()
        {
            var services = new ServiceCollection();


            //Core
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IMarvelApiService, MarvelApiService>();

            //ViewModels
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<ComicDetailPageViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }
        public static BaseViewModel GetViewModel<TViewModel>()
    where TViewModel : BaseViewModel
    => ServiceProvider.GetService<TViewModel>();
    }
}
