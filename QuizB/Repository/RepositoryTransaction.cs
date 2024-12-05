using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuizB.Contract.IRepository;
using QuizB.DataBase;
using QuizB.Dto;
using QuizB.Entity;
using QuizB.MyMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Repository
{
    public class RepositoryTransaction : IRepositoryTransaction
    {

        private readonly AppDbContext appDbContext;
        string path = "E:/maktab_c#/HW/HW15/QuizB/QuizB/bin/Debug/net8.0/VerificationCode.txt";
       
        public RepositoryTransaction()
        {
            appDbContext = new AppDbContext();
        }

       

        public Card GetCard(string CardNumber)
        {
            return appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == CardNumber );
        }

        public List<GetTrranDto> GetListOfTransactions(string CardNumber)
        {
         

            return appDbContext.Transactions.Where(x => x.Card.CardNumber == CardNumber && x.Card.UserId == MemoryDb.CurrentCard.UserId).AsNoTracking()
                 .Select(x => new GetTrranDto()
                 {
                     Id = x.Id,
                     SourceCardNumber = x.SourceCardNumber,
                     DestinationCardNumber = x.DestinationCardNumber,
                     Amount = x.Amount,
                     TransactionDate = x.TransactionDate,
                     isSuccessful = x.isSuccessful,


                 }).ToList();
        }

        

        public float SumTransactionCard(string CardNumber, float Amount)
        {
            var today = DateTime.Today;
            var sumTransaction = appDbContext.Transactions.Where(x => x.SourceCardNumber == CardNumber && x.TransactionDate.Date == today)
                 .Sum(x => x.Amount);
            MemoryDb.CurrentCard.SumTransaction = sumTransaction;
            return sumTransaction;
        }

        public void Transfer(Transaction transaction)
        {
          
                appDbContext.Transactions.Add(transaction);
                appDbContext.SaveChanges();
        }

        public void GenerateVerificationCode(string CardSouNumber)
        {
            
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            Random random1 = new Random();
            var randomNumber = random1.Next(10000, 100000).ToString();
            
            var date = DateTime.Now;
            var save = $"{CardSouNumber}-{randomNumber}-{date}";
            File.AppendAllText(path, save + Environment.NewLine);
        }

        public string ReadVerificationCode()
        {
            var data = File.ReadAllText(path);
            return data;
          
           
        }
    }


    }

