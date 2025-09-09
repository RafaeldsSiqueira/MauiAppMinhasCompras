using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<Produto> ProdutosFiltrados { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Lista inicial de produtos (mock)
            Produtos = new ObservableCollection<Produto>
            {
                new Produto { Descricao = "Arroz" },
                new Produto { Descricao = "Feijão" },
                new Produto { Descricao = "Macarrão" },
                new Produto { Descricao = "Café" },
                new Produto { Descricao = "Leite" }
            };

            // Inicialmente exibe todos
            ProdutosFiltrados = new ObservableCollection<Produto>(Produtos);

            BindingContext = this;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue?.ToLower() ?? string.Empty;

            var filtered = Produtos
                .Where(p => p.Descricao.ToLower().Contains(searchText))
                .ToList();

            ProdutosFiltrados.Clear();
            foreach (var item in filtered)
                ProdutosFiltrados.Add(item);
        }
    }
}
