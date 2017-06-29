namespace CryptoManager.Entidades.Especificas
{
    public class PoloniexEntidade
    {
        public PoloniexMoedaEntidade BTC_GAME { get; set; }
        public PoloniexMoedaEntidade BTC_XMR { get; set; }
        public PoloniexMoedaEntidade BTC_ETH { get; set; }
        public PoloniexMoedaEntidade BTC_MAID { get; set; }
        public PoloniexMoedaEntidade BTC_DCR { get; set; }
        public PoloniexMoedaEntidade BTC_DGB { get; set; }
        public PoloniexMoedaEntidade BTC_ETC { get; set; }
        public PoloniexMoedaEntidade BTC_FCT { get; set; }
        public PoloniexMoedaEntidade BTC_GNT { get; set; }
        public PoloniexMoedaEntidade BTC_LSK { get; set; }
        public PoloniexMoedaEntidade BTC_XRP { get; set; }
        public PoloniexMoedaEntidade BTC_SC { get; set; }
        public PoloniexMoedaEntidade BTC_STR { get; set; }
        public PoloniexMoedaEntidade BTC_STRAT { get; set; }

        public class PoloniexMoedaEntidade
        {
            public double last { get; set; }
        }
    }
}
