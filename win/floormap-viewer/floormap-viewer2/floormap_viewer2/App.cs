using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace floormap_viewer2
{
    public class App : Application
    {
        public App()
        {
            var browser = new WebView();
            browser.Source = "http://xxx.azurewebsites.net";

            // The root page of your application
            var cp = new ContentPage();
            MainPage = cp;
            cp.Content = browser;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
