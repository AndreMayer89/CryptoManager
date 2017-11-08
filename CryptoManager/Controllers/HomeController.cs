using CryptoManager.Business;
using CryptoManager.Entidades;
using CryptoManager.Entidades.Especificas;
using CryptoManager.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using CryptoManager.Util.Cache;
using System;

namespace CryptoManager.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            HomeModel model = new HomeModel();
            model.CotacoesBtc = new CotacoesBtcModel();
            return View(model);
        }
        
        public FileResult DownloadWallet(string chaveSessao, string senha)
        {
            var entidade = CmCache.Obter().ObterItem<CarteiraEntradaEntidade>(chaveSessao);
            byte[] fileBytes = Encrypt(JsonConvert.SerializeObject(entidade), senha);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Carteira.wallet");
        }

        public JsonResult Login(HttpPostedFileBase arquivo, string senha)
        {
            byte[] data = new byte[arquivo.ContentLength];
            arquivo.InputStream.Read(data, 0, data.Length);

            var carteiraConsolidadaJson = Decrypt(data, senha);
            var carteira = ObterEntradaApi<CarteiraEntradaEntidade>(carteiraConsolidadaJson);

            HomeModel model = ObterModel(carteira);

            return Json(new
            {
                html = RenderizarVisaoParcial("_CorpoPagina", model),
                listaCryptos = model.GridListaCryptos.Lista,
                logou = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterCorpoPagina(double totalInvestido, string entradaPolo, string entradaBittrex,
            string entradaKraken, string entradaBitfinex, string listaBalancoColdWalletString, string listaComprasColdWalletString)
        {
            var entradaPoloEntidade = ObterEntradaApi<PoloniexEntradaApiEntidade>(entradaPolo);
            var entradaBittrexEntidade = ObterEntradaApi<BittrexEntradaApiEntidade>(entradaBittrex);
            var entradaKrakenEntidade = ObterEntradaApi<KrakenEntradaApiEntidade>(entradaKraken);
            var entradaBitfinexEntidade = ObterEntradaApi<BitfinexEntradaApiEntidade>(entradaBitfinex);
            var listaBalancoColdWallet = ObterEntradaApi<List<MoedaEmCarteiraEntidade>>(listaBalancoColdWalletString);
            var listaComprasColdWallet = ObterEntradaApi<List<CompraMoedaEmColdEntidade>>(listaComprasColdWalletString);

            var carteiraEntrada = new CarteiraEntradaEntidade
            {
                Investido = totalInvestido,
                Poloniex = entradaPoloEntidade,
                Bittrex = entradaBittrexEntidade,
                Kraken = entradaKrakenEntidade,
                Bitfinex = entradaBitfinexEntidade,
                BalancoCold = listaBalancoColdWallet,
                ComprasCold = listaComprasColdWallet
            };

            HomeModel model = ObterModel(carteiraEntrada);
            string chaveSessao = Guid.NewGuid().ToString();
            CmCache.Obter().AdicionarItem(chaveSessao, carteiraEntrada, CmCache.TEMPO_CACHE_10_MINUTOS);
            return Json(new
            {
                html = RenderizarVisaoParcial("_CorpoPagina", model),
                listaCryptos = model.GridListaCryptos.Lista,
                chaveSessao = chaveSessao
            }, JsonRequestBehavior.AllowGet);
        }
        
        private HomeModel ObterModel(CarteiraEntradaEntidade carteiraEntrada)
        {
            HomeModel model = new HomeModel();
            model.CotacoesBtc = ObterCotacoesBtc();

            model.GridListaCryptos = ObterModelListaCryptos(model.ValorBrlBtcDouble, model.ValorUsdBtcDouble, 
                model.ValorBrlBtcRealDouble, carteiraEntrada.Poloniex, carteiraEntrada.Bittrex, carteiraEntrada.Kraken, carteiraEntrada.Bitfinex,
                carteiraEntrada.BalancoCold, carteiraEntrada.ComprasCold);
            model.ValorTotalInvestido = carteiraEntrada.Investido;
            PreencherSecaoExchange(model.Poloniex, carteiraEntrada.Poloniex);
            PreencherSecaoExchange(model.Bittrex, carteiraEntrada.Bittrex);
            PreencherSecaoExchange(model.Kraken, carteiraEntrada.Kraken);
            PreencherSecaoExchange(model.Bitfinex, carteiraEntrada.Bitfinex);
            model.GridInputCompraCold.ListaCompras = carteiraEntrada.ComprasCold;
            model.GridBalancoCold.ListaBalancos = carteiraEntrada.BalancoCold;
            return model;
        }

        private GridListaCryptosModel ObterModelListaCryptos(double valorBrlBtc, double valorUsdBtc, double valorBrlBtcReal, 
            PoloniexEntradaApiEntidade entradaPoloniex, BittrexEntradaApiEntidade entradaBittrex, KrakenEntradaApiEntidade entradaKraken,
            BitfinexEntradaApiEntidade entradaBitfinex, List<MoedaEmCarteiraEntidade> listaBalancoColdWallet,
            List<CompraMoedaEmColdEntidade> listaComprasColdWallet)
        {
            List<CryptoModel> lista = new List<CryptoModel>();
            var consulta = new ConsultaExchangesBusiness().ConsultarExchanges(entradaPoloniex,
                entradaBittrex, entradaKraken, entradaBitfinex, listaBalancoColdWallet, listaComprasColdWallet);
            foreach (var quantidade in consulta.ListaQuantidades)
            {
                if (quantidade.Quantidade > 0)
                {
                    double valorTotalEmBtc = ConsultaExchangesBusiness.ObterValorTotalEmBtc(consulta.ListaCotacoes, quantidade);
                    double valorUnidadeEmBtc = quantidade.Quantidade > 0 ? (valorTotalEmBtc / quantidade.Quantidade) : 0;
                    double valorTotalBrl = valorTotalEmBtc * valorBrlBtc;
                    double valorTotalBrlReal = valorTotalEmBtc * valorBrlBtcReal;
                    if (valorTotalBrl > 0 && valorTotalBrlReal > 0)
                    {
                        double valorTotalDolares = valorTotalEmBtc * valorUsdBtc;
                        double porcentagemValorizacaoBtc = quantidade.QuantidadeBtcInvestida > 0
                            ? (100 * ((valorTotalEmBtc - quantidade.QuantidadeBtcInvestida) / quantidade.QuantidadeBtcInvestida)) : 0;
                        lista.Add(new CryptoModel
                        {
                            NomeCrypto = quantidade.Tipo.Nome,
                            SiglaCrypto = quantidade.Tipo.Sigla,
                            QuantidadeCryptoDouble = quantidade.Quantidade,
                            QuantidadeBtcInvestida = quantidade.QuantidadeBtcInvestida,
                            ValorCryptoBtc = valorUnidadeEmBtc.ToString("N8"),
                            ValorTotalBtcDouble = valorTotalEmBtc,
                            PorcentagemValorizacaoEmRelacaoAoBtc = porcentagemValorizacaoBtc.ToString("N2"),
                            PorcentagemValorizacaoEmRelacaoAoBtcDouble = porcentagemValorizacaoBtc.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")),
                            ValorTotalBrlDouble = valorTotalBrl,
                            ValorTotalBrlRealDouble = valorTotalBrlReal,
                            ValorTotalDolaresDouble = valorTotalDolares
                        });
                    }
                }
            }
            var model = new GridListaCryptosModel(valorBrlBtc, valorBrlBtcReal)
            {
                Lista = lista.OrderByDescending(c => c.ValorTotalBrlRealDouble).ToList(),
                ResultadosOperacao = ObterListaResultadosOperacao(consulta.ListaResultadosOperacoesExchanges)
            };
            model.Lista.ForEach(c => c.PercentualRelativoDouble = (100 * c.ValorTotalBtcDouble / model.ValorTotalBtcDouble));
            return model;
        }

        private List<ResultadoOperacaoModel> ObterListaResultadosOperacao(List<ResultadoOperacaoEntidade> listaResultadosOperacoesExchanges)
        {
            List<ResultadoOperacaoModel> listaRetorno = new List<ResultadoOperacaoModel>();
            foreach (var entidade in listaResultadosOperacoesExchanges)
            {
                listaRetorno.AddRange(entidade.ListaTipoOperacao.Select(e => new ResultadoOperacaoModel()
                {
                    Exchange = entidade.Exchange,
                    MensagemErro = e.MensagemErro,
                    TempoExecucao = e.Milisegundos,
                    TipoOperacao = e.Operacao
                }));
            }
            return listaRetorno;
        }

        private void PreencherSecaoExchange(SecaoInputExchangeModel secaoInputExchange, BaseEntradaApiEntidade entrada)
        {
            if (entrada != null)
            {
                secaoInputExchange.Key = entrada.ApiKey;
                secaoInputExchange.Secret = entrada.ApiSecret;
            }
        }

        private CotacoesBtcModel ObterCotacoesBtc()
        {
            var cotacoesEntidade = CotacoesBusiness.ObterCotacoesBtc();
            return new CotacoesBtcModel()
            {
                BtcBrlFoxbit = cotacoesEntidade.BtcBrlFoxbit,
                BtcUsd = cotacoesEntidade.BtcUsd,
                UsdBrl = cotacoesEntidade.UsdBrl
            };
        }

        private T ObterEntradaApi<T>(string entradaEmString)
        {
            T entradaEntidade = default(T);
            if (!string.IsNullOrWhiteSpace(entradaEmString))
            {
                entradaEntidade = JsonConvert.DeserializeObject<T>(entradaEmString);
            }
            return entradaEntidade;
        }

        private string RenderizarVisaoParcial(string nomeVisao, object model)
        {
            if (string.IsNullOrEmpty(nomeVisao))
            {
                nomeVisao = ControllerContext.RouteData.GetRequiredString("action");
            }
            if (model != null)
            {
                ViewData.Model = model;
            }
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, nomeVisao);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        private const string initVector = "tu89geji340t89u2";

        private const int keysize = 256;

        private static byte[] Encrypt(string Text, string Key)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Encrypted;
        }

        private static string Decrypt(byte[] EncryptedText, string Key)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] DeEncryptedText = EncryptedText;
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[DeEncryptedText.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

    }
}