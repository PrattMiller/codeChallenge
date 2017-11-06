using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using PME.Logging;

namespace PME.Configuration
{
    internal class InMemoryAppSettings : Startable, IAppSettings
    {

        private readonly IDictionary<string, string> _items = new SortedDictionary<string, string>();
        private volatile object _lockObject = new object();

        public InMemoryAppSettings()
        {
            // NOTE: No logger here.  I epxect some settings to exist with a logging implmentation and don't
            // want a circular reference.
        }

        protected override void OnStart()
        {
            // Pre-load the settings object with all app.config settings
            foreach (string key in ConfigurationManager.AppSettings.Keys)
            {
                AddOrUpdate(key, ConfigurationManager.AppSettings[key]);
            }
        }

        protected override void OnStop()
        {
            _items.Clear();
        }

        public IEnumerable<KeyValuePair<string, string>> Get(Regex expression)
        {
            lock (_lockObject)
            {
                foreach (var setting in _items)
                {
                    if (expression.IsMatch(setting.Key))
                    {
                        yield return setting;
                    }
                }
            }
        }

        public string Get(string key)
        {
            if (!IsRunning)
            {
                this.Start();
            }

            return Get(key, string.Empty);
        }

        public T Get<T>(string key, T defaultValue)
        {
            if (!IsRunning)
            {
                this.Start();
            }

            return Get(key, defaultValue, CultureInfo.InvariantCulture);
        }

        public T Get<T>(string key, T defaultValue, IFormatProvider formatProvider)
        {
            if (!IsRunning)
            {
                this.Start();
            }

            if (!_items.ContainsKey(key))
            {
                AddOrUpdate(key, defaultValue);

                return defaultValue;
            }

            var appSettingValue = _items[key];

            if (string.IsNullOrEmpty(appSettingValue))
            {
                return defaultValue;
            }

            return (T)Convert.ChangeType(appSettingValue, typeof(T));
        }

        public void AddOrUpdate(string settingName, object settingValue)
        {
            if (!IsRunning)
            {
                this.Start();
            }

            if (ReferenceEquals(settingValue, null))
            {
                return;
            }
            
            lock (_lockObject)
            {
                _items[settingName] = settingValue.ToString();
            }
        }

    }
}
