using System;

namespace Common.Models
{
    public class DocumentData
    {
        public string UploadedBy { get; set; }
        public string UploadTimestamp { get; set; }
        public int FileSize { get; set; }
        public string VendorName { get; set; }
        public string InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountDue { get; set; }
        public string Currency { get; set; }
        public decimal TaxAmount { get; set; }
        public string ProcessingStatus { get; set; }
        public string Id { get; set; }
    }
}
