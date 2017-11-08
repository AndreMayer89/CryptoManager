using CryptoManager.Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CryptoManager.Controllers
{
    [RoutePrefix("api/cotacoes")]
    public class CotacoesController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("cotar")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public HttpResponseMessage Cotar(int? idExchange)
        {
            return CriarRespostaOk(new ConsultaExchangesBusiness().Cotar(idExchange));
        }

        protected HttpResponseMessage CriarRespostaOk()
        {
            return CriarRespostaOk(new { });
        }

        protected HttpResponseMessage CriarRespostaOk(object resposta)
        {
            return Request.CreateResponse(HttpStatusCode.OK, resposta, "application/json");
        }
    }
}
