using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JulioCesarApp.View.Ingrediente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        public Formulario()
        {
            InitializeComponent();

            var medidas = new List<string> { "Xícara", "Chá", "Litros", "ML", "KG", "Gramas" };
   
            foreach (string medida in medidas)
            {
                pck_medida.Items.Add(medida);
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                string mensagem = "Ingrediente adicionado";
                Model.Ingrediente ingrediente_selecionado = new Model.Ingrediente();

                if (pck_medida.SelectedIndex == -1)
                {
                    await DisplayAlert("Aviso!", "Selecione a medida", "OK");

                    return;
                }

                if (BindingContext != null)
                {
                    ingrediente_selecionado = BindingContext as Model.Ingrediente;
                }

                Model.Ingrediente ingrediente = new Model.Ingrediente()
                {
                    IngredienteId = ingrediente_selecionado.IngredienteId,
                    ReceitaId = ingrediente_selecionado.ReceitaId,
                    Nome = txt_nome.Text,
                    Quantidade = txt_quantidade.Text,
                    Medida = pck_medida.Items[pck_medida.SelectedIndex],
                };

                if (ingrediente.IngredienteId == 0)
                {
                    await App.Database.InsertIngrediente(ingrediente);
                }
                else
                {
                    await App.Database.UpdateIngrediente(ingrediente);
                    mensagem = "Ingrediente alterado";
                }

                await DisplayAlert("Sucesso!", mensagem, "OK");

                await Navigation.PushAsync(new Listagem
                {
                    BindingContext = ingrediente
                }); 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}