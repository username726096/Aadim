using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AadimPark.Models.ViewModels
{
    public class paymentVM
    {
        public int? ID { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string TransactionUuid { get; set; }
        public string ProductCode { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
        public string SignedFieldNames { get; set; }
        public string Signature { get; set; }
        public string Secret { get; set; }
    }
}