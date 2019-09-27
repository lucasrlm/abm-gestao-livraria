using GestaoLivraria.Util.Enum;

namespace GestaoLivraria.Dados.Entidades
{
    public class PedidoCabecalho
    {
        public int Id { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public string UsuarioNome { get; set; }
    }
}
