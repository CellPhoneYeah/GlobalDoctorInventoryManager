using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class PluginHelper
    {
        public static PluginHelper Instance;

        private string _CatalogPath;
        public string CatalogPath
        {
            get;
            set;
        }

        string _CatalogPattern;
        public string CatalogPattern
        {
            get;
            set;
        }

        private CompositionContainer container;

        public string PluginName { get; private set; }

        private PluginHelper()
        {
            if (string.IsNullOrEmpty(_CatalogPattern))
            {
                _CatalogPattern = "*.dll";
            }

            if (string.IsNullOrEmpty(_CatalogPath))
            {
                string pathSetting = ConfigHelper.GetSettingByName("PluginPath");
                if (pathSetting != "")
                {
                    _CatalogPath = pathSetting;
                }
                else
                {
                    //while it is a web ,set the path on .\bin
                    if (System.Web.HttpContext.Current == null)
                        _CatalogPath = ".";
                    else
                        _CatalogPath = ".\bin";
                }
            }
        }

        static PluginHelper()
        {
            Instance = new PluginHelper();
            Instance.PluginName = ConfigHelper.GetSettingByName("PluginName");
            Instance.SetContainer();
        }

        public void SetContainer()
        {
            AggregateCatalog catalog = new AggregateCatalog(new DirectoryCatalog(_CatalogPath, _CatalogPattern));
            container = new CompositionContainer(catalog);
        }

        /// <summary>
        /// get single plugin value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPluginValue<T>()
        {
            List<T> resultList = default(List<T>);
            resultList = GetPluginValues<T>();
            if (resultList.Count > 0)
            {
                return resultList.FirstOrDefault();
            }
            else
            {
                throw new Exception("can't get plugin value");
            }
        }

        /// <summary>
        /// get plugin values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetPluginValues<T>()
        {
            try
            {
                IEnumerable<T> resultCollection = null;
                //if there haven't set plugin name ,then use default exported
                if (string.IsNullOrEmpty(PluginName))
                {
                    resultCollection = container.GetExportedValues<T>();
                    if (resultCollection.Any())
                        return resultCollection.ToList();
                    else
                        throw new Exception("can't exported plugin");
                }
                else
                {
                    resultCollection = container.GetExportedValues<T>(PluginName);
                    if (resultCollection.Any())
                        return resultCollection.ToList();
                    else
                        throw new Exception("can't exported plugin named " + PluginName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("can't exported plugin "+ ex.Message);
            }
        }
    }
}
