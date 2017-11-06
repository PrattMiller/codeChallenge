using System;

namespace PME.Logging
{
    public interface ILogFactory : IRunnable
    {
        string LogDirectory
        {
            get;
            set;
        }

        ILog CreateForType<T>();
        ILog CreateForType(Type type);
        ILog CreateForType(object instance);
        ILog Create(string logFullName);
    }
}
