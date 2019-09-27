using GestaoLivraria.Util.Enum;

namespace GestaoLivraria.Dados.Modelos.ListarPedidos
{
    public class ListarPedidosRequisicao
    {
        public int? Id { get; set; }
        public int? LivroId { get; set; }
        public StatusPedido? StatusPedido { get; set; }
        public string UsuarioNome { get; set; }
    }
}
