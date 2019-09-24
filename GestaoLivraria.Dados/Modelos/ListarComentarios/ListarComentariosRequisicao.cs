using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoLivraria.Dados.Modelos.ListarComentarios
{
    public class ListarComentariosRequisicao
    {
        public int? Id { get; set; }

        public int? LivroId { get; set; }

        public string TextoAutocomplete { get; set; }
    }
}
