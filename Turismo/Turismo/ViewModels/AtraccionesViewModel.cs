using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using Turismo.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Turismo.Views;
using System.IO;
using Newtonsoft.Json;

namespace Turismo.ViewModels
{
    public class AtraccionesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<AtraccionesTuristicas> Grupo { get; set; } = new ObservableCollection<AtraccionesTuristicas>();

        public AtraccionesTuristicas Atracciones { get; set; }

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand MostrarDetallesCommand { get; set; }

        public string Error { get; set; }

        public AtraccionesViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command(Agregar);
            EliminarCommand = new Command<AtraccionesTuristicas>(Eliminar);
            MostrarDetallesCommand = new Command<AtraccionesTuristicas>(MostrarDetalles);
        }

        private void MostrarDetalles(AtraccionesTuristicas obj)
        {
            throw new NotImplementedException();
        }

        private void Eliminar(AtraccionesTuristicas a)
        {
            if (a!=null)
            {
                Grupo.Remove(a);
                Serializar();
            } 
        }

        private void CambiarVista(string vista)
        {
            if (vista == "Agregar")
            {
                Atracciones = new AtraccionesTuristicas();
                vistaAtracccion = new AgregarAtraccionView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(vistaAtracccion);
            }
            else if(vista== "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
        void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "grupo.json";
            File.WriteAllText( file,JsonConvert.SerializeObject(Grupo));
        }
        void Deserializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "grupo.json";
            if (File.Exists(file))
            {
                Grupo = JsonConvert.DeserializeObject<ObservableCollection<AtraccionesTuristicas>>(File.ReadAllText(file));
            }
        }
           
        
        AgregarAtraccionView vistaAtracccion;

        private void Agregar()
        {
            Error = "";
            if (Atracciones != null)
            {
                if (string.IsNullOrEmpty(Atracciones.NombreAtraccion))
                {
                    Error = "Escriba el nombre de la atracción";
                }
                if (string.IsNullOrWhiteSpace(Atracciones.URLImagen))
                {
                    Error = "Escriba el URL de la imagen de la atracción turística";
                }
                if (string.IsNullOrWhiteSpace(Atracciones.Ubicacion))
                {
                    Error = "Escriba la ubicación de la atracción";
                }
                if (string.IsNullOrWhiteSpace(Atracciones.Descripcion))
                {
                    Error = "Escriba la descripción de la atracción";
                }
                if (string.IsNullOrWhiteSpace(Error))
                {
                    Grupo.Add(Atracciones);
                    CambiarVista("Ver");
                    Serializar();
                } 
                Change();
            }
        }

        private void Change()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }


    }
}
