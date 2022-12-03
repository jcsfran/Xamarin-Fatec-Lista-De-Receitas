using System;
using System.Collections.Generic;
using SQLite;

namespace JulioCesarApp.Model
{
    public class Receita
    {
        string _nome;
        string _modo_preparo;

        [PrimaryKey, AutoIncrement]
        public int ReceitaId { get; set; }
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

        public string ModoPreparo
        {
            get => _modo_preparo;
            set
            {
                if (value == null)
                {
                    throw new Exception("O campo Modo de Preparo é obrigatório");
                }

                _modo_preparo = value;
            }
        }
    }
}
