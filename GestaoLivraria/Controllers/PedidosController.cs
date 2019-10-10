using System;
using System.Collections.Generic;
using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.AtualizarPedido;
using GestaoLivraria.Dados.Modelos.CriarPedido;
using GestaoLivraria.Dados.Modelos.ListarPedidos;
using GestaoLivraria.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GestaoLivraria.Util.Enum;

namespace GestaoLivraria.Controllers
{
    /// <summary>
    /// Controller de pedidos
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoNegocio _pedidoNegocio = new PedidoNegocio();
        private static readonly HttpClient client = new HttpClient();
        private readonly string _URLPagamento = "https://localhost:5004/v1/pagamentos";
        private readonly string _URLAuditoria = "https://localhost:5003/v1/pedidos/pagamentos";
        private readonly string _URLAutenticacao = "https://localhost:5002/v1/users/authenticate";
        /// <summary>
        /// Lista todos os pedidos a partir dos filtros passados
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Retorno<Pedido>> Get([FromQuery]Requisicao<ListarPedidosRequisicao> requisicao)
        {
            var retorno = new Retorno<Pedido>()
            {
                Itens = _pedidoNegocio.ListarPedidos(requisicao)
            };

            if (retorno.Itens == null || retorno.Itens.Count() == 0)
                return NoContent();

            return Ok(retorno.RetornarPaginadoOrdenado
                (requisicao.PropriedadeOrdenacao,
                 requisicao.OrdemDecrescente,
                 requisicao.Deslocamento,
                 requisicao.TamanhoPagina));
        }

        /// <summary>
        /// Cria um pedido
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Pedido> Post([FromBody]CriarPedidoRequisicao requisicao)
        {
            return Ok(_pedidoNegocio.CriarPedido(requisicao));
        }

        /// <summary>
        /// Atualiza um pedido
        /// </summary>
        /// <param name="requisicao"></param>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        [HttpPut("{pedidoId}")]
        public ActionResult<Pedido> Put([FromBody]AtualizarPedidoRequisicao requisicao, int pedidoId)
        {
            return Ok(_pedidoNegocio.AtualizarPedido(requisicao, pedidoId));
        }

        /// <summary>
        /// Realiza pagamento
        /// </summary>
        /// <param name="requisicao"></param>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        [HttpPut("{pedidoId}/pagamentos")]
        public async Task<Pedido> PutPedido(int pedidoId, [FromBody]Usuario usuario)
        {
            //gerar pagamento através do pedido (normalmente haveria alguma função para calcular o valor a se pagar com base no pedido)
            Cartao cartao = new Cartao
            {
                Id = 1,
                Bandeira = "Visa",
                NomeProprietario = "José das Graças",
                MesExpiracao = 8,
                AnoExpiracao = 23,
                IdUsuario = 1,
                CodigoSeguranca = 145
            };
            
            Pagamento pagamento = new Pagamento
            {
                Id = 1,
                Valor = 500,
                Moeda = "BRL",
                IdUsuario = 1,
                Cartao = cartao
            };

            //enviar o pagamento para a api de autenticação
            var respostaAutenticacao = await client.PostAsJsonAsync(_URLAutenticacao, usuario);
            var respostaAutenticacaoString = await respostaAutenticacao.Content.ReadAsStringAsync();

            if (!respostaAutenticacao.IsSuccessStatusCode)
            {
                throw new Exception(respostaAutenticacaoString);
            }

            //enviar o pagamento para a api de transação de cartão de crédito
            var respostaPagamento = await client.PostAsJsonAsync(_URLPagamento, pagamento);
            var respostaPagamentoString = await respostaPagamento.Content.ReadAsStringAsync();

            if (!respostaPagamento.IsSuccessStatusCode)
            {
                throw new Exception(respostaPagamentoString);
            }

            //enviar o pagamento para a api de log
            var respostaAuditoria = await client.PostAsJsonAsync(_URLAuditoria, pagamento);
            var respostaAuditoriaString = await respostaAuditoria.Content.ReadAsStringAsync();
            
            if (!respostaAuditoria.IsSuccessStatusCode)
            {
                throw new Exception(respostaAuditoriaString);
            }
            
            //atualizar a requisição (status do pedido)
            AtualizarPedidoRequisicao requisicao = new AtualizarPedidoRequisicao
            {
                StatusPedido = StatusPedido.Realizado,
                UsuarioNome = "José das Graças",
                LivrosId = new List<int>
                {
                    1,2,3,4,5
                }
                
            };
            
            return _pedidoNegocio.AtualizarPedido(requisicao, pedidoId);
        }
    }
}
