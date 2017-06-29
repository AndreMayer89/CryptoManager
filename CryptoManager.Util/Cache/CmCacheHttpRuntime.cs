using System;
using System.Collections.Generic;

namespace CryptoManager.Util.Cache
{
    public class CmCacheHttpRuntime : CmCacheBase
    {
        #region Classes

        public class ItemCache<TTipoObj>
        {
            public TTipoObj Item { get; set; }

            public static ItemCache<TTipoObj> ConstruirItem(TTipoObj obj)
            {
                ItemCache<TTipoObj> objLocal = new ItemCache<TTipoObj>();
                objLocal.Item = obj;
                return objLocal;
            }
        }

        #endregion

        #region Métodos Base

        public override bool ContemItem(string chaveItem)
        {
            return System.Web.HttpRuntime.Cache[chaveItem] != null;
        }

        protected override void RemoverItem(string chaveItem)
        {
            System.Web.HttpRuntime.Cache.Remove(chaveItem);
        }

        public override TTipoItem ObterItem<TTipoItem>(string chaveItem)
        {
            return ((ItemCache<TTipoItem>)System.Web.HttpRuntime.Cache[chaveItem]).Item;
        }

        public override void AdicionarItem<TTipoItem>(string chaveItem, TTipoItem item, int duracaoMinutos)
        {
            ItemCache<TTipoItem> itemCache = ItemCache<TTipoItem>.ConstruirItem(item);
            System.Web.HttpRuntime.Cache.Insert(chaveItem, itemCache, null, DateTime.UtcNow.AddMinutes(duracaoMinutos),
                    System.Web.Caching.Cache.NoSlidingExpiration);
        }

        protected override List<string> ObterChaves()
        {
            List<string> keys = new List<string>();
            System.Collections.IDictionaryEnumerator enumerator = System.Web.HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            return keys;
        }

        public override void LimparCache()
        {
            System.Collections.IDictionaryEnumerator enumerator = System.Web.HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                System.Web.HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
        }

        #endregion
    }
}
