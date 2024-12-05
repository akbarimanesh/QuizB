using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
       
        public List<Card> Cards { get; set; }
    }
}
