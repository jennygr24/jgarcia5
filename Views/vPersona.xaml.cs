using jgarcia5.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jgarcia5.Views
{
    public partial class vPersona : ContentPage
    {
        public vPersona()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RefrescarListaPersonas();
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            LblStatus.Text = "";
            App.personRepo.AddNewPerson(txtName.Text);
            LblStatus.Text = App.personRepo.StatusMessage;
            RefrescarListaPersonas();
        }

        private void btnObtener_Clicked(object sender, EventArgs e)
        {
            LblStatus.Text = "";
            RefrescarListaPersonas();
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            LblStatus.Text = "";
            var selectedPerson = pickerPersonas.SelectedItem as Persona;
            if (selectedPerson != null)
            {
                selectedPerson.Name = txtNuevoNombre.Text;
                App.personRepo.ActualizarPersona(selectedPerson);
                LblStatus.Text = App.personRepo.StatusMessage;
                RefrescarListaPersonas();
            }
            else
            {
                LblStatus.Text = "Seleccione una persona para actualizar.";
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            LblStatus.Text = "";
            var selectedPerson = pickerPersonas.SelectedItem as Persona;
            if (selectedPerson != null)
            {
                App.personRepo.EliminarPersona(selectedPerson);
                LblStatus.Text = App.personRepo.StatusMessage;
                RefrescarListaPersonas();
            }
            else
            {
                LblStatus.Text = "Seleccione una persona para eliminar.";
            }
        }

        private void RefrescarListaPersonas()
        {
            var personas = App.personRepo.getAllPeople();
            pickerPersonas.ItemsSource = personas; 
            pickerPersonas.ItemDisplayBinding = new Binding("Name");
            listaPersona.ItemsSource = personas;
            LblStatus.Text = App.personRepo.StatusMessage;
        }

        private void pickerPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (pickerPersonas.SelectedIndex !=-1)
            {
                Persona seleccionarPersona= (Persona)pickerPersonas.SelectedItem;
                txtId.Text = seleccionarPersona.Id.ToString(); 
                txtNuevoNombre.Text = seleccionarPersona.Name; 
            }
        }
    }
}
