using System.Collections.Generic;

namespace GestaoLivraria.Dados.Modelos
{
    public class Retorno<T>
    {
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalItens { get; set; }
        public IEnumerable<T> Itens { get; set; }
    }
}
