using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JulioCesarApp.View.Receita
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        public Formulario()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (BindingContext != null)
            {
                tbt_ingredientes.Text = "Ingredientes";
                tbt_ingredientes.IsEnabled = true;

                bloquearCampos();
            }
        }

        private async void ToolbarItem_Salvar_Clicked(object sender, EventArgs e)
        {
            if (tbt_salvar.Text == "🖋 Editar")
            {
                tbt_salvar.Text = "Salvar";
                txt_nome.IsEnabled = true;
                txt_modo_preparo.IsEnabled = true;

                return;
            }

            try
            {
                Model.Receita receita_selecionada = new Model.Receita();

                if (BindingContext != null)
                {
                    receita_selecionada = BindingContext as Model.Receita;
                }
              
                Model.Receita receita = new Model.Receita()
                {
                    ReceitaId = receita_selecionada.ReceitaId,
                    Nome = txt_nome.Text,
                    ModoPreparo = txt_modo_preparo.Text,
                };

                if (receita.ReceitaId == 0)
                {
                    receita = App.Database.Insert(receita);

                    await DisplayAlert("Sucesso!", "Receita Inserida", "OK");

                    await Navigation.PushAsync(new Ingrediente.Listagem
                    {
                        BindingContext = receita
                    });

                }
                else
                {
                    await App.Database.Update(receita);

                    await DisplayAlert("Sucesso!", "Receita atualizada", "OK");
                    bloquearCampos();

                    return;
                } 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void ToolbarItem_Listar_Ingredientes_Clicked(object sender, EventArgs e)
        {
            if (BindingContext != null)
            {
                Model.Receita receita = new Model.Receita();

                receita = BindingContext as Model.Receita;

                await Navigation.PushAsync(new Ingrediente.Listagem
                {
                    BindingContext = receita
                });
            }
        }

        private void bloquearCampos()
        {
            tbt_salvar.Text = "🖋 Editar";
            txt_nome.IsEnabled = false;
            txt_modo_preparo.IsEnabled = false;
        }
    }
}