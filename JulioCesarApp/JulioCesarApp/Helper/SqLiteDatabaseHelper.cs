using JulioCesarApp.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JulioCesarApp.Helper
{
    public partial class SqLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _connection;

        public SqLiteDatabaseHelper(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Receita>().Wait();
            _connection.CreateTableAsync<Ingrediente>().Wait();
        }

        public Task<List<Receita>> GetAll()
        {
            return _connection.Table<Receita>().ToListAsync();
        }

        public Receita Insert(Receita receita)
        {
            _connection.InsertAsync(receita);         

            string sql = "SELECT receitaId FROM Receita ORDER BY receitaId DESC LIMIT 1";
            
            Task<List<Receita>> lst_tmp =  _connection.QueryAsync<Receita>(sql);

            lst_tmp.Result.ForEach(x => receita.ReceitaId = x.ReceitaId);

            return receita;
        }

        public Task<List<Receita>> Search(string name)
        {
            string sql = "SELECT * FROM Receita WHERE nome LIKE '%" + name + "%'";

            return _connection.QueryAsync<Receita>(sql);
        }

        public Task<List<Receita>> Update(Receita receita)
        {
            string sql = "UPDATE Receita SET Nome = ?, ModoPreparo = ? WHERE receitaId = ?";

            return _connection.QueryAsync<Receita>(sql, receita.Nome, receita.ModoPreparo, receita.ReceitaId);
        }

        public Task<int> Delete(int id)
        {
            return _connection.Table<Receita>().DeleteAsync(i => i.ReceitaId == id);
        }

        public Task<int> InsertIngrediente(Ingrediente ingrediente)
        {
            return _connection.InsertAsync(ingrediente);
        }

        public Task<List<Ingrediente>> GetAllIngredientes(int receitaId)
        {
            string sql = "SELECT * FROM Ingrediente WHERE receitaId = " + receitaId;

            return _connection.QueryAsync<Ingrediente>(sql);
        }

        public Task<List<Ingrediente>> UpdateIngrediente(Ingrediente ingrediente)
        {
            string sql = "UPDATE Ingrediente SET Nome = ?, Quantidade = ?, Medida = ? WHERE ingredienteId = ?";

            return _connection.QueryAsync<Ingrediente>(sql, ingrediente.Nome, ingrediente.Quantidade, ingrediente.Medida, ingrediente.IngredienteId);
        }

        public Task<int> DeleteIngrediente(int id)
        {
            return _connection.Table<Ingrediente>().DeleteAsync(i => i.IngredienteId == id);
        }
    }
}
