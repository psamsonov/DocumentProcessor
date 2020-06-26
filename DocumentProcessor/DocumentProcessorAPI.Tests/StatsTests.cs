using NUnit.Framework;
using DocumentProcessorAPI.Services;
using System.Collections.Generic;
using Common.Models;
using DocumentProcessorAPI.Storage;
using System.Linq;

namespace DocumentProcessorAPI.Tests
{
    class StatsTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStats ()
        {
            var mockDictionary = new List<DocumentData>();
            mockDictionary.Add(new DocumentData
            {
                UploadedBy = "peter@test.com",
                FileSize = 1,
                TotalAmount = 1,
                TotalAmountDue = 1
            });
            mockDictionary.Add( new DocumentData
            {
                UploadedBy = "peter@test.com",
                FileSize = 1,
                TotalAmount = 1,
                TotalAmountDue = 1
            });
            
            var stats = DocumentService.GetStats(mockDictionary);

            Assert.AreEqual(2, stats.FirstOrDefault().FileCount);
            Assert.AreEqual(2, stats.FirstOrDefault().TotalFileSize);
            Assert.AreEqual(2, stats.FirstOrDefault().TotalAmount);
            Assert.AreEqual(2, stats.FirstOrDefault().TotalAmountDue);
        }



    }
}
