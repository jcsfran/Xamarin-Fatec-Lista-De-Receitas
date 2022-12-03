using System;
using SQLite;
namespace JulioCesarApp.Model
{
    public class Ingrediente
    {
        string _nome;
        string _quantidade;
        string _medida;
        int _receitaId;

        [PrimaryKey, AutoIncrement]
        public int IngredienteId { get; set; }

        [Indexed]
        public int ReceitaId
        {
            get => _receitaId;
            set => _receitaId = value;
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if (value == null)
                {
                    throw new Exception("O campo Nome é obrigatório");
                }

                _nome = value;
            }
        }

        public string Quantidade
        {
            get => _quantidade;
            set
            {
                if (value == null)
                {
                    throw new Exception("O campo Quantidade é obrigatório");
                }

                _quantidade = value;
            }
        }

        public string Medida
        {
            get => _medida;
            set
            {
                if (value == null)
                {
                    throw new Exception("O campo Medida é obrigatório");
                }

                _medida = value;
            }
        }
    }
}
