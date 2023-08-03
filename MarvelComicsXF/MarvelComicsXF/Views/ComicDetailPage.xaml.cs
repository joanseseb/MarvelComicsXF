using MarvelComicsXF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarvelComicsXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComicDetailPage : ContentPage
    {
        private int currentIndex = 0;
        private Timer timer;

        public ComicDetailPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopTimer();
        }
        private void StartTimer()
        {
            var count = GetItemsSourceCount();
            if (count > 0)
            {
                timer = new Timer(async (_) =>
                {
                    await Device.InvokeOnMainThreadAsync(() =>
                    {
                        currentIndex = (currentIndex + 1) % count;
                        CharacterCarouselView.Position = currentIndex;
                        return true;
                    });
                }, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3));
            }
        }
        private void StopTimer()
        {
            timer?.Dispose();
        }
        private int GetItemsSourceCount()
        {
            if (CharacterCarouselView.ItemsSource is IEnumerable<object> itemsSource)
            {
                return itemsSource.Count();
            }
            return 0;
        }

        private void CharacterCarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            currentIndex = e.CurrentPosition;
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            // User has started swiping, stop the timer
            StopTimer();
            // User has completed swiping, restart the timer after 5 seconds
            var lastPanTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                var elapsed = DateTime.Now - lastPanTime;
                if (elapsed.TotalSeconds >= 5)
                {
                    // More than 5 seconds have passed since the last pan, restart the timer
                    StartTimer();
                    return false; // Stop the periodic timer
                }
                return true; // Continue the periodic timer
            });
        }

        private async void CharacterCarouselView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            await Task.Delay(100);
        }
    }
}