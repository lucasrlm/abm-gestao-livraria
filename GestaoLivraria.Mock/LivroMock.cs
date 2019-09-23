using GestaoLivraria.Dados.Entidades;
using System.Collections.Generic;

namespace GestaoLivraria.Mock
{
    public class LivroMock
    {  
        public IEnumerable<Livro> ListarLivros()
        {
            return new List<Livro>()
            {
                Livro1(),
                Livro2()
            };
        }

        public Livro Livro1()
        {
            return new Livro()
            {
                Id = 1,
                Nome = "Livro1"
            };
        }

        public Livro Livro2()
        {
            return new Livro()
            {
                Id = 2,
                Nome = "Livro2"
            };
        }
    }
}
