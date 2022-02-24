using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZxingTestProject.PageModels;

namespace ZxingTestProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            MainPage = new FreshNavigationContainer(page);
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
