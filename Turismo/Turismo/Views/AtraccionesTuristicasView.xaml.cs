using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turismo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtraccionesTuristicasView : ContentPage
    {
        public AtraccionesTuristicasView()
        {
            InitializeComponent();
        }

        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
            //Pregunta si desea eliminar
           if(await DisplayAlert("Por favor confirme","¿Está seguro que desea eliminar la atracción?", "Si","No")== true)
            {
                var sw= (SwipeItem)sender;
                x.EliminarCommand.Execute(sw.CommandParameter);
            }
        }
    }
}