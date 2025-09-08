using System.Collections.ObjectModel;

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
                new Produto { Nome = "Arroz" },
                new Produto { Nome = "Feijão" },
                new Produto { Nome = "Macarrão" },
                new Produto { Nome = "Café" },
                new Produto { Nome = "Leite" }
            };

            // Inicialmente exibe todos
            ProdutosFiltrados = new ObservableCollection<Produto>(Produtos);

            BindingContext = this;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue?.ToLower() ?? string.Empty;

            var filtered = Produtos
                .Where(p => p.Nome.ToLower().Contains(searchText))
                .ToList();

            ProdutosFiltrados.Clear();
            foreach (var item in filtered)
                ProdutosFiltrados.Add(item);
        }
    }

    public class Produto
    {
        public string Nome { get; set; }
        public string Descricao { get; internal set; }
        public object Id { get; internal set; }
        public double Quantidade { get; internal set; }
        public double Preco { get; internal set; }
    }
}
