using System;
using PME.Logging;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PME.Configuration
{
    public interface IAppSettings : IRunnable
    {

        string Get(string key);

        IEnumerable<KeyValuePair<string, string>> Get(Regex expression);

        T Get<T>(string key, T defaultValue);

        T Get<T>(string key, T defaultValue, IFormatProvider formatProvider);


    }
}
