using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Data.Service.Helper
{
    public sealed class UrlConfigSingleton
    {
        static readonly UrlConfigSingleton _instance = new UrlConfigSingleton();
        public static UrlConfigSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        public UrlConfigSingleton()
        {
            this.Url = ConfigurationManager.AppSettings["AppUrl"];
        }

        public string Url { get; set; }
    }
}
