using ASF.Entities;
using ASF.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Services.Cache
{
    public class DataCache
    {
        #region Singleton
        private static DataCache _instance;
        private static readonly object InstanceLock = new object();
        public static DataCache Instance
        {
            get {
                if (_instance == null) {
                    lock (InstanceLock) {
                        if (_instance == null) {
                            _instance = new DataCache();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        private readonly ICacheService _cacheService;
        private DataCache() {
            _cacheService = DependencyResolver.Current.GetService<ICacheService>();
        }

        public List<Category> CategoryAll(){
            var result = _cacheService.GetOrAdd(DataCacheSetting.Category.Key,
                () =>
                {
                    var cp = new CategoryProcess();
                    return cp.SelectList();
                }, DataCacheSetting.Category.SlidingExpiration);
            return result;
        }
    }
}