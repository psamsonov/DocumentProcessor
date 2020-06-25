using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessorAPI.Services
{
    public class PDFService
    {
        public static string GetTextFromPDFBytes(byte[] pdf)
        {
            return GetPDFFromFile(WritePDFToTempFolder(pdf));
        }

        public static string WritePDFToTempFolder(byte[] pdf)
        {
            string tempPath = System.IO.Path.GetTempPath() + System.IO.Path.PathSeparator + Guid.NewGuid().ToString() + ".pdf";
            System.IO.File.WriteAllBytes(tempPath, pdf);
            return tempPath;
        }

        public static string GetPDFFromFile(string path)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(path));

            LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();

            PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
            parser.ProcessPageContent(pdfDoc.GetFirstPage());

            pdfDoc.Close();

            return strategy.GetResultantText();

        }
    }
}
