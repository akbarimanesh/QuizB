using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Contract.IService
{
    public interface IServiceCard
    {
        public Result Login(string cardNumber, string password);
      
    }
}
