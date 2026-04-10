using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banking.Models
{
    public class TransactionRecord
    {
        public int TransactionID { get; set; }
        public int SourceAccountNumber { get; set; }
        public int? DestinationAccountNumber { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
