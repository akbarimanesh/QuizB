using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Bank
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? IsMessage { get; set; }
        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            IsMessage = message;
        }
    }
}
