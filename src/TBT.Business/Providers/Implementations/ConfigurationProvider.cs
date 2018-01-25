using System;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ConfigurationManager;

namespace TBT.Business.Providers.Implementations
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private IConfigurationManager _configurationManager;

        public ConfigurationProvider(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        #region Interface Members

        public string ConnectionString(string name)
        {
            var value = _configurationManager.ConnectionStrings[name];

            if (value == null)
            {
                throw new NullReferenceException("Connection string can't be found.");
            }

            return value.ConnectionString;
        }

        public T Get<T>(string key)
        {
            return _configurationManager.Get<T>(key);
        }

        #endregion
    }
}
