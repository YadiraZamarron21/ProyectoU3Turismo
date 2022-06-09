using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Turismo.Views;

namespace Turismo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AtraccionesTuristicasView());
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
