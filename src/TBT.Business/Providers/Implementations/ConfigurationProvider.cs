using System;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ConfigurationManager;

namespace TBT.Business.Providers.Implementations
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private IConfigurationManager configurationManager;

        public ConfigurationProvider(IConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        #region Interface Members

        public string ConnectionString(string name)
        {
            var value = configurationManager.ConnectionStrings[name];

            if (value == null)
            {
                throw new NullReferenceException("Connection string can't be found.");
            }

            return value.ConnectionString;
        }

        public T Get<T>(string key)
        {
            return configurationManager.Get<T>(key);
        }

        #endregion
    }
}
