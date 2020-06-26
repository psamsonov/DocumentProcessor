using Common.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
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

        public static IEnumerable<DocumentStats> GetStats()
        {
            Dictionary<string, DocumentStats> stats = new Dictionary<string, DocumentStats>();
            foreach(var document in GetDictionaryInstance().Values)
            {
                if (!stats.ContainsKey(document.UploadedBy))
                {
                    stats.Add(document.UploadedBy, new DocumentStats());
                }

                var stat = stats[document.UploadedBy];
                stat.FileCount++;
                stat.TotalAmount += document.TotalAmount;
                stat.TotalAmountDue += document.TotalAmountDue;
                stat.TotalFileSize += document.FileSize;

            }

            foreach (var key in stats.Keys)
            {
                stats[key].UploadedBy = key;
            }

            return new List<DocumentStats>(stats.Values);
        }
    }
}
