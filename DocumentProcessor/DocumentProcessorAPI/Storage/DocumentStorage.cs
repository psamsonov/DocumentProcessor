using Common.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentProcessorAPI.Storage
{
    public class DocumentStorage
    {
        public static Dictionary<string, DocumentData> documents;

        private static Dictionary<string, DocumentData> GetDictionaryInstance()
        {
            if (documents == null)
            {
                documents = new Dictionary<string, DocumentData>();
            }
            return documents;
        }

        public static DocumentData GetDocument(string id)
        {
            if (GetDictionaryInstance().ContainsKey(id))
            {
                return GetDictionaryInstance()[id];
            }
            return null;
        }

        public static IEnumerable<DocumentData> GetDocuments()
        {
            return new List<DocumentData>(GetDictionaryInstance().Values);
        }

        public static string AddDocument(DocumentData document)
        {
            string id = Guid.NewGuid().ToString();
            document.Id = id;
            GetDictionaryInstance().Add(id, document);
            return id;
        }
    }
}
