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
using System.Net.Http;
using System.Threading.Tasks;
using GestaoLivraria.Util.Enum;
using Microsoft.Extensions.Configuration;

namespace GestaoLivraria.Controllers
{
    /// <summary>
    /// Controller de pedidos
    /// </summary>
    [Route("v1/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoNegocio _pedidoNegocio = new PedidoNegocio();
        private static readonly HttpClient _clienteHttp = new HttpClient();
        private IConfiguration _configuracao;
        
        public PedidosController(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

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
        /// <param name="usuario"></param>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        [HttpPut("{pedidoId}/pagamentos")]
        public async Task<Pedido> PutPedido(int pedidoId, [FromBody]Usuario usuario)
        {
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

            var respostaAutenticacao = await _clienteHttp.PostAsJsonAsync(_configuracao.GetSection("Microsservicos:Autenticacao").Value, usuario);
            var respostaAutenticacaoMensagem = await respostaAutenticacao.Content.ReadAsStringAsync();

            if (!respostaAutenticacao.IsSuccessStatusCode)
            {
                throw new Exception(respostaAutenticacaoMensagem);
            }

            var respostaPagamento = await _clienteHttp.PostAsJsonAsync(_configuracao.GetSection("Microsservicos:Pagamentos").Value, pagamento);
            var respostaPagamentoMensagem = await respostaPagamento.Content.ReadAsStringAsync();

            if (!respostaPagamento.IsSuccessStatusCode)
            {
                throw new Exception(respostaPagamentoMensagem);
            }

            var respostaAuditoria = await _clienteHttp.PostAsJsonAsync(_configuracao.GetSection("Microsservicos:Auditoria").Value, pagamento);
            var respostaAuditoriaMensagem = await respostaAuditoria.Content.ReadAsStringAsync();
            
            if (!respostaAuditoria.IsSuccessStatusCode)
            {
                throw new Exception(respostaAuditoriaMensagem);
            }
            
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
