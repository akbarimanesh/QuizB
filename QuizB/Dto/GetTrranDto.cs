﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Dto
{
    public class GetTrranDto
    {
        public int Id { get; set; }
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool isSuccessful { get; set; }
    }
}
