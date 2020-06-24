using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class DocumentStats
    {
        public string UploadedBy { get; set; }

        public int FileCount { get; set; }

        public int TotalFileSize { get; set; }

        public int TotalAmount { get; set; }

        public int TotalAmountDue { get; set; }
    }
}
