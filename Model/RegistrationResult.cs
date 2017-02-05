using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RegistrationResult
    {
        public RegistrationResult(bool isSuccess, string message = "")
        {
            this.IsSuccess = isSuccess;
            this.MEssage = message;
        }

        public bool IsSuccess { get; private set; }

        public string MEssage { get; private set; }
    }
}
