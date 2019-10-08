
using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Mock;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Repositorio
{
    public class PedidoRepositorio
    {
        public IEnumerable<Pedido> ListarPedidos(Requisicao<ListarPedidosRequisicao> requisicao)
        {
            var tabelaPedidoCabecalho = new PedidoCabecalhoMock().ListarPedidosCabecalho();
            var tabelaPedidoDetalhes = new PedidoDetalhesMock().ListarPedidoDetalhes();

            var tabelaPedido = new List<Pedido>();

            foreach (var pedido in tabelaPedidoCabecalho)
            {
                tabelaPedido.Add(new Pedido()
                {
                    PedidoCabecalho = pedido,
                    PedidoDetalhes = tabelaPedidoDetalhes.Where(p => p.PedidoCabecalhoId == pedido.Id)
                });
            }

            var pedidos = tabelaPedido.ToList();

            if (requisicao != null && requisicao.Filtros != null)
            {
                if (requisicao.Filtros.Id.HasValue)
                    pedidos = tabelaPedido.Where(p => p.PedidoCabecalho.Id == requisicao.Filtros.Id).ToList();

                else
                    pedidos = tabelaPedido.Where(p =>
                        (requisicao.Filtros.LivroId.HasValue ? p.PedidoDetalhes.Any(pd => pd.LivroId == requisicao.Filtros.LivroId) : true) &&
                        (requisicao.Filtros.StatusPedido.HasValue ? p.PedidoCabecalho.StatusPedido == requisicao.Filtros.StatusPedido : true) &&
                        (!string.IsNullOrEmpty(requisicao.Filtros.UsuarioNome) ? p.PedidoCabecalho.UsuarioNome.Contains(requisicao.Filtros.UsuarioNome) : true)).ToList();
            }

            return pedidos;
        }
    }
}
