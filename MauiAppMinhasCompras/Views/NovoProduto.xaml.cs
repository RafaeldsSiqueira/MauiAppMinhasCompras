using System;
using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MauiAppMinhasCompras.Views

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
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
                Descricao = txt_descricao.Text.Trim(),
                Quantidade = quantidade,
                Preco = preco
            };

            await App.DB.Insert(p);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        } // Fecha try-catch
    } // Fecha ToolbarItem_Clicked
} // Fecha classe NovoProduto