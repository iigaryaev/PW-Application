using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW_Application.Models
{
    public class AccountStateViewModel
    {
        public string Name { get; set; }

        public int Balance { get; set; }


        public IEnumerable<PaymentHistoryItemViewModel> History { get; set; }

        public AccountStateViewModel()
        {
            this.History = new List<PaymentHistoryItemViewModel>();
        }
    }
}