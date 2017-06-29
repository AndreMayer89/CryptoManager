using System;

namespace CryptoManager.Entidades
{
    public class ErroConsultaApiEntidade
    {
        public Exception Excecao { get; set; }
        public TipoExchange Exchange { get; set; }
    }
}
