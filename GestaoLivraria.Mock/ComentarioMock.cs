using GestaoLivraria.Dados.Entidades;
using System.Collections.Generic;

namespace GestaoLivraria.Mock
{
    public class ComentarioMock
    {
        public List<Comentario> ListarComentarios()
        {
            return new List<Comentario>()
            {
                Comentario1(),
                Comentario2(),
                Comentario3(),
            };
        }

        private Comentario Comentario1()
        {
            return new Comentario()
            {
                Id = 1,
                LivroId = 1,
                Texto = "Comentário 1",
                UsuarioNome = "Lucas"
            };
        }

        private Comentario Comentario2()
        {
            return new Comentario()
            {
                Id = 2,
                LivroId = 2,
                Texto = "Comentário 2",
                UsuarioNome = "Lucas"
            };
        }

        private Comentario Comentario3()
        {
            return new Comentario()
            {
                Id = 3,
                LivroId = 3,
                Texto = "Comentário 3",
                UsuarioNome = "Lucas"
            };
        }
    }
}
