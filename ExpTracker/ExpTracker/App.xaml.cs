using Xamarin.Forms;
using System;
using Xamarin.Forms.Xaml;

namespace ExpTracker
{
    public partial class App : Application
    {

        public static string FolderPath { get; set; }
        public NavigationPage TabbedPage { get; }
        public App()
        {
            InitializeComponent();

             var nav = new NavigationPage(new MainPage());
            // nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            //nav.BarTextColor = Color.White;


            //  nav.BarTextColor = Color.Red;

             MainPage = nav;



            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
           
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
    }
}