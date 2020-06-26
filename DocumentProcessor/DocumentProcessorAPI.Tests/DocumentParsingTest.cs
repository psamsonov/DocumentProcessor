using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using DocumentProcessorAPI.Services;
using NuGet.Frameworks;

namespace DocumentProcessorAPI.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestInvoice1()
        {
            var text = @"Hubdoc Limited Bank House, 171 Midsummer Boulevard Milton Keynes, Buckinghamshire, MK91EB
  Invoice 1002 February 22, 2019 
 
 UK Company Alan UK test address Toronto, Ontario
 
  Invoice Receipt 
 
 This email confirms your recent payment of £22.50. Here are the details of your payment:
 
 
 
Account Description Amount
alan+ukclient1@hubdoc.com
Business client on 2019 partner promotion
£7.50
alan+ukclient2@hubdoc.com
Business client on 2019 partner promotion
£7.50
alan+ukclient3@hubdoc.com
Business client on 2019 partner promotion
£7.50
 Subtotal £22.50
 Tax 0% £0.00
 Total £22.50
 Paid -£22.50
 Total Due
£0.00 GBP
 
 
 Thank you for your business. Your next invoice date is March 22, 2019. 
 This message was sent to alan+uk@hubdoc.com because you have a Hubdoc account. View your invoice on Hubdoc 
 
 
 
©2019 Hubdoc, Inc | All Rights Reserved | Privacy Policy | Terms of Service
 ";

            var result = DocumentService.ExtractDocumentDataFromText("peter@test.com", 1234, text);

            Assert.AreEqual("2019-02-22", result.InvoiceDate);
            Assert.AreEqual(1234, result.FileSize);
            Assert.AreEqual("GBP", result.Currency);
            Assert.AreEqual(0.00m, result.TaxAmount);
            Assert.AreEqual(22.50m, result.TotalAmount);
            Assert.AreEqual(0.00m, result.TotalAmountDue);
            Assert.AreEqual("peter@test.com", result.UploadedBy);
            Assert.AreEqual("UK Company Alan UK test address Toronto, Ontario", result.VendorName);
        }


        [Test]
        public void TestInvoice2()
        {
            var text = @"
  Invoice 1018 March 12, 2019 
 
 Alan Free Alan Free Australia Fake Address Brisbane, AL
 
  Invoice Receipt 
 
 This email confirms your recent payment of $40.00. Here are the details of your payment:
 
 
 
Account Description Amount
alan+freeclient5@hubdoc.com
Business client on wholesale pricing
$20.00
alan+freeclient3@hubdoc.com
Business client on wholesale pricing
$20.00
alan+freeclient1@hubdoc.com
Business client on wholesale pricing
$20.00
alan+freeclient4@hubdoc.com
Business client on wholesale pricing
$20.00
alan+freeclient2@hubdoc.com
Business client on wholesale pricing
$20.00
alan+freeclient7@hubdoc.com
Business client on wholesale pricing
$20.00
alan+freeclient6@hubdoc.com
Business client on wholesale pricing
$20.00
 Subtotal $140.00
 Xero promotion -$100.00
 Tax 0% $0.00
 Total $40.00
 Paid -$40.00
 Total Due
$0.00 USD
 
 

  Thank you for your business. Your next invoice date is April 12, 2019. 
 This message was sent to alan+free@hubdoc.com because you have a Hubdoc account. View your invoice on Hubdoc 
 
 
©2019 Hubdoc, Inc | All Rights Reserved | Privacy Policy | Terms of Service
 
 ";

            var result = DocumentService.ExtractDocumentDataFromText("peter2@test.com", 12345, text);

            Assert.AreEqual("2019-03-12", result.InvoiceDate);
            Assert.AreEqual(12345, result.FileSize);
            Assert.AreEqual("USD", result.Currency);
            Assert.AreEqual(0.00m, result.TaxAmount);
            Assert.AreEqual(40.00m, result.TotalAmount);
            Assert.AreEqual(0.00m, result.TotalAmountDue);
            Assert.AreEqual("peter2@test.com", result.UploadedBy);
            Assert.AreEqual("Alan Free Alan Free Australia Fake Address Brisbane, AL", result.VendorName);
        }


        [Test]
        public void TestInvoice3()
        {
            var text = @" Invoice 1030 March 13, 2019 
 
 canadian
Alan test address London, ON
 
  Invoice Receipt 
 
 This email confirms your recent payment of $14.13. Here are the details of your payment:
 
 
 
Account Description Amount
alan+caclient@hubdoc.com
Business client on 2019 partner promotion
$12.50
alan+caclient2@hubdoc.com
Business client on 2019 partner promotion
$12.50
alan+caclient3@hubdoc.com
Business client on 2019 partner promotion
$12.50
alan+caclient4@hubdoc.com
Business client on 2019 partner promotion
$12.50
alan+caclient5@hubdoc.com
Business client on wholesale pricing
$25.00
alan+caclient6@hubdoc.com
Business client on wholesale pricing
$25.00
 Subtotal $100.00
 
QBO MAG promotion: 5 Free Accounts
-$87.50
 GST 13% $1.63
 Total $14.13
 Paid -$14.13
 Total Due
$0.00 CAD
 
 
 Thank you for your business. Your next invoice date is April 13, 2019. 
 
 HST/GST # 813417409 
 
This message was sent to alan+ca@hubdoc.com because you have a Hubdoc account. View your invoice on Hubdoc 
 
 
©2019 Hubdoc, Inc | All Rights Reserved | Privacy Policy | Terms of Service
 
 
 ";

            var result = DocumentService.ExtractDocumentDataFromText("peter3@test.com", 12345, text);

            Assert.AreEqual("2019-03-13", result.InvoiceDate);
            Assert.AreEqual(12345, result.FileSize);
            Assert.AreEqual("CAD", result.Currency);
            Assert.AreEqual(1.63m, result.TaxAmount);
            Assert.AreEqual(14.13m, result.TotalAmount);
            Assert.AreEqual(0.00m, result.TotalAmountDue);
            Assert.AreEqual("peter3@test.com", result.UploadedBy);
            Assert.AreEqual("canadian", result.VendorName);
        }

        [Test]
        public void TestInvoice4()
        {
            var text = @" Invoice 1020 March 18, 2019 
 
 The Company Alan AB 55 address london, ON
 
  Invoice Receipt 
 
 This email confirms your recent payment of $118.65. Here are the details of your payment:
 
 
 
Account Description Amount
alan+abclient1@hubdoc.com
Business client on wholesale pricing
$25.00
alan+abclient2@hubdoc.com
Business client on wholesale pricing
$25.00
alan+abclient3@hubdoc.com
Business client on wholesale pricing
$25.00
alan+abclient4@hubdoc.com
Business client on wholesale pricing
$25.00
alan+abclient5@hubdoc.com
Business client on 2019 partner promotion
$12.50
alan+abclient6@hubdoc.com
Business client on 2019 partner promotion
$12.50
 Subtotal $125.00
 
Hubdoc Partner Discount (20% off)
-20.00
 GST 13% $13.65
 Total $118.65
 Paid -$118.65
 Total Due
$0.00 CAD
 
 
 Thank you for your business. Your next invoice date is April 18, 2019. 
 
 HST/GST # 813417409 
 
This message was sent to alan+ab@hubdoc.com because you have a Hubdoc account. View your invoice on Hubdoc 
 
 
©2019 Hubdoc, Inc | All Rights Reserved | Privacy Policy | Terms of Service
 ";

            var result = DocumentService.ExtractDocumentDataFromText("peter3@test.com", 12345, text);

            Assert.AreEqual("2019-03-18", result.InvoiceDate);
            Assert.AreEqual(12345, result.FileSize);
            Assert.AreEqual("CAD", result.Currency);
            Assert.AreEqual(13.65m, result.TaxAmount);
            Assert.AreEqual(118.65m, result.TotalAmount);
            Assert.AreEqual(0.00m, result.TotalAmountDue);
            Assert.AreEqual("peter3@test.com", result.UploadedBy);
            Assert.AreEqual("The Company Alan AB 55 address london, ON", result.VendorName);
        }
        

        //Invoices #4 and #5 appear to be identical, so there will not be another test
    }
}

