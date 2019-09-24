using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.ListarLivros;
using GestaoLivraria.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace GestaoLivraria.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroNegocio _livroNegocio = new LivroNegocio();

        // GET api/values
        [HttpGet]
        public ActionResult<Retorno<ListarLivrosRetorno>> Get([FromQuery]Requisicao<ListarLivrosRequisicao> requisicao)
        {
            return _livroNegocio.ListarLivros(requisicao).RetornarPaginadoOrdenado
                (requisicao.PropriedadeOrdenacao,
                 requisicao.OrdemDescendente,
                 requisicao.Deslocamento,
                 requisicao.TamanhoPagina);
        }

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
