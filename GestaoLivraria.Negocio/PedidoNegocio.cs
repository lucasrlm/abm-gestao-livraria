using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.AtualizarPedido;
using GestaoLivraria.Dados.Modelos.CriarPedido;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Repositorio;
using GestaoLivraria.Util;
using GestaoLivraria.Util.Enum;
using GestaoLivraria.Util.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Negocio
{
    public class PedidoNegocio
    {
        private readonly PedidoRepositorio _pedidoRepositorio = new PedidoRepositorio();

        public IEnumerable<Pedido> ListarPedidos(Requisicao<ListarPedidosRequisicao> requisicao)
        {
            requisicao.ValidarParametros();
            ValidarParametrosListarPedidos(requisicao.Filtros);

            return _pedidoRepositorio.ListarPedidos(requisicao);
        }

        public Pedido CriarPedido(CriarPedidoRequisicao requisicao)
        {
            ValidarParametrosCriarPedido(requisicao);

            var pedidoDetalhes = new List<PedidoDetalhes>();
            var pedidoDetalhesId = 1;

            foreach (var livro in requisicao.LivrosId)
            {
                pedidoDetalhes.Add(new PedidoDetalhes()
                {
                    Id = pedidoDetalhesId,
                    LivroId = livro,
                    PedidoCabecalhoId = 1
                });

                pedidoDetalhesId += 1;
            }

            return new Pedido()
            {
                PedidoCabecalho = new PedidoCabecalho()
                {
                    Id = 1,
                    StatusPedido = StatusPedido.Aberto,
                    UsuarioNome = requisicao.UsuarioNome
                },
                PedidoDetalhes = pedidoDetalhes
            };
        }

        public Pedido AtualizarPedido(AtualizarPedidoRequisicao requisicao, int pedidoId)
        {
            ValidarParametrosAtualizarPedido(requisicao, pedidoId);

            var pedidoDetalhes = new List<PedidoDetalhes>();
            var pedidoDetalhesId = 1;

            foreach (var livro in requisicao.LivrosId)
            {
                pedidoDetalhes.Add(new PedidoDetalhes()
                {
                    Id = pedidoDetalhesId,
                    LivroId = livro,
                    PedidoCabecalhoId = pedidoId
                });

                pedidoDetalhesId += 1;
            }

            return new Pedido()
            {
                PedidoCabecalho = new PedidoCabecalho()
                {
                    Id = pedidoId,
                    StatusPedido = StatusPedido.Aberto,
                    UsuarioNome = requisicao.UsuarioNome
                },
                PedidoDetalhes = pedidoDetalhes
            };
        }

        public void ValidarParametrosListarPedidos(ListarPedidosRequisicao requisicao)
        {
            if (requisicao?.Id <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(requisicao.Id)));
            if (requisicao?.LivroId <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(requisicao.LivroId)));
        }

        public void ValidarParametrosCriarPedido(CriarPedidoRequisicao requisicao)
        {
            if (requisicao.LivrosId == null || requisicao.LivrosId.Count() == 0 || requisicao.LivrosId.Any(l => l <= 0))
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(requisicao.LivrosId)));
        }

        public void ValidarParametrosAtualizarPedido(AtualizarPedidoRequisicao requisicao, int pedidoId)
        {
            if (pedidoId <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(pedidoId)));
            if (requisicao.LivrosId == null || requisicao.LivrosId.Count() == 0 || requisicao.LivrosId.Any(l => l <= 0))
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(requisicao.LivrosId)));
            if (requisicao.StatusPedido <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(requisicao.StatusPedido)));
        }
    }
}
