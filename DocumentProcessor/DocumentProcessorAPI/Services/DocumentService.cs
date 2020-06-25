using Common.Models;
using DocumentProcessorAPI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentProcessorAPI.Services
{
    public class DocumentService
    {

        public static string SaveDocumentDataFromText(string email, string text)
        {
            DocumentData data = new DocumentData();
            data.UploadTimestamp = DateTime.Now.ToFileTimeUtc().ToString();
            data.UploadedBy = email;

            var id = DocumentStorage.AddDocument(data);

            return id;
        }

        public static DocumentData GetDocument(string id)
        {
            return DocumentStorage.GetDocument(id);
        }

        public static IEnumerable<DocumentData> GetDocuments()
        {
            return DocumentStorage.GetDocuments();
        }
    }
}
