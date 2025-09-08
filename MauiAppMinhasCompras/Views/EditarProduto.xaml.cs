using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;

            if (produto_anexado == null)
            {
                await DisplayAlert("Erro", "Produto não encontrado.", "OK");
                return;
            }

            // Validação dos campos
            if (string.IsNullOrWhiteSpace(txt_descricao.Text))
            {
                await DisplayAlert("Erro", "Por favor, preencha a descrição do produto.", "OK");
                return;
            }

            if (!double.TryParse(txt_quantidade.Text, out double quantidade) || quantidade <= 0)
            {
                await DisplayAlert("Erro", "Por favor, insira uma quantidade válida maior que zero.", "OK");
                return;
            }

            if (!double.TryParse(txt_preco.Text, out double preco) || preco <= 0)
            {
                await DisplayAlert("Erro", "Por favor, insira um preço válido maior que zero.", "OK");
                return;
            }

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text.Trim(),
                Quantidade = quantidade,
                Preco = preco
            };

            await App.DB.Update(p);
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
