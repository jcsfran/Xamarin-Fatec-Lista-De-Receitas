using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace JulioCesarApp.View.Receita
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<Model.Receita> list_receitas = new ObservableCollection<Model.Receita>();

        public Listagem()
        {
            InitializeComponent();

            lst_receitas.ItemsSource = list_receitas;
        }

        private void ToolbarItem_Clicked_Adicionar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Formulario());
        }

        protected override void OnAppearing()
        {
            if (list_receitas.Count == 0)
            {
                Task.Run(async () =>
                {
                    List<Model.Receita> temp = await App.Database.GetAll();

                    foreach (Model.Receita receita in temp)
                    {
                        list_receitas.Add(receita);
                    }
                });
            }

            ref_carregando.IsRefreshing = false;
        }

        private void ref_carregando_Refreshing(object sender, EventArgs e)
        {
            list_receitas.Clear();

            Task.Run(async () =>
            {
                List<Model.Receita> temp = await App.Database.GetAll();

                foreach (Model.Receita receita in temp)
                {
                    list_receitas.Add(receita);
                }
            });

            ref_carregando.IsRefreshing = false;
        }

        private async void MenuItem_Clicked_Remover(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;
            Model.Receita receita_selecionada = (Model.Receita)disparador.BindingContext;

            string mensagem = "Remover a receita: " + receita_selecionada.Nome + "?";
            bool confirmacao = await DisplayAlert("Tem certeza?", mensagem, "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.Delete(receita_selecionada.ReceitaId);

                list_receitas.Remove(receita_selecionada);
            }
        }

        private void lst_receitas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Model.Receita receita_selecionada = e.SelectedItem as Model.Receita;

            Navigation.PushAsync(new Formulario
            {
                BindingContext = receita_selecionada
            });
        }
    }
}