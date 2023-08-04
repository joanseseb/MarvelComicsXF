using System;
using Xamarin.UITest;
using NUnit.Framework;
using UITest;

[TestFixture(Platform.Android)]
//[TestFixture(Platform.iOS)]
public class MainPageTests
{
    IApp app;
    readonly Platform platform;

    public MainPageTests(Platform platform)
    {
        this.platform = platform;
    }

    [SetUp]
    public void BeforeEachTest()
    {
        app = AppInitializer.StartApp(platform);
    }

    [Test]
    public void VerifyPageContent()
    {
        // Verify the presence of elements on the page
        app.Query(c => c.Marked("BackIcon"));
        app.Query(c => c.Marked("SearchBar"));
        app.Query(c => c.Marked("SearchIcon"));
        app.Query(c => c.Marked("CollectionView"));
    }

    [Test]
    
    public void TapSearchIcon()
    {
        //Tap search icon and verify backicon and searchbar appears
        app.Tap(c => c.Marked("SearchIcon"));
        _ = app.WaitForElement(c => c.Marked("BackIcon"));
        _ = app.WaitForElement(c => c.Marked("SearchBar"));
    }
    [Test]
    public void TapKeyboardSearchIcon()
    {
        // Enter search text into the search bar and verify keyboard dimisses
        app.Tap(c => c.Marked("SearchIcon"));
        app.EnterText(c => c.Marked("SearchBar"), "Spider");
        app.DismissKeyboard(); // Dismiss the keyboard if it is open
    }
}
