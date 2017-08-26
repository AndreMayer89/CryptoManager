using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoManager.Entidades
{
    public class TipoCrypto
    {
        private static readonly List<TipoCrypto> Lista = new List<TipoCrypto>();

        public static readonly TipoCrypto Real = Criar("Real", "BRL");
        public static readonly TipoCrypto Bitcoin = Criar("Bitcoin", "BTC");
        public static readonly TipoCrypto Ethereum = Criar("Ethereum", "ETH");
        public static readonly TipoCrypto Ripple = Criar("Ripple", "XRP");
        public static readonly TipoCrypto NEM = Criar("NEM", "XEM");
        public static readonly TipoCrypto EthereumClassic = Criar("EthereumClassic", "ETC");
        public static readonly TipoCrypto Litecoin = Criar("Litecoin", "LTC");
        public static readonly TipoCrypto Dash = Criar("Dash", "DASH");
        public static readonly TipoCrypto Stratis = Criar("Stratis", "STRAT");
        public static readonly TipoCrypto Monero = Criar("Monero", "XMR");
        public static readonly TipoCrypto Bytecoin = Criar("Bytecoin", "BCN");
        public static readonly TipoCrypto Waves = Criar("Waves", "WAVES");
        public static readonly TipoCrypto Stellar = Criar("Stellar", "STR");
        public static readonly TipoCrypto Steem = Criar("Steem", "STEEM");
        public static readonly TipoCrypto Golem = Criar("Golem", "GNT");
        public static readonly TipoCrypto Digibyte = Criar("Digibyte", "DGB");
        public static readonly TipoCrypto Siacoin = Criar("Siacoin", "SC");
        public static readonly TipoCrypto GameCredits = Criar("GameCredits", "GAME");
        public static readonly TipoCrypto Lisk = Criar("Lisk", "LSK");
        public static readonly TipoCrypto Factom = Criar("Factom", "FCT");
        public static readonly TipoCrypto Maid = Criar("Maid", "MAID");
        public static readonly TipoCrypto BasicAttentionToken = Criar("BasicAttentionToken", "BAT");
        public static readonly TipoCrypto Decred = Criar("Decred", "DCR");
        public static readonly TipoCrypto Iconomi = Criar("Iconomi", "ICN");
        public static readonly TipoCrypto Byteball = Criar("Byteball", "GBYTE");
        public static readonly TipoCrypto WeTrust = Criar("WeTrust", "TRST");
        public static readonly TipoCrypto FirstBlood = Criar("FirstBlood", "1ST");
        public static readonly TipoCrypto Verge = Criar("Verge", "XVG");
        public static readonly TipoCrypto Ardor = Criar("Ardor", "ARDR");
        public static readonly TipoCrypto NXT = Criar("NXT", "NXT");
        public static readonly TipoCrypto FoldingCoin = Criar("FoldingCoin", "FLDC");
        public static readonly TipoCrypto Gnosis = Criar("Gnosis", "GNO");
        public static readonly TipoCrypto EdgeLess = Criar("EdgeLess", "EDG");
        public static readonly TipoCrypto iExec = Criar("iExec", "RLC");
        public static readonly TipoCrypto Augur = Criar("Augur", "REP");
        public static readonly TipoCrypto Lumen = Criar("Lumen", "XLM");
        public static readonly TipoCrypto IOTA = Criar("IOTA", "IOT");
        public static readonly TipoCrypto Bitshares = Criar("Bitshares", "BTS");
        public static readonly TipoCrypto Zcash = Criar("Zcash", "ZEC");
        public static readonly TipoCrypto Dogecoin = Criar("Dogecoin", "DOGE");
        public static readonly TipoCrypto Bitconnect = Criar("Bitconnect", "BCC");
        public static readonly TipoCrypto Komodo = Criar("Komodo", "KMD");
        public static readonly TipoCrypto NEO = Criar("NEO", "NEO");
        public static readonly TipoCrypto DigixDAO = Criar("DigixDAO", "DGD");
        public static readonly TipoCrypto QuantumResistantLedger = Criar("Quantum Resistant Ledger", "QRL");
        public static readonly TipoCrypto Voxels = Criar("Voxels", "VOX");
        public static readonly TipoCrypto ReddCoin = Criar("ReddCoin", "RDD");
        public static readonly TipoCrypto LBRYCredits = Criar("LBRY Credits", "LBC");
        public static readonly TipoCrypto Ubiq = Criar("Ubiq", "UBQ");
        public static readonly TipoCrypto Aragon = Criar("Aragon", "ANT");
        public static readonly TipoCrypto Wings = Criar("Wings", "WINGS");
        public static readonly TipoCrypto Humaniq = Criar("Humaniq", "HMQ");
        public static readonly TipoCrypto Counterparty = Criar("Counterparty", "XCP");
        public static readonly TipoCrypto ZCoin = Criar("ZCoin", "XVC");
        public static readonly TipoCrypto SwarmCity = Criar("SwarmCity", "SWT");
        public static readonly TipoCrypto PIVX = Criar("PIVX", "PIVX");
        public static readonly TipoCrypto Influxcoin = Criar("Influxcoin", "INFX");
        public static readonly TipoCrypto BitcoinDark = Criar("BitcoinDark", "BTCD");
        public static readonly TipoCrypto ZClassic = Criar("ZClassic", "ZCL");
        public static readonly TipoCrypto VeriCoin = Criar("VeriCoin", "VRC");
        public static readonly TipoCrypto Ark = Criar("Ark", "ARK");
        public static readonly TipoCrypto Peercoin = Criar("Peercoin", "PPC");
        public static readonly TipoCrypto DigitalNote = Criar("DigitalNote", "XDN");
        public static readonly TipoCrypto Credibit = Criar("Credibit", "CRB");
        public static readonly TipoCrypto ZenCash = Criar("ZenCash", "ZEN");
        public static readonly TipoCrypto Syscoin = Criar("Syscoin", "SYS");
        public static readonly TipoCrypto Viacoin = Criar("Viacoin", "VIA");
        public static readonly TipoCrypto SingularDTV = Criar("SingularDTV", "SNGLS");
        public static readonly TipoCrypto Chronobank = Criar("Chronobank", "TIME");
        public static readonly TipoCrypto TokenCard = Criar("TokenCard", "TKN");
        public static readonly TipoCrypto Bancor = Criar("Bancor", "BNT");
        public static readonly TipoCrypto Status = Criar("Status", "SNT");
        public static readonly TipoCrypto BitcoinCash = Criar("BitcoinCash", "BCH");
        public static readonly TipoCrypto ZeroX = Criar("0X", "ZRX");
        public static readonly TipoCrypto OmiseGo = Criar("OmiseGo", "OMG");
        public static readonly TipoCrypto EOS = Criar("EOS", "EOS");
        public static readonly TipoCrypto Santiment = Criar("Santiment", "SAN");


        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Sigla { get; private set; }

        private TipoCrypto(int id, string nome, string sigla)
        {
            Id = id;
            Nome = nome;
            Sigla = sigla;
        }

        private static TipoCrypto Criar(string nome, string sigla)
        {
            TipoCrypto tipo = new TipoCrypto(Lista.Count, nome, sigla);
            Lista.Add(tipo);
            return tipo;
        }

        public static TipoCrypto Obter(int id)
        {
            return Lista.FirstOrDefault(t => t.Id == id);
        }

        public static TipoCrypto ObterPorNome(string nome)
        {
            return Lista.FirstOrDefault(t => string.Equals(t.Nome, nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public static TipoCrypto ObterPorSigla(string sigla)
        {
            TipoCrypto tipo = Lista.FirstOrDefault(t => string.Equals(t.Sigla, sigla, StringComparison.InvariantCultureIgnoreCase));
            return tipo == null ? new TipoCrypto(-1, sigla, sigla) : tipo;
        }

        public static List<TipoCrypto> ListarTodasParaInputTela()
        {
            List<TipoCrypto> lista = new List<TipoCrypto>();
            lista.AddRange(Lista.Where(c => c.Sigla != "BRL"));
            return lista;
        }
    }
}
