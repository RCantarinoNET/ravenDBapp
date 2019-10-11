using System;
using Raven.Client.Documents;

namespace appRavenDB
{
    public class DocumentManager
    {
        private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateStore);
        public static IDocumentStore Store => _store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore intern = new DocumentStore()
            {
                Database = "db",
                Urls = new[] {"http://127.0.0.1:50311"},
                Conventions =
                {
                    MaxNumberOfRequestsPerSession = 10,
                    UseOptimisticConcurrency = true
                }
            }.Initialize();

            return intern;
        }
    }
}