namespace CryptoManager.Util.Cache
{
    public static class CmCache
    {
        public const int TEMPO_CACHE_12_HORAS = 12 * 60;
        public const int TEMPO_CACHE_5_MINUTOS = 5;
        public const int TEMPO_CACHE_10_MINUTOS = 10;
        public const int TEMPO_CACHE_30_MINUTOS = 30;

        private const string EstrategiaDeCacheNaoDefinida = "Estratégia de cache não definida.";

        private static CmCacheBase CACHE;

        public static CmCacheBase Obter()
        {
            if (CACHE == null)
            {
                throw new CryptoManagerException(EstrategiaDeCacheNaoDefinida);
            }
            return CACHE;
        }

        public static void RegistrarCache(CmCacheBase cache)
        {
            CACHE = cache;
        }
    }
}
