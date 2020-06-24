﻿using System;

namespace Common.Models
{
    public class DocumentData
    {
        public string UploadedBy { get; set; }
        public string UploadTimestamp { get; set; }
        public int FileSize { get; set; }
        public string VendorName { get; set; }
        public string InvoiceDate { get; set; }
        public int TotalAmount { get; set; }
        public int TotalAmountDue { get; set; }
        public string Currency { get; set; }
        public int TaxAmount { get; set; }
        public string ProcessingStatus { get; set; }
    }
}