using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW_Application.Models
{
    public enum PaymentDirection
    {
        In, Out
    }


    public class PaymentHistoryItemViewModel
    {
        public int Id { get; set; }

        public PaymentDirection Direction { get; set; }

        public string OtherUserName { get; set; }

        public int Ammount { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedUtc { get; set; }

        public string State { get; set; }
    }
}