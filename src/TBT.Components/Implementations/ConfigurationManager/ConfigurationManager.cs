using System;
using TBT.Components.Interfaces.ConfigurationManager;

namespace TBT.Components.Implementations.ConfigurationManager
{
    public class ConfigurationManager : IConfigurationManager
    {
        public T Get<T>(string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];

            if (value == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }


        public System.Configuration.ConnectionStringSettingsCollection ConnectionStrings => System.Configuration.ConfigurationManager.ConnectionStrings;
    }
}
