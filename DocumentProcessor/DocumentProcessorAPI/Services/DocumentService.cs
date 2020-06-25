using Common.Models;
using DocumentProcessorAPI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentProcessorAPI.Services
{
    public class DocumentService
    {

        public static string SaveDocumentDataFromText(string email, int filesize, string text)
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

                var words = lines[start].Trim().Split(" ");
                if (DateTime.TryParse(String.Join(' ', words[2], words[3], words[4]), out DateTime result))
                {
                    data.InvoiceDate = result.ToFileTimeUtc().ToString();
                }
                else
                {
                    Console.WriteLine(lines[start] + " does not contain a DateTime");
                }
               
            }
            else
            {
                if (DateTime.TryParse(lines[start - 1], out DateTime result))
                {
                    data.InvoiceDate = result.ToFileTimeUtc().ToString();
                }
                else
                {
                    Console.WriteLine(lines[start - 1] + " is not a DateTime");
                }
            }

            int vendorIndex = start+1;
            while (String.IsNullOrWhiteSpace(lines[vendorIndex]))
            {
                vendorIndex++;
            }
            data.VendorName = lines[vendorIndex];

            for (int i = start; i< lines.Length; i++)
            {
                // The currency and total due straddle the " Total Due" line
                // or sometimes are after it
                if (lines[i] == " Total Due")
                {
                    if (lines[i + 1].Contains(" "))
                    {
                        var words = lines[i + 1].Trim().Split(" ");
                        data.TotalAmountDue = Decimal.Parse(words[0].Substring(1));
                        data.Currency = words[1];
                    }
                    else
                    {
                        data.TotalAmountDue = Decimal.Parse(lines[i - 1].Substring(1));
                        data.Currency = lines[i + 1];
                    }
                   
                }
                // The total is on the same line
                else if (lines[i].StartsWith(" Total")) {
                    data.TotalAmount = Decimal.Parse(lines[i].Trim().Split(" ")[1].Substring(1));
                }
                // Taxes are on the same line but can be different strings, so there needs to be some trickery
                else if (lines[i].Contains("%"))
                {
                    var words = lines[i].Trim().Split(" ");
                    data.TotalAmount = Decimal.Parse(words[^1].Substring(1));
                }
            }

            data.UploadTimestamp = DateTime.Now.ToFileTimeUtc().ToString();
            data.UploadedBy = email;
            data.FileSize = filesize;

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
