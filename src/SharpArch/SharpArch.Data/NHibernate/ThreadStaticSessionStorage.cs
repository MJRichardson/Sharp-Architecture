using System;
using System.Collections.Generic;
using NHibernate;

namespace SharpArch.Data.NHibernate
{
    /// <summary>
    /// Per-thread Implementation of ISessionStorage.
    /// This is useful when launching many threads for processing 
    /// (assuming each thread requires its own session).
    /// </summary>
    public class ThreadStaticSessionStorage : ISessionStorage
    {
        public ISession GetSessionForKey(string factoryKey)
        {
            return SimpleSessionStorage.GetSessionForKey(factoryKey);
        }

        public void SetSessionForKey(string factoryKey, ISession session)
        {
            SimpleSessionStorage.SetSessionForKey(factoryKey, session);
        }

        public IEnumerable<ISession> GetAllSessions()
        {
            return SimpleSessionStorage.GetAllSessions();
        }

        private static SimpleSessionStorage SimpleSessionStorage
        {
            get { return _simpleSessionStorage ?? (_simpleSessionStorage = new SimpleSessionStorage()); }
        }

        [ThreadStatic] 
        private static SimpleSessionStorage _simpleSessionStorage;
    }
}