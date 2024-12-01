using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Entity
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public string Password { get; set; }
       
        [NotMapped]
        public float  SumTransaction { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
