using Common.Models;
using DocumentProcessorAPI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentProcessorAPI.Services
{
    public class DocumentService
    {

        public static string SaveDocumentDataFromText(string email, int filesize, string text)
        {
            var data = ExtractDocumentDataFromText(email, filesize, text);
                
            var id = DocumentStorage.AddDocument(data);

            return id;
        }

        public static DocumentData ExtractDocumentDataFromText(string email, int filesize, string text)
        {
            DocumentData data = new DocumentData();

            Console.WriteLine(text);

            var lines = text.Split("\n");
            int start = 0;

            // Find where the invoice number is. This is the start of all the parsing.
            // This can happen in various places depending on the document
            for (; start < lines.Length; start++)
            {
                if (lines[start].Contains("Invoice"))
                {
                    Console.WriteLine("starting at line " + start);
                    break;
                }
            }

            // Edge case: invoice 1 has the date on the same line as the invoice number
            // all others have it on a different line
            if ( start > 0 && String.IsNullOrWhiteSpace(lines[start-1]))
            {
                data.InvoiceDate = ExtractDateFromSameLine(lines[start], start);

            }
            else
            {
                if (start > 0 && DateTime.TryParse(lines[start - 1], out DateTime result))
                {
                    data.InvoiceDate = result.ToShortDateString();
                }
                else
                {
                    data.InvoiceDate = ExtractDateFromSameLine(lines[start], start);
                }
            }

            int vendorIndex = start+1;
            while (String.IsNullOrWhiteSpace(lines[vendorIndex]))
            {
                vendorIndex++;
            }
            data.VendorName = lines[vendorIndex].Trim();

            for (int i = start; i< lines.Length; i++)
            {
                // The currency and total due straddle the " Total Due" line
                // or sometimes are after it
                if (lines[i].StartsWith(" Total Due"))
                {
                    if (lines[i + 1].Contains(" "))
                    {
                        var words = lines[i + 1].Trim().Split(" ");
                        data.TotalAmountDue = Decimal.Parse(words[0].Substring(1));
                        data.Currency = words[1].Trim();
                    }
                    else
                    {
                        data.TotalAmountDue = Decimal.Parse(lines[i - 1].Substring(1));
                        data.Currency = lines[i + 1].Trim();
                    }
                   
                }
                // The total is on the same line, but avoid Total Due
                else if (lines[i].StartsWith(" Total") && !lines[i].Contains("Due")) {
                    data.TotalAmount = Decimal.Parse(lines[i].Trim().Split(" ")[1].Substring(1));
                }
                // Taxes are on the same line but can be different strings, so there needs to be some trickery
                // And also discounts contain a % sign too, so watch out
                else if (lines[i].Contains('%') && !lines[i].Contains("off"))
                {
                    var words = lines[i].Trim().Split(" ");
                    data.TaxAmount = Decimal.Parse(words[^1].Substring(1));
                }
            }

            data.UploadTimestamp = DateTime.Now.ToFileTimeUtc().ToString();
            data.UploadedBy = email;
            data.FileSize = filesize;

            return data;
        }

        private static string ExtractDateFromSameLine(string line, int start)
        {
            var words = line.Trim().Split(" ");
            if (DateTime.TryParse(String.Join(' ', words[2], words[3], words[4]), out DateTime result))
            {
               return result.ToShortDateString();
            }
            else
            {
                return null;
            }
        }

        public static DocumentData GetDocument(string id)
        {
            return DocumentStorage.GetDocument(id);
        }

        public static IEnumerable<DocumentData> GetDocuments()
        {
            return DocumentStorage.GetDocuments();
        }

        public static IEnumerable<DocumentStats> GetStats(IEnumerable<DocumentData> documents)
        {
            Dictionary<string, DocumentStats> stats = new Dictionary<string, DocumentStats>();
            foreach (var document in documents)
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

        public static IEnumerable<DocumentStats> GetStats()
        {
            var documents = DocumentStorage.GetDocuments();
            return GetStats(documents);
        }
    }
}
