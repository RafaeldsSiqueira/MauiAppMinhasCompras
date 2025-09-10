using MauiAppMinhasCompras.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // =========================
        // Create
        // =========================
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // =========================
        // Update
        // =========================
        // Melhoria 1: Usar UpdateAsync em vez de QueryAsync para retornar o número de linhas afetadas
        public Task<int> Update(Produto p)
        {
            return _conn.UpdateAsync(p);
        }

        // =========================
        // Delete
        // =========================
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>()
                        .DeleteAsync(i => i.Id == id);
        }

        // =========================
        // Read all
        // =========================
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        // =========================
        // Search
        // =========================
        // Busca segura usando parâmetro SQL para evitar SQL Injection
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";
            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }

        // =========================
        // Read by Id
        // =========================
        // Melhoria 4: Usar SQL direto para evitar problemas de tipo com Id
        public Task<Produto?> GetById(int id)
        {
            string sql = "SELECT * FROM Produto WHERE Id = ?";
            return _conn.QueryAsync<Produto>(sql, id)
                        .ContinueWith(t => t.Result.FirstOrDefault());
        }
    }
}
