using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JulioCesarApp.View.Ingrediente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<Model.Ingrediente> list_ingredientes = new ObservableCollection<Model.Ingrediente>();

        public Listagem()
        {
            InitializeComponent();

            lst_ingredientes.ItemsSource = list_ingredientes;

            NavigationPage.SetHasBackButton(new Receita.Listagem(), true);
        }

        private void ToolbarItem_Clicked_Adicionar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Formulario
            {
                BindingContext = new Model.Ingrediente
                {
                    ReceitaId = Convert.ToInt32(lbl_receita_id.Text)
                }
            });
        }

        protected override void OnAppearing()
        {
            if (list_ingredientes.Count == 0)
            {
                Task.Run(async () =>
                {
                    List<Model.Ingrediente> temp = await App.Database.GetAllIngredientes(Convert.ToInt16(lbl_receita_id.Text));

                    foreach (Model.Ingrediente ingrediente in temp)
                    {
                        list_ingredientes.Add(ingrediente);
                    }
                });
            }

            ref_carregando.IsRefreshing = false;
        }

        private void ref_carregando_Refreshing(object sender, EventArgs e)
        {
            list_ingredientes.Clear();

            Task.Run(async () =>
            {
                List<Model.Ingrediente> temp = await App.Database.GetAllIngredientes(Convert.ToInt16(lbl_receita_id.Text));

                foreach (Model.Ingrediente ingrediente in temp)
                {
                    list_ingredientes.Add(ingrediente);
                }
            });

            ref_carregando.IsRefreshing = false;
        }

        private async void MenuItem_Clicked_Remover(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;
            Model.Ingrediente ingrediente_selecionado = (Model.Ingrediente)disparador.BindingContext;

            string mensagem = "Remover a receita: " + ingrediente_selecionado.Nome + "?";
            bool confirmacao = await DisplayAlert("Tem certeza?", mensagem, "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.DeleteIngrediente(ingrediente_selecionado.IngredienteId);

                list_ingredientes.Remove(ingrediente_selecionado);
            }
        }

        private void lst_ingredientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Model.Ingrediente ingrediente_selecionado = e.SelectedItem as Model.Ingrediente;

            Navigation.PushAsync(new Formulario
            {
                BindingContext = ingrediente_selecionado
            });
        }
    }
}