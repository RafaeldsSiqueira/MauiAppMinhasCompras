using System;
using System.Linq;
using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
{
    public ListaProduto()
    {
        InitializeComponent();
        // CarregarProdutos será chamado no OnAppearing
    }

    private async Task CarregarProdutos()
    {
        try
        {
            var produtos = await App.Db.GetAll();
            lst_produtos.ItemsSource = produtos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao carregar produtos: {ex.Message}", "OK");
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoProduto());
    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            var produtos = await App.Db.GetAll();
            double total = produtos.Sum(p => p.Total);
            await DisplayAlert("Total das Compras", $"Valor total: {total:C}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao calcular total: {ex.Message}", "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                await CarregarProdutos();
            }
            else
            {
                var produtos = await App.Db.Search(e.NewTextValue);
                lst_produtos.ItemsSource = produtos;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro na busca: {ex.Message}", "OK");
        }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        await CarregarProdutos();
        lst_produtos.IsRefreshing = false;
    }

    private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Produto produto)
        {
            var editarPage = new EditarProduto();
            editarPage.BindingContext = produto;
            await Navigation.PushAsync(editarPage);
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var menuItem = sender as MenuItem;
            var produto = menuItem?.CommandParameter as Produto;
            
            if (produto != null)
            {
                bool confirmar = await DisplayAlert("Confirmar", 
                    $"Deseja realmente remover o produto '{produto.Descricao}'?", 
                    "Sim", "Não");
                
                if (confirmar)
                {
                    await App.Db.Delete(produto.Id);
                    await DisplayAlert("Sucesso", "Produto removido com sucesso!", "OK");
                    await CarregarProdutos();
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao remover produto: {ex.Message}", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarProdutos();
    }
}
}